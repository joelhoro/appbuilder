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
            LoadSettings();
        }

        public void LoadSettings()
        {
            AppEnvironment.LoadSettings();
            var appListViewModel =  ApplicationListVM.Create(AppEnvironment.Settings.Applications);
            AppListControl.DataContext = appListViewModel;
            LogFileControl.SetContext(appListViewModel);
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
