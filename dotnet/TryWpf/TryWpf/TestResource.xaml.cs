using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TryWpf
{
    /// <summary>
    /// Interaction logic for TestResource.xaml
    /// </summary>
    public partial class TestResource : Window
    {
        public TestResource()
        {
            InitializeComponent();
        }

        private void changeResourceButton_Click(object sender, RoutedEventArgs e)
        {
            //(Resources["brushResource"] as SolidColorBrush).Color = Colors.BlueViolet;
            Resources["brushResource"] = new SolidColorBrush(Colors.BlueViolet);
        }
    }
}
