using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using UtilityForms.Properties;
using UtilityForms.View;
using UtilityForms.View.Converters;

namespace UtilityForms
{
    public static class Toast
    {
        public static void IncrementToastCount() => ToastController.Instance.ToastCount++;
        public static void DecrementToastCount() => ToastController.Instance.ToastCount--;

        private static void ToastBase(string title, System.Drawing.Bitmap bitmap, Brush color, string msg = null, Action func = null)
        {
            IncrementToastCount();
            var toast = new ToastView();

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            var toastHeight = (int)(toast.Height + 10);
            var desktopHeight = (int)(desktopWorkingArea.Height / toastHeight);

            var XOffset = (ToastController.Instance.Offset / desktopHeight) + 1;
            var YOffset = XOffset > 1
                ? (ToastController.Instance.Offset % desktopHeight) + 1
                : (ToastController.Instance.Offset % desktopHeight);

            toast.Left = desktopWorkingArea.Right - XOffset * toast.Width;
            toast.Top = desktopWorkingArea.Bottom - YOffset * toastHeight;

            toast.Image.Source = BitmapSourceConverter.ConvertFromImage(bitmap);
            toast.bgColour.Background = color;
            toast.Title.Text = title;
            if (msg == null)
            {
                toast.Message.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                toast.Message.Text = msg;
            }

            if (func != null) toast.OnClickEvent += new ToastView.OnClick(func);

            toast.Show();
        }

        private const string SuccessMessage = "Success!";
        public static void Success() => ToastBase(SuccessMessage, Resources.toast_success, Brushes.SeaGreen);
        public static void Success(string message) => ToastBase(SuccessMessage, Resources.toast_success, Brushes.SeaGreen, msg: message);
        public static void Success(string title, string message) => ToastBase(title, Resources.toast_success, Brushes.SeaGreen, msg: message);
        public static void Success(Action action, string title = SuccessMessage, string message = "Click me" ) => ToastBase(title, Resources.toast_success, Brushes.SeaGreen, msg: message, func: action);

        private const string ErrorMessage = "Error!";
        public static void Error() => ToastBase(ErrorMessage, Resources.toast_error, Brushes.DarkRed);
        public static void Error(string message) => ToastBase(ErrorMessage, Resources.toast_error, Brushes.DarkRed, msg: message);
        public static void Error(string title, string message) => ToastBase(title, Resources.toast_error, Brushes.DarkRed, msg: message);
        public static void Error(Action action, string title = ErrorMessage, string message = "Click me") => ToastBase(title, Resources.toast_error, Brushes.DarkRed, msg: message, func: action);

        private const string WarningMessage = "Warning!";
        public static void Warning() => ToastBase(WarningMessage, Resources.toast_warning, Brushes.DarkOrange);
        public static void Warning(string message) => ToastBase(WarningMessage, Resources.toast_warning, Brushes.DarkOrange, msg: message);
        public static void Warning(string title, string message) => ToastBase(title, Resources.toast_warning, Brushes.DarkOrange, msg: message);
        public static void Warning(Action action, string title = WarningMessage, string message = "Click me") => ToastBase(title, Resources.toast_warning, Brushes.DarkOrange, msg: message, func: action);

        private const string InfoMessage = "Info";
        public static void Info() => ToastBase(InfoMessage, Resources.toast_info, Brushes.RoyalBlue);
        public static void Info(string message) => ToastBase(InfoMessage, Resources.toast_info, Brushes.RoyalBlue, msg: message);
        public static void Info(string title, string message) => ToastBase(title, Resources.toast_info, Brushes.RoyalBlue, msg: message);
        public static void Info(Action action, string title = InfoMessage, string message = "Click me") => ToastBase(title, Resources.toast_info, Brushes.RoyalBlue, msg: message, func: action);
    }

    internal class ToastController
    {
        private int _ToastCount = 0;
        public int ToastCount { 
            get => _ToastCount; 
            set
            {
                if (value == 0) Offset = 0;
                else if (value > _ToastCount) Offset += 1;  
                _ToastCount = value;
            }

        }
        public int Offset { get; private set; } = 0;

        private ToastController()
        {

        }

        private static ToastController _instance;
        public static ToastController Instance
        {
            get { return _instance ?? (_instance = new ToastController()); }
            set { _instance = value; }
        }
    }
}
