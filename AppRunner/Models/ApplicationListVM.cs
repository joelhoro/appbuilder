using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Models
{
    public class ApplicationListVM : PropertyNotify
    {
        private ObservableCollection<ApplicationVM> _applicationList;
        public ObservableCollection<ApplicationVM> ApplicationList
        {
            get { return _applicationList; }
            set { _applicationList = value; NotifyPropertyChanged(); }
        }

        private ApplicationVM _activeApplication;
        public ApplicationVM ActiveApplication
        {
            get { return _activeApplication; }
            set { _activeApplication = value; NotifyPropertyChanged(); }
        }

        public delegate void ActiveApplicationChangeEventHandler(object sender, ApplicationVM args);

        public event ActiveApplicationChangeEventHandler ActiveApplicationChangeEvent;

        public static ApplicationListVM Create(IEnumerable<ApplicationVM> applicationModels)
        {
            var listModel = new ApplicationListVM();
            var i = 0;
            applicationModels
                .Select(a => { a.Initialize(listModel,i++); return true; })
                .ToList();

            listModel.ApplicationList = new ObservableCollection<ApplicationVM>(applicationModels);
            return listModel;
        }

        public void Add(ApplicationVM obj)
        {
            ApplicationList.Add(obj);
        }
        public void Remove(ApplicationVM obj)
        {
            ApplicationList.Remove(obj);
        }
    }
}
