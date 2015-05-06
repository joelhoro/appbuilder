using System.Linq;
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
            BuildLogControl.SetContext(appListViewModel);
            RunLogControl.SetContext(appListViewModel);
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
                case "RefreshFileList":
                    var results = FileSystem.Initialize(AppEnvironment.Settings.Workspaces, refreshCache:true);
                    var message = results.Select(r => r.Key + ": " + r.Value + " solutions").Join("\n");
                    var title = "Found {0} files in {1} directories".With( results.Sum(r => r.Value),results.Count());
                    MessageBox.Show(message, title);
                    break;
                default:
                    break;
            }
        }

    }
}
