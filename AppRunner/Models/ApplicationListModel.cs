using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Models
{
    public class ApplicationListModel : PropertyNotify
    {
        private ObservableCollection<ApplicationModel> _applicationList;
        public ObservableCollection<ApplicationModel> ApplicationList
        {
            get { return _applicationList; }
            set { _applicationList = value; NotifyPropertyChanged(); }
        }

        private ApplicationModel _activeApplication;
        public ApplicationModel ActiveApplication
        {
            get { return _activeApplication; }
            set { _activeApplication = value; NotifyPropertyChanged(); }
        }

        public delegate void ActiveApplicationChangeEventHandler(object sender, ApplicationModel args);

        public event ActiveApplicationChangeEventHandler ActiveApplicationChangeEvent;

        public static ApplicationListModel Create(IEnumerable<ApplicationModel> applicationModels)
        {
            var listModel = new ApplicationListModel();
            var i = 0;
            applicationModels
                .Select(a => { a.Initialize(listModel,i++); return true; })
                .ToList();

            listModel.ApplicationList = new ObservableCollection<ApplicationModel>(applicationModels);
            return listModel;
        }

        public void Add(ApplicationModel obj)
        {
            ApplicationList.Add(obj);
        }
        public void Remove(ApplicationModel obj)
        {
            ApplicationList.Remove(obj);
        }

        internal void SetActiveApplication(ApplicationModel app)
        {
            this.ActiveApplication = app;
            ActiveApplicationChangeEvent(this, app);
        }
    }
}
