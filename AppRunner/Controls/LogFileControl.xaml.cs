using AppRunner.Models;
using AppRunner.Utilities;
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
using System.Windows.Threading;

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

        DispatcherTimer dispatcherTimer;

        internal void SetContext(LogFileViewModel logFileViewModel)
        {

            DataContext = logFileViewModel;
            //logFileViewModel.PropertyChanged += (s, e) =>
            //{
            //    if (e.PropertyName == "FileContent")
            //        TextBox1.ScrollToEnd();
            //};

            if (dispatcherTimer != null) dispatcherTimer.Stop();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((s, e) => { if(logFileViewModel != null) logFileViewModel.UpdateContent(); });
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        internal void SetContext(Executable solution)
        {
            DataContext = solution;
            DataReceivedEventHandler update = (s, e) =>
                Dispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        TextBox1.Text += e.Data + "\n";
                        TextBox1.ScrollToEnd();
                    }));
                
            solution.OutputDataReceived += update;
        }
    }
}
