using System;
using System.Collections.ObjectModel;
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
        public Executable Test;
        public void Build()
        {
            //Solution = new Solution(WorkSpace, Executable);
            //var outputPath = @"C:\temp\build1";
            //Solution.Build(outputPath, Executable);
            var file = @"C:\Users\Joel\Documents\Visual Studio 2013\Projects\DummyExe\DummyExe\bin\x64\Debug\DummyExe.exe";
            Test = new Executable(file);
            _buildOutput = new StringBuilder();
            Test.OutputDataReceived += (s, e) => { _buildOutput.AppendLine(e.Data); };
            var args = "100 100 \"{0}\"".With(ToString());
            Test.Run(args);
        }

        public override string ToString()
        {
            return "[{0}] {1} {2}".With(WorkSpace, Executable, CommandLineArgs);
        }
    }
}
