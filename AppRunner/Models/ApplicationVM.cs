﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls.Primitives;
using AppRunner.Properties;
using AppRunner.Utilities;

namespace AppRunner.Models
{
    #region Application status
    public enum ApplicationStatus
    {
        Idle,
        Building,
        BuildFailed,
        BuildCompleted,
        Running,
        Completed,
        Aborted
    };
    [DataContract]
    public class ApplicationVM : PropertyNotify
    {
        #region Choices

        public ObservableCollection<string> SolutionChoices
        {
            get { return new ObservableCollection<string>(FileSystem.FileList(WorkSpace)); }
        }

        public ObservableCollection<string> WorkSpaceChoices
        {
            get
            {
                return AppEnvironment.Settings.Workspaces;
            }
        }

        public ObservableCollection<string> CommandLineHistory
        {
            get
            {
                return new ObservableCollection<string>(AppEnvironment.Settings.CommandLineHistory.GetOrDefault(Executable));
            }
        }

        #endregion
        public ApplicationVM()
        {
            WorkSpace = WorkSpaceChoices.First();
            //Executable = ExecutableChoices.First();
            CommandLineArgs = "<command line args>";
        }

        [DataMember]
        private string _workSpace;
        [DataMember]
        private string _solution;
        [DataMember]
        private string _executable;
        [DataMember]
        private string _commandLineArgs;
        public string WorkSpace { get { return _workSpace; } set { _workSpace = value; NotifyPropertyChanged(); NotifyPropertyChanged("SolutionChoices");} }
        public string Executable { get { return _executable; } set { _executable = value; NotifyPropertyChanged(); } }
        public string Solution { get { return _solution; } set { _solution = value; NotifyPropertyChanged(); } }
        public string CommandLineArgs { get { return _commandLineArgs; } set { _commandLineArgs = value; NotifyPropertyChanged(); } }
        //public Solution Solution;


        public void Initialize(ApplicationListVM parent, int idx)
        {
            _parent = parent;
            _parentIdx = idx;

            Status = ApplicationStatus.Idle;
        }

        private ApplicationStatus _status;
        private ApplicationListVM _parent;
        private int _parentIdx;

        public bool IsActiveApplication { get { return _parent.ActiveApplication == this; } }

        public ApplicationStatus Status
        {
            get { return _status; }
            internal set
            {
                _status = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CanBuild");
                NotifyPropertyChanged("CanRun");
                NotifyPropertyChanged("CanBuildRun");
                NotifyPropertyChanged("CanAbort");
                NotifyPropertyChanged("Description");
            }
        }

        private ApplicationStatus[] CanBuildStages
        {
            get { return new[] { ApplicationStatus.Idle, ApplicationStatus.BuildFailed, ApplicationStatus.Completed, ApplicationStatus.BuildCompleted, ApplicationStatus.Aborted }; }  
        }

        private ApplicationStatus[] CanRunStages
        {
            get { return new[] { ApplicationStatus.BuildCompleted, ApplicationStatus.Completed }; }
        }
       
        public bool CanBuild
        {
            get { return CanBuildStages.Contains(Status); }
        }
        public bool CanRun
        {
            get { return CanRunStages.Contains(Status); }
        }
        public bool CanAbort
        {
            get { return Status != ApplicationStatus.Idle; }
        }

        public bool CanBuildRun
        {
            get { return CanBuild && CanRun; }
        }

        #endregion

        public ISolution SolutionObj;

        public void Build()
        {
            if(AppEnvironment.Settings.TestMode)
                SolutionObj = new DummySolution(WorkSpace,Solution);
            else
                SolutionObj = new Solution(WorkSpace, Solution);

            var path = AppEnvironment.Settings.Path;
            var mask = AppEnvironment.Settings.BuildDir;
            var outputPath = FileSystem.GetFirstDirName(path, mask,createDirectory: true);

            SolutionObj.BuildAsync(outputPath);
            SetActiveApplication(_parent);

            Status = ApplicationStatus.Building;
            SolutionObj.ExecutionCompleted += (s, e) => { 
                Status = ApplicationStatus.BuildCompleted;
            };
        }

        public Executable ExecutableObj;

        public void Run()
        {
            AppEnvironment.Settings.AddToHistory(Executable, CommandLineArgs);
            NotifyPropertyChanged("CommandLineHistory");

            var executableFullPath = WorkSpace + Solution + Executable;
            if(AppEnvironment.Settings.TestMode)
                executableFullPath = @"DummyExe.exe";

            var commandLineExpanded = Extensions.ExpandCommandLine(CommandLineArgs);
            ExecutableObj = new Executable(executableFullPath);
            Status = ApplicationStatus.Running;
            ExecutableObj.RunAsync("15 Test running with " + commandLineExpanded);
            ExecutableObj.ExecutionCompleted += (s, e) =>
            {
                Status = ApplicationStatus.BuildCompleted;
            };
        }


        public string Description { get { return ToString(); } }

        public string Output
        {
            get
            {
                if (Status == ApplicationStatus.Running || Status == ApplicationStatus.Completed) return ExecutableObj.Output;
                if (SolutionObj != null)
                    return SolutionObj.Output;
                else
                    return "<idle>";
            }
        }

        public override string ToString()
        {
            if(Status == ApplicationStatus.Building)
                return "Building {0}".With(SolutionObj);
            else if (Status == ApplicationStatus.BuildCompleted)
                return "Build failed!";
            else
                return "[{0}] {1} {2}".With(WorkSpace, Executable, CommandLineArgs);
        }

        internal void SetActiveApplication(ApplicationListVM AppListVm)
        {
            AppListVm.ActiveApplication = this;
            AppListVm.ApplicationList
                .ForEach(a => a.NotifyPropertyChanged("IsActiveApplication"));
        }
    }
}