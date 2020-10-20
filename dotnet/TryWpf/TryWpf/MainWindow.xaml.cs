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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TryWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register(nameof(Counter), typeof(int), typeof(MainWindow));
        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();

            Counter = 0;
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (_, __) => Counter++;
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button_Click");
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Grid_Click");
        }

        private void MyWindow_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("MyWindow_Click");
        }

        private void MyCustomControl_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("MyCustomControl_Click");
        }
    }
}
