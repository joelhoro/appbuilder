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
        private ObservableCollection<ApplicationViewModel> _applicationList;
        public ObservableCollection<ApplicationViewModel> ApplicationList
        {
            get { return _applicationList; }
            set { _applicationList = value; NotifyPropertyChanged(); }
        }

        private ApplicationViewModel _activeApplication;
        public ApplicationViewModel ActiveApplication
        {
            get { return _activeApplication; }
            set { _activeApplication = value; NotifyPropertyChanged(); }
        }

        public delegate void ActiveApplicationChangeEventHandler(object sender, ApplicationViewModel args);

        public event ActiveApplicationChangeEventHandler ActiveApplicationChangeEvent;

        public static ApplicationListViewModel Create(IEnumerable<ApplicationViewModel> applicationViewModels)
        {
            var listModel = new ApplicationListViewModel();
            var i = 0;
            applicationViewModels
                .Select(a => { a.Initialize(listModel,i++); return true; })
                .ToList();

            listModel.ApplicationList = new ObservableCollection<ApplicationViewModel>(applicationViewModels);
            return listModel;
        }

        public void Add(ApplicationViewModel obj)
        {
            ApplicationList.Add(obj);
        }
        public void Remove(ApplicationViewModel obj)
        {
            ApplicationList.Remove(obj);
        }

        internal void SetActiveApplication(ApplicationViewModel app)
        {
            this.ActiveApplication = app;
            ActiveApplicationChangeEvent(this, app);
        }
    }
}
