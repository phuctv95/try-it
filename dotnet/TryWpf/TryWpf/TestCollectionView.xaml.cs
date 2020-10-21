using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for TestCollectionView.xaml
    /// </summary>
    public partial class TestCollectionView : Window
    {
        public ICollectionView ContactFilterView { get; set; }
        public TestCollectionView()
        {
            InitializeComponent();

            var contacts = GetContacts();
            ContactFilterView = CollectionViewSource.GetDefaultView(contacts);
            ContactFilterView.Filter = OnFilterTriggered;
            DgData.ItemsSource = ContactFilterView;
        }
        private List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact() { Age = 33, Name = "Chelsea" },
                new Contact() { Age = 30, Name = "Taylor" },
                new Contact() { Age = 35, Name = "Chris" },
                new Contact() { Age = 23, Name = "Scarlett" },
                new Contact() { Age = 42, Name = "Dwayne" },
            };
        }
        public bool OnFilterTriggered(object item)
        {
            if (item is Contact contact)
            {
                var bFrom = int.TryParse(TBFrom.Text, out int from);
                var bTo = int.TryParse(TBTo.Text, out int to);
                if (bFrom && bTo)
                    return (contact.Age >= from && contact.Age <= to);
            }
            return true;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            ContactFilterView.Refresh();
        }
    }
    public class Contact
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
