using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using UtilityForms.ViewModel.Enums;

namespace UtilityForms.View.Converters
{
    public class NotificationIconToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is NotificationIcon))
                return Visibility.Collapsed;

            var icon = (NotificationIcon)value;
            return icon != NotificationIcon.None ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}