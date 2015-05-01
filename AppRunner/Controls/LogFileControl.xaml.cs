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
            (DataContext as ApplicationVM).Run();
        }

        
        internal void ScrollToEnd()
        {
            MainTextBox.ScrollToEnd();
        }

        private DispatcherTimer _dispatcherTimer;

        private Dictionary<ApplicationStatus, Tuple<Brush, Brush>>  ColorDictionary = new Dictionary
            <ApplicationStatus, Tuple<Brush, Brush>>()
        {
            { ApplicationStatus.BuildFailed,     Tuple.Create<Brush,Brush>(Brushes.Coral, Brushes.White) },
            { ApplicationStatus.Running,         Tuple.Create<Brush,Brush>(Brushes.CornflowerBlue, Brushes.White) },
            { ApplicationStatus.BuildCompleted,  Tuple.Create<Brush,Brush>(Brushes.GreenYellow, Brushes.DarkBlue) },
            { ApplicationStatus.Building,        Tuple.Create<Brush,Brush>(Brushes.DarkGray,Brushes.Black) }
        };

    public void SetContext(ApplicationListVM applicationList)
        {
            DataContext = applicationList;
            if (_dispatcherTimer != null) _dispatcherTimer.Stop();

            // this is a very inefficient way to poll for changes, we should
            // bind to the OutputDataReceived directly...
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler((s, e) =>
            {
                var colors = Tuple.Create<Brush,Brush>(Brushes.CornflowerBlue,Brushes.White);
                if (applicationList.ActiveApplication != null)
                {
                    var app = applicationList.ActiveApplication;
                    if (ColorDictionary.ContainsKey(app.Status))
                        colors = ColorDictionary[app.Status];

                    MainTextBox.Text = applicationList.ActiveApplication.Output;
                    MainTextBox.ScrollToEnd();

                    MainTextBox.Background = colors.Item1;
                    MainTextBox.Foreground = colors.Item2;
                }
            });
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            _dispatcherTimer.Start();

            //application.SolutionObj.ExecutionCompleted += (s,e) =>
            //    Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        MainTextBox.Background = Brushes.Beige;
            //        MainTextBox.Foreground = Brushes.Black;
            //        if(_dispatcherTimer != null)
            //            _dispatcherTimer.Stop();
            //        _dispatcherTimer = null;
            //    }));
        }
    }
}
