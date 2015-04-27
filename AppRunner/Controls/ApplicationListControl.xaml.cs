using AppRunner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppRunner.Controls
{
    /// <summary>
    /// Interaction logic for ApplicationListControl.xaml
    /// </summary>
    public partial class ApplicationListControl : UserControl
    {
        public ApplicationListControl()
        {
            ActiveApplicationChangeEvent = (s, e) => { };
            InitializeComponent();
        }

        public ApplicationListModel AppListModel
        {
            get
            {
                return DataContext as ApplicationListModel;
            }
        }

        private void ButtonCallBack(object sender, RoutedEventArgs e)
        {
            var action = (sender as Button).Content.ToString();
            if (action == "Delete")
            {
                var app = (sender as Button).Tag as ApplicationModel;
                AppListModel.Remove(app);
            }
            else if (action == "Add Application")
            {
                AppListModel.Add(new ApplicationModel());
            }
            else if (action == "View")
            {
                var app = (sender as Button).Tag as ApplicationModel;
                SetActiveApplication(app);
            }
        }

        private void SetActiveApplication(ApplicationModel app)
        {
            AppListModel.SetActiveApplication(app);
        }

        public event ApplicationListModel.ActiveApplicationChangeEventHandler ActiveApplicationChangeEvent;
    }

}
