using AppRunner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            appcontrol1.DataContext = new ApplicationViewModel();
            var fileName = @"c:\temp\index.html";
            fileName = @"c:\temp\bootstrap.css";
            logfilecontrol1.SetContext(new LogFileViewModel(fileName));


            appcontrol2.DataContext = new ApplicationViewModel();
        }

    }
}
