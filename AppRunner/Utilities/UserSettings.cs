using System.IO;
using AppRunner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppRunner.Utilities
{
    public class UserSettings
    {
        public bool TestMode = true;
        public string MsBuildPath = @"""c:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe""";

        public ObservableCollection<string> Workspaces = new ObservableCollection<string>();
        public List<ApplicationVM> Applications = new List<ApplicationVM>() {new ApplicationVM(empty: true) };
        public Dictionary<string,List<string>> CommandLineHistory = new Dictionary<string, List<string>>();
        public string TmpPath = @"C:\temp\apprunner";
        public string BuildDir = "{0}_{{0:d3}}";

        internal void AddToHistory(string Executable, string commandLineArgs)
        {
            var executableWithoutExtension = Path.GetFileNameWithoutExtension(Executable);
            if (executableWithoutExtension == null) return;
            if(!CommandLineHistory.ContainsKey(executableWithoutExtension))
                CommandLineHistory[executableWithoutExtension] = new List<string>() {commandLineArgs};

            if(!CommandLineHistory[executableWithoutExtension].Contains(commandLineArgs))
                CommandLineHistory[executableWithoutExtension].Add(commandLineArgs);
        }
    }

    
}
