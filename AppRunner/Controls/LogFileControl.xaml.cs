using AppRunner.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ApplicationControl.xaml
    /// </summary>
    public partial class LogFileControl : UserControl
    {
        public LogFileControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ApplicationViewModel).Run();
        }

        internal void ScrollToEnd()
        {
            TextBox1.ScrollToEnd();
        }

        internal void SetContext(LogFileViewModel logFileViewModel)
        {
            DataContext = logFileViewModel;
            logFileViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FileContent")
                    TextBox1.ScrollToEnd();
            };
        }
    }
}
