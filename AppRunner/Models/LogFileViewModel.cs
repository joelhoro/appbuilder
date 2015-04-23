using AppRunner.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppRunner.Models
{
    public class LogFileViewModel : PropertyNotify
    {
        public object parent;

        public LogFileViewModel(string fullFileName)
        {
            FullFileName = fullFileName;
//            StartTimer();
        }

        private string fullFileName;
        public string FullFileName { get { return fullFileName; } set { fullFileName = value; NotifyPropertyChanged(); } }

        public string FileName { get { return Path.GetFileName(FullFileName); } }

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
                StreamReader streamReader = new StreamReader(FullFileName);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                var s = DateTime.Now.ToString();
                return  s + "\n\n" + text;
            }
            set { }
        }

    }
}
