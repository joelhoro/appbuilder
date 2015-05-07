using AppRunner.Models;
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
using AppRunner.Utilities;

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
            (DataContext as ApplicationVM).Run();
        }

        private void ApplicationAction(object sender, RoutedEventArgs evt)
        {
            var action = (sender as Button).Content.ToString();
            var app = (sender as Button).Tag as ApplicationVM;
            if (action == "Build")
            {
                app.Build();
                StartTimer(app,app.SolutionObj as Executable);
            }
            else if (action == "Abort")
            {
                app.SolutionObj.Destroy();
                app.Status = ApplicationStatus.Aborted;
            }
            else if (action == "Run")
            {
                app.Run();
                StartTimer(app,app.ExecutableObj);
            }
            else if (action == "Build & Run")
            {
                app.BuildAndRun();
                StartTimer(app, app.SolutionObj as Executable);
            }
        }

        private void StartTimer(ApplicationVM app, Executable executable)
        {
            var startTime = DateTime.Now;
            DataReceivedEventHandler outputChangedDelegate = (s, e) => Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    var minutes = (DateTime.Now - startTime).Minutes;
                    var seconds = (DateTime.Now - startTime).Seconds;
                    TimeElapsedLabel.Background = Brushes.CadetBlue;
                    TimeElapsedLabel.Content = "{0}:{1:d2}".With(minutes, seconds);
                })
                );

            executable.AddOutputHandler(outputChangedDelegate);
            executable.ExecutionCompleted +=
                (s, e) => Dispatcher.BeginInvoke(new Action(() => { TimeElapsedLabel.Background = Brushes.CornflowerBlue; }));
        }

        private void EnterExecutableChoices(object sender, MouseEventArgs e)
        {
            var app = (sender as ComboBox).Tag as ApplicationVM;
            app.RefreshDropDowns();

        }
    }
}
