using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
        private string workSpace;
        [DataMember]
        private string executable;
        [DataMember]
        private string commandLineArgs;
        public string WorkSpace { get { return workSpace; } set { workSpace = value; NotifyPropertyChanged(); } }
        public string Executable { get { return executable; } set { executable = value; NotifyPropertyChanged(); } }
        public string CommandLineArgs { get { return commandLineArgs; } set { commandLineArgs = value; NotifyPropertyChanged(); } }
        public Solution Solution;

        public void Run()
        {
            MessageBox.Show(String.Format("Running {0} with {1}", Executable, CommandLineArgs));
            // run executable somehow
        }

        public void Build()
        {
            Solution = new Solution(WorkSpace, Executable);
            var outputPath = @"C:\temp\build1";
            Solution.Build(outputPath, Executable);
        }

        public override string ToString()
        {
            return "[{0}] {1} {2}".With(WorkSpace, Executable, CommandLineArgs);
        }
    }
}
