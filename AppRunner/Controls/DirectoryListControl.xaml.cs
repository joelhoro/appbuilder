﻿using AppRunner.Models;
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
    /// Interaction logic for DirectoryListControl.xaml
    /// </summary>
    public partial class DirectoryListControl : UserControl
    {
        public DirectoryListControl()
        {
            SelectionChange = (s, e) => { return; };
            InitializeComponent();
        }

        public event SelectionChangedEventHandler SelectionChange;

        public LogFileViewModel ActiveFile { get { return listbox.SelectedItem as LogFileViewModel; } }
    }
}