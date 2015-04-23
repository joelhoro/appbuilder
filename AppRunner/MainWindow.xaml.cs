using AppRunner.Controls;
using AppRunner.Models;
using System;
using System.Collections.Generic;
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
            dirlist.SetContext(new DirectoryListViewModel(path));
            dirlist.fileGrid.SelectionChanged += (s,e) => logfilecontrol1.SetContext((s as DataGrid).SelectedItem as LogFileViewModel);
        }
    }
}
