using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using UtilityForms.ViewModel.Enums;

namespace UtilityForms.View.Converters
{
    public class NotificationIconToImageSourceConverter : IValueConverter
    {
        private static readonly Lazy<BitmapToImageSourceConverter> InstanceObj =
            new Lazy<BitmapToImageSourceConverter>(() => new BitmapToImageSourceConverter());

        public static BitmapToImageSourceConverter Instance
        {
            get { return InstanceObj.Value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is NotificationIcon)) return null;

            var icon = (NotificationIcon)value;
            Bitmap bmp = null;

            switch (icon)
            {
                case NotificationIcon.None:
                    break;
                case NotificationIcon.Checked:
                    bmp = Properties.Resources.Checked;
                    break;
                case NotificationIcon.Cancel:
                    bmp = Properties.Resources.Cancel;
                    break;
                case NotificationIcon.Error:
                    bmp = Properties.Resources.Error;
                    break;
                case NotificationIcon.Hazzard:
                    bmp = Properties.Resources.Hazzard;
                    break;
                case NotificationIcon.Important:
                    bmp = Properties.Resources.Important;
                    break;
                case NotificationIcon.NoEntry:
                    bmp = Properties.Resources.NoEntry;
                    break;
                case NotificationIcon.Poison:
                    bmp = Properties.Resources.Poison;
                    break;
                case NotificationIcon.Stop:
                    bmp = Properties.Resources.Stop;
                    break;
                case NotificationIcon.Unavailable:
                    bmp = Properties.Resources.Unavailable;
                    break;
                case NotificationIcon.Info:
                    bmp = Properties.Resources.Info;
                    break;
                case NotificationIcon.Question:
                    bmp = Properties.Resources.Question;
                    break;
                case NotificationIcon.Warning:
                    bmp = Properties.Resources.Warning;
                    break;
                default:
                    break;
            }

            return bmp == null ? null : BitmapSourceConverter.ConvertFromImage(bmp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
