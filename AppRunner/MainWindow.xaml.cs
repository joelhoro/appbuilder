using AppRunner.Controls;
using AppRunner.Models;
using AppRunner.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace AppRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var path = @"\Users\Joel\Desktop\tests";
            //dirlist.SetContext(new DirectoryListViewModel(path));
            //dirlist.fileGrid.SelectionChanged += (s,e) => logfilecontrol1.SetContext((s as DataGrid).SelectedItem as LogFileViewModel);
            
            var fileName = @"c:\users\joel\documents\visual studio 2013\Projects\DummyExe\DummyExe\bin\Debug\DummyExe.exe";

            //AppEnvironment.LoadSettings();
            ApplicationListBox.DataContext = AppEnvironment.Settings.Applications;
            //logfilecontrol1.SetContext(executable);
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as MenuItem).Tag.ToString();
            switch (tag)
            {
                case "Save":
                    AppEnvironment.SaveSettings();
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ApplicationRemove(object sender, RoutedEventArgs e)
        {
        }

        private void ApplicationAction(object sender, RoutedEventArgs e)
        {
            var action = (sender as Button).Content.ToString();
            var apps = (ApplicationListBox.DataContext as ObservableCollection<ApplicationViewModel>);
            if (action == "Delete")
            {
                var appToRemove = (sender as Button).Tag as ApplicationViewModel;
                apps.Remove(appToRemove);
            }
            else if (action == "Add Application")
            {
                apps.Add(new ApplicationViewModel());
            }
            else if (action == "Build")
            {
                var appToBuild = (sender as Button).Tag as ApplicationViewModel;
            }
        }
    }
}
