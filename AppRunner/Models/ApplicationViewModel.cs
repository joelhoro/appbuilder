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

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
