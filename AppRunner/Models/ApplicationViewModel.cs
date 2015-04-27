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
    public class ApplicationViewModel : PropertyNotify
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

        public ApplicationViewModel()
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

        private bool Flash { get { return true; } }

        private StringBuilder _buildOutput = new StringBuilder();
        public string BuildOutput { get { return _buildOutput.ToString(); } }

        public enum ApplicationStatus
        {
            Idle,
            Building,
            BuildFailed,
            Running,
            Completed
        };

        public ApplicationStatus Status
        {
            get { return _status; }
            private set { _status = value; NotifyPropertyChanged();}
        }

        public void Initialize(ApplicationListViewModel parent, int idx)
        {
            _parent = parent;
            _parentIdx = idx;

            _buildOutput = new StringBuilder();
            Test = null;
            Status = ApplicationStatus.Idle;
        }

        public Executable Test;
        private ApplicationStatus _status;
        private ApplicationListViewModel _parent;
        private int _parentIdx;

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
            var args = "30 \"{0} - {1}\"".With(finalPath,Executable);
            Test.Run(args);
        }

        public override string ToString()
        {
            return "[{0}] {1} {2}".With(WorkSpace, Executable, CommandLineArgs);
        }
    }
}
