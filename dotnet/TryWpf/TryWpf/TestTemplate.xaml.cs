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
    /// Interaction logic for TestTemplate.xaml
    /// </summary>
    public partial class TestTemplate : Window
    {
        public TestTemplate()
        {
            InitializeComponent();

            listBox.ItemsSource = new List<Book>
            {
                new Book { Title = "Title 1", Author = "Author 1" },
                new Book { Title = "Title 2", Author = "Author 2" },
                new Book { Title = "Title 3", Author = "Author 3" },
            };
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
