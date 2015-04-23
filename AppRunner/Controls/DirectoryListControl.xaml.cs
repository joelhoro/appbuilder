using AppRunner.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for DirectoryListControl.xaml
    /// </summary>
    public partial class DirectoryListControl : UserControl
    {
        public DirectoryListControl()
        {
            InitializeComponent();
        }

        public LogFileViewModel ActiveFile { get { return fileGrid.SelectedItem as LogFileViewModel; } }

        public DirectoryListViewModel ViewModel { get { return DataContext as DirectoryListViewModel; } }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fileGrid.DataContext = ViewModel.Files;
        }

        internal void SetContext(DirectoryListViewModel directoryListViewModel)
        {
            DataContext = directoryListViewModel;
            fileGrid.DataContext = ViewModel.Files;
        }
    }
}
