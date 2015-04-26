using AppRunner.Controls;
using AppRunner.Models;
using AppRunner.Utilities;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace AppRunner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //var path = @"\Users\Joel\Desktop\tests";
            //dirlist.SetContext(new DirectoryListViewModel(path));
            //dirlist.fileGrid.SelectionChanged += (s,e) => logfilecontrol1.SetContext((s as DataGrid).SelectedItem as LogFileViewModel);
            
            var fileName = @"c:\users\joel\documents\visual studio 2013\Projects\DummyExe\DummyExe\bin\Debug\DummyExe.exe";

            LoadSettings();
        }

        public void LoadSettings()
        {
            AppEnvironment.LoadSettings();
            var appListViewModel = new ApplicationListViewModel { ApplicationList = AppEnvironment.Settings.Applications };
            AppListControl.DataContext = appListViewModel;
            //appListViewModel.ActiveApplication.PropertyChanged += (s, e) =>
            //{
            //    LogFileControl.Content = appListViewModel.ActiveApplication.BuildOutput;
            //};
        }

        private void MenuItemClick(object sender, RoutedEventArgs evt)
        {
            var tag = (sender as MenuItem).Tag.ToString();
            switch (tag)
            {
                case "Save":
                    AppEnvironment.SaveSettings();
                    break;
                case "Restore":
                    LoadSettings();
                    break;
                case "Edit":
                    var process = Process.Start("notepad.exe", AppEnvironment.SettingsFileName );
                    Dispatcher.BeginInvoke(
                        new Action(() =>
                            {
                                process.WaitForExit(); 
                                LoadSettings(); 
                            })
                        );
                    
                    break;
                default:
                    break;
            }
        }

    }
}
