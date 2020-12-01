using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using DevExpress.Mvvm;

namespace TryDevExpress.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Person> Items { get; set; } = new ObservableCollection<Person>
        {
            new Person
            {
                Group = "G1",
                Class = "C1",
                Id = "001",
                Name = "N003",
            },
            new Person
            {
                Group = "G1",
                Class = "C1",
                Id = "002",
                Name = "N002",
            },
            new Person
            {
                Group = "G1",
                Class = "C1",
                Id = "003",
                Name = "N003",
            },
            new Person
            {
                Group = "G1",
                Class = "C2",
                Id = "004",
                Name = "N004",
            },
            new Person
            {
                Group = "G1",
                Class = "C2",
                Id = "005",
                Name = "N005",
            },
            new Person
            {
                Group = "G2",
                Class = "C3",
                Id = "006",
                Name = "N006",
            },
            new Person
            {
                Group = "G2",
                Class = "C3",
                Id = "006",
                Name = "N006",
            },
            new Person
            {
                Group = null,
                Class = "C1",
                Id = "007",
                Name = "N007",
            },
            new Person
            {
                Group = null,
                Class = "C1",
                Id = "008",
                Name = "N008",
            },
            new Person
            {
                Group = null,
                Class = "C2",
                Id = "009",
                Name = "N009",
            },
        };
        public IList<Person> ComboBoxItems { get; set; } = new List<Person>
        {
            new Person { Id = "001", Name = "Gregory S. Price" },
            new Person { Id = "002", Name = "Irma R. Marshall" },
            new Person { Id = "003", Name = "John C. Powell" }
        };
        public virtual ObservableCollection<string> ComboBoxItems2 { get; set; } = new ObservableCollection<string> { "Item 1", "Item 2", "Item 3" };
        public virtual string SelectedItem { get; set; } = string.Empty;

        public void OnSelectedItemChanged()
        {

        }

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();

            ProcessEmptyGroup(Items);
        }

        private void ProcessEmptyGroup(ObservableCollection<Person> items)
        {
            var classMapGroup = new Dictionary<string, string>();
            foreach (var item in items)
            {
                if (item.Group != null)
                {
                    item.GroupView = item.Group;
                    continue;
                }

                if (classMapGroup.ContainsKey(item.Class))
                {
                    item.GroupView = classMapGroup[item.Class];
                }
                else
                {
                    var group = Guid.NewGuid().ToString();
                    classMapGroup.Add(item.Class, group);
                    item.GroupView = group;
                }
            }
        }
    }

    public class Person
    {
        public string? Group { get; set; }
        public string GroupView { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
    [ValueConversion(typeof(string), typeof(string))]
    public class GroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var groupData = (DevExpress.Xpf.Grid.GridGroupValueData)value;
            return Guid.TryParse(groupData.Value as string, out var _) ? string.Empty : groupData.GroupColumnHeaderText + groupData.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "bbb";
        }
    }
}