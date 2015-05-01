using System;
using System.IO;
using System.Linq;

namespace AppRunner.Models
{
    public class LogFileVM : PropertyNotify
    {
        public object Parent;

        public LogFileVM(string fullFileName)
        {
            FullFileName = fullFileName;
//            StartTimer();
        }

        private string _fullFileName;
        public string FullFileName { get { return _fullFileName; } set { _fullFileName = value; NotifyPropertyChanged(); } }

        public string FileName { get { return Path.GetFileName(FullFileName); } }

        // ReSharper disable once ExplicitCallerInfoArgument
        public void UpdateContent() { NotifyPropertyChanged("FileContent"); }
        public double FileSize { get { return new FileInfo(FullFileName).Length; } }
        public DateTime CreationDate { get { return File.GetCreationTime(FullFileName); } }

        public string AppName
        {
            get
            {
                var app = new[] { "MiniApp", "vNextApp" }
                    .Where(appName => FileName.Contains(appName));
                if (app.Count() == 1)
                    return app.Single();
                else
                    return "Others";
            }
        }
        public string FileContent
        {
            get
            {
                string text;
                try
                {
                    StreamReader streamReader = new StreamReader(FullFileName);
                    text = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                catch
                {
                    text = String.Format("Could not open {0}", _fullFileName);
                }
                return text;
            }
            set { }
        }

    }
}
