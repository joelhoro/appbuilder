using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Models
{
    public class ApplicationListViewModel : PropertyNotify
    {
        private ObservableCollection<ApplicationViewModel> applicationList;
        public ObservableCollection<ApplicationViewModel> ApplicationList
        {
            get { return applicationList; }
            set { applicationList = value; NotifyPropertyChanged(); }
        }

        private ApplicationViewModel activeApplication;
        public ApplicationViewModel ActiveApplication
        {
            get { return activeApplication; }
            set { activeApplication = value; NotifyPropertyChanged(); }
        }

        public void Add(ApplicationViewModel obj)
        {
            ApplicationList.Add(obj);
        }
        public void Remove(ApplicationViewModel obj)
        {
            ApplicationList.Remove(obj);
        }
    }
}
