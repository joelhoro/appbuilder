using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ShadowLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Debugger.Launch();
            InitializeComponent();
            var args = Environment.GetCommandLineArgs();
            var solutionName = args[1];
            var binaryDirectory = args[2];

            DataContext = new ShadowCopierModel(solutionName, binaryDirectory);
        }

        private void RunAndBuild(object sender, RoutedEventArgs e)
        {
            var shadowCopier = DataContext as ShadowCopierModel;
            shadowCopier.BuildAndrun();
            shadowCopier.build.OutputDataReceived += (o, args) => Dispatcher.BeginInvoke(new Action(
                    () => LogFileControl.Text = shadowCopier.build.Output
                ) );

        }
    }
}
