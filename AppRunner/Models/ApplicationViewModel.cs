using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppRunner.Models
{
    class ApplicationViewModel : PropertyNotify
    {
        public ObservableCollection<string> ExecutableChoices { get; set; }

        public ApplicationViewModel()
        {
            ExecutableChoices = new ObservableCollection<string>() { "AAA", "BBB" };
        }

        private string executable;
        private string commandlineargs;
        public string Executable { get { return executable; } set { executable = value; NotifyPropertyChanged(); } }
        public string CommandLineArgs { get { return commandlineargs; } set { commandlineargs = value; NotifyPropertyChanged(); } }

        public void Run()
        {
            MessageBox.Show(String.Format("Running {0} with {1}", Executable, CommandLineArgs));
            // run executable somehow
        }
    }
}
