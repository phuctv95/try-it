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
    /// Interaction logic for TestValueConverter.xaml
    /// </summary>
    public partial class TestValueConverter : Window
    {
        public TestValueConverter()
        {
            InitializeComponent();
        }
    }

    [ValueConversion(typeof(string), typeof(bool))]
    public class YesNoToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return (value.ToString().ToLower()) switch
            {
                "yes" => true,
                "no" => false,
                _ => false,
            };
        }

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return value switch
            {
                bool valueBool => valueBool == true ? "yes" : "no",
                _ => "no",
            };
        }
	}
}
