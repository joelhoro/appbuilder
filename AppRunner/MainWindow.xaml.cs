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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var path = @"\Users\Joel\Desktop\tests";
            //dirlist.SetContext(new DirectoryListViewModel(path));
            //dirlist.fileGrid.SelectionChanged += (s,e) => logfilecontrol1.SetContext((s as DataGrid).SelectedItem as LogFileViewModel);
            
            var fileName = @"c:\users\joel\documents\visual studio 2013\Projects\DummyExe\DummyExe\bin\Debug\DummyExe.exe";

            LoadSettings();
            //AppEnvironment.LoadSettings();
            //logfilecontrol1.SetContext(executable);
        }

        public void LoadSettings()
        {
            AppEnvironment.LoadSettings();
            AppListControl.DataContext = AppEnvironment.Settings.Applications;
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
                        }));
                    
                    break;
                default:
                    break;
            }
        }

    }
}
