using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ListProject.ViewModel.Converters
{
    public class VisibilityToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return boolean ? Visibility.Visible : Visibility.Hidden;
            }
            return Visibility.Hidden;
        }
    }
}