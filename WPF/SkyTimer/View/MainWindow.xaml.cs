﻿using Microsoft.Practices.ServiceLocation;
using Octokit;
using SkyTimer.Helper;
using SkyTimer.Properties;
using SkyTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SkyTimer.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && e.IsDown && !Settings.Default.StackmatMode)
            {
                ServiceLocator.Current.GetInstance<TimerViewModel>().SpaceKeyDown();
            }
            if (e.Key == Key.X)
            {
                ServiceLocator.Current.GetInstance<RecordListViewModel>().RemoveLast();
            }
            if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                ServiceLocator.Current.GetInstance<RecordListViewModel>().PlusTwoLast();
            }
            if (e.Key == Key.D)
            {
                ServiceLocator.Current.GetInstance<RecordListViewModel>().DNFLast();
            }
            if (e.Key == Key.Right)
            {
                ServiceLocator.Current.GetInstance<RecordListViewModel>().RequestScramble();
            }
            e.Handled = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && e.IsUp && !Settings.Default.StackmatMode)
            {
                ServiceLocator.Current.GetInstance<TimerViewModel>().SpaceKeyUp();
            }
            e.Handled = true;
        }
    }
}
