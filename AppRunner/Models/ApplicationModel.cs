using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using AppRunner.Utilities;

namespace AppRunner.Models
{
    [DataContract]
    public class ApplicationModel : PropertyNotify
    {
        public ObservableCollection<string> ExecutableChoices { get { 
            return new ObservableCollection<string>() { "MiniApp", "Hindsight" };
        } }

        public ObservableCollection<string> WorkSpaceChoices
        {
            get
            {
                return AppEnvironment.Settings.Workspaces;
            }
        }

        public ApplicationModel()
        {
            WorkSpace = WorkSpaceChoices.First();
            Executable = ExecutableChoices.First();
            CommandLineArgs = "<command line args>";
        }

        [DataMember]
        private string _workSpace;
        [DataMember]
        private string _executable;
        [DataMember]
        private string _commandLineArgs;
        public string WorkSpace { get { return _workSpace; } set { _workSpace = value; NotifyPropertyChanged(); } }
        public string Executable { get { return _executable; } set { _executable = value; NotifyPropertyChanged(); } }
        public string CommandLineArgs { get { return _commandLineArgs; } set { _commandLineArgs = value; NotifyPropertyChanged(); } }
        public Solution Solution;

        public void Run()
        {
            MessageBox.Show(String.Format("Running {0} with {1}", Executable, CommandLineArgs));
            // run executable somehow
        }

        private StringBuilder _buildOutput = new StringBuilder();
        public string BuildOutput { get { return _buildOutput.ToString(); } }

        public enum ApplicationStatus
        {
            Idle,
            Building,
            BuildFailed,
            BuildSucceeded,
            Running,
            Completed
        };

        public ApplicationStatus Status
        {
            get { return _status; }
            private set { _status = value; 
                NotifyPropertyChanged(); 
                NotifyPropertyChanged("CanBuild");
                NotifyPropertyChanged("CanRun");
                NotifyPropertyChanged("CanBuildRun");
            }
        }

        public void Initialize(ApplicationListModel parent, int idx)
        {
            _parent = parent;
            _parentIdx = idx;

            _buildOutput = new StringBuilder();
            Test = null;
            Status = ApplicationStatus.Idle;
        }

        public Executable Test; // clearly this is just for testing...
        private ApplicationStatus _status;
        private ApplicationListModel _parent;
        private int _parentIdx;

        private ApplicationStatus[] CanBuildStages
        {
            get { return new[] { ApplicationStatus.Idle, ApplicationStatus.BuildFailed, ApplicationStatus.Completed }; }  
        }

        private ApplicationStatus[] CanRunStages
        {
            get { return new[] { ApplicationStatus.BuildSucceeded, ApplicationStatus.Completed }; }
        }
       
        public bool CanBuild
        {
            get { return CanBuildStages.Contains(Status); }
        }
        public bool CanRun
        {
            get { return CanRunStages.Contains(Status); }
        }
        public bool CanBuildRun
        {
            get { return CanBuild && CanRun; }
        }

        public void Build()
        {
            //Solution = new Solution(WorkSpace, Executable);
            //var outputPath = @"C:\temp\build1";
            //Solution.Build(outputPath, Executable);
            var file = @"C:\Users\Joel\Documents\Visual Studio 2013\Projects\DummyExe\DummyExe\bin\x64\Debug\DummyExe.exe";
            DataReceivedEventHandler appendLine = (s, e) => { _buildOutput.AppendLine(e.Data); };
            if (Test != null)
            {
                Test.Destroy();
            }
            Test = new Executable(file);
            
            _parent.SetActiveApplication(this);
            Status = ApplicationStatus.Building;
            Test.AddOutputHandler(appendLine);
            Test.ExecutionCompleted += (s, e) => { 
                Status = ApplicationStatus.Completed;
                Test.Destroy();
            };

            var path = @"C:\temp\apprunner\build-{0:d3}".With(_parentIdx);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var counter = 0;
            while (Directory.Exists(@"{0}\{1:d3}".With(path, counter)))
                counter++;
            string finalPath = @"{0}\{1:d3}".With(path, counter);

            Directory.CreateDirectory(finalPath);
            var args = "3 \"{0} - {1}\"".With(finalPath,Executable);
            Test.Run(args);
        }

        public override string ToString()
        {
            return "[{0}] {1} {2}".With(WorkSpace, Executable, CommandLineArgs);
        }
    }
}
