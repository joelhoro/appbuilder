using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Data;

namespace AppRunner.Models
{
    public class DirectoryListVM : PropertyNotify
    {
        private string _host;
        private string _directory;

        public string Host { get { return _host; } set { _host = value; NotifyPropertyChanged(); } }
        public string DirectoryName { get { return _directory; } set { _directory = value; NotifyPropertyChanged(); } }

        public DirectoryListVM(string path, string host = "localhost")
        {
            Host = host;
            DirectoryName = path;
        }

        public string FullPath { get { return String.Format("\\\\{0}\\{1}", Host, DirectoryName ); } }

        public CollectionViewSource Files
        {
            get
            {
                var data = new ObservableCollection<LogFileVM>(
                    Directory.GetFiles(FullPath, "*.*")
                    .Select(f => new LogFileVM(f)))
                    .OrderBy(f => f.AppName)
                    .ThenByDescending(f => f.CreationDate);

                var cvs = new CollectionViewSource {Source = data};
                cvs.GroupDescriptions.Add(new PropertyGroupDescription("AppName"));
                return cvs;
            }
        }

    }
}
