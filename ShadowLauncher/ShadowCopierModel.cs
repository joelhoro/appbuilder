using AppRunner.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using AppRunner.Models;
using Microsoft.Win32;

namespace ShadowLauncher
{
    public class Settings
    {
        private const string SettingsFileName = @"C:\temp\shadowlauncher.json";

        [DataMember]
        public Dictionary<string, List<string>> History;

        public static Settings Load()
        {
            if (!File.Exists(SettingsFileName))
            {
                var settings = new Settings { History = new Dictionary<string, List<string>>() };
                Serializer.Save(SettingsFileName,settings);
                return settings;
            }
            else
                return Serializer.Load<Settings>(SettingsFileName);
        }

        public void Save()
        {
            Serializer.Save(SettingsFileName,this);
        }
    }

    class ShadowCopierModel : PropertyNotify
    {

        private Settings _settings;
        private string _solutionName;
        public string SolutionName { get { return _solutionName; } set { _solutionName = value; NotifyPropertyChanged(); } }
        public string BinaryDirectory { get; set; }
        public string CommandLineArgs { get; set; }

        public List<string> CommandLineHistory
        {
            get
            {
                var history = _settings.History.GetOrDefault(SolutionName);
                CommandLineArgs = history.FirstOrDefault();
                return history;
            }
        }

        public string Output { get; set; }

        public ShadowCopierModel(string solutionName, string binaryDirectory)
        {
            SolutionName = solutionName;
            BinaryDirectory = binaryDirectory;
            _settings = Settings.Load();
        }

        public string OutputDirectory
        {
            get
            {
                var solutionName = Path.GetFileNameWithoutExtension(SolutionName);
                var path = @"C:\temp\build\" + solutionName + @"\";
                var counter = 1;
                Func<int,string> fileName = i => String.Format("{0}{1:D2}", path, counter);
                while (Directory.Exists(fileName(counter)))
                    counter++;
                return fileName(counter);
            }
        }

        public Solution build = null;

        public void BuildAndrun()
        {
            List<string> value;

            AddToHistory();
            string fullPathName = @"C:\Users\Joel\Documents\Visual Studio 2013\Projects\DummyExe\DummyExe\bin\x64\Debug\DummyExe.exe";
            build = new Solution(fullPathName);
            build.Build(OutputDirectory);
        }

        private void AddToHistory()
        {
            var existingHistory = _settings.History.GetOrDefault(SolutionName);
            if (existingHistory.Last() != CommandLineArgs)
                existingHistory.Add(CommandLineArgs);
            _settings.History[SolutionName] = existingHistory;
            _settings.Save();
            NotifyPropertyChanged("CommandLineHistory");
        }
    }
}
