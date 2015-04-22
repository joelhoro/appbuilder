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
    class LogFileViewModel : PropertyNotify
    {
        public object parent;

        public LogFileViewModel(string fileName)
        {
            LogFileName = fileName;
            StartTimer();
        }


        public void StartTimer()
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //var x = FileContent;
            NotifyPropertyChanged("FileContent");
        }

        private string logFileName;
        public string LogFileName { get { return logFileName; } set { logFileName = value; NotifyPropertyChanged(); } }

        public string FileContent
        {
            get
            {
                StreamReader streamReader = new StreamReader(LogFileName);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                var s = DateTime.Now.ToString();
                return  s + "\n\n" + text;
            }
            set { }
        }

    }
}
