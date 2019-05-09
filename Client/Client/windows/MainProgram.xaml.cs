﻿using System;
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
using System.Windows.Shapes;
using Client.windows.MainProgram;

namespace Client.windows
{
    /// <summary>
    /// Interaction logic for MainProgram.xaml
    /// </summary>
    public partial class PageHolderWindow : Window
    {
        public PageHolderWindow() {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            //Main.Content = new Edit();
        }

        public void SwitchWindow(string newPage) {
            Main.Content = new Create();
        }


    }
}
