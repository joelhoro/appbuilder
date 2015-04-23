﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Models
{
    class DirectoryListViewModel : PropertyNotify
    {
        private string host;
        private string directory;

        public string Host { get { return host; } set { host = value; NotifyPropertyChanged(); } }
        public string DirectoryName { get { return directory; } set { directory = value; NotifyPropertyChanged(); } }

        public DirectoryListViewModel(string path, string host = "localhost")
        {
            Host = host;
            DirectoryName = path;
        }

        public string FullPath { get { return String.Format("\\\\{0}\\{1}", Host, DirectoryName ); } }

        public ObservableCollection<LogFileViewModel> Files
        {
            get
            {
                return new ObservableCollection<LogFileViewModel>( 
                    Directory.GetFiles(FullPath,"*.*")
                    .Select(f => new LogFileViewModel(f)) );
            }
        }

    }
}