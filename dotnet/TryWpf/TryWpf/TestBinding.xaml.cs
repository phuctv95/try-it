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
    /// Interaction logic for TestBinding.xaml
    /// </summary>
    public partial class TestBinding : Window
    {
        public Person Person = new Person { Name = "Leo" };
        public TestBinding()
        {
            InitializeComponent();
            DataContext = Person;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Person.Name);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
