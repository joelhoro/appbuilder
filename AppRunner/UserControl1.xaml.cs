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
    public class MyViewModel : INotifyPropertyChanged
    {
        public MyViewModel(string text)
        {
            ButtonText = text;
        }
        private string t;

        public string ButtonText { get; set; }
        private int i = 0;
        public void Update()
        {
            ButtonText = "Text #" + i.ToString();
            NotifyPropertyChanged("ButtonText");
            i++;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1() {
            InitializeComponent();
        }
        public UserControl1(string text)
        {
            InitializeComponent();
            DataContext = new MyViewModel(text);
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MyViewModel).Update();
        }
    }
}
