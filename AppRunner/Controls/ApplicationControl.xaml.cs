using AppRunner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AppRunner.Controls
{
    /// <summary>
    /// Interaction logic for ApplicationControl.xaml
    /// </summary>
    public partial class ApplicationControl : UserControl
    {
        private string buildButtonVisible;
        public string BuildButtonVisible { get { return buildButtonVisible; } set { buildButtonVisible = value; /*NotifyPropertyChanged();*/ } }
        public ApplicationControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ApplicationViewModel).Run();
        }

        private void ApplicationAction(object sender, RoutedEventArgs e)
        {
            var action = (sender as Button).Content.ToString();
            if (action == "Build")
            {
                var appToBuild = (sender as Button).Tag as ApplicationViewModel;
                appToBuild.Build();
            }
        }

    }
}
