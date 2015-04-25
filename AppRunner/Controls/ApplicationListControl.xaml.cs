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
            InitializeComponent();
        }

        public ObservableCollection<ApplicationViewModel> AppListViewModel
        {
            get
            {
                return (DataContext as ObservableCollection<ApplicationViewModel>);
            }
        }

        private void ApplicationAction(object sender, RoutedEventArgs e)
        {
            var action = (sender as Button).Content.ToString();
            if (action == "Delete")
            {
                var appToRemove = (sender as Button).Tag as ApplicationViewModel;
                AppListViewModel.Remove(appToRemove);
            }
            else if (action == "Add Application")
            {
                AppListViewModel.Add(new ApplicationViewModel() );
            }
            else if (action == "Build")
            {
                var appToBuild = (sender as Button).Tag as ApplicationViewModel;
            }
        }
    }

}
