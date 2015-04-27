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
            //dirlist.SetContext(new DirectoryListModel(path));
            //dirlist.fileGrid.SelectionChanged += (s,e) => logfilecontrol1.SetContext((s as DataGrid).SelectedItem as LogFileModel);
            
            var fileName = @"c:\users\joel\documents\visual studio 2013\Projects\DummyExe\DummyExe\bin\Debug\DummyExe.exe";

            LoadSettings();
        }

        public void LoadSettings()
        {
            AppEnvironment.LoadSettings();
            var appListModel =  ApplicationListModel.Create( AppEnvironment.Settings.Applications );
            AppListControl.DataContext = appListModel;
            appListModel.ActiveApplicationChangeEvent += (sender, args) =>
            {
                var applicationModel = args as ApplicationModel;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    LogFileControl.SetContext(applicationModel);
                }));
            };
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
