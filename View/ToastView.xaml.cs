using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UtilityForms.Properties;

namespace UtilityForms.View
{
    /// <summary>
    /// Interaction logic for ToastView.xaml
    /// </summary>
    /// 
    public partial class ToastView : Window
    {
        private enum Mode
        {
            Display,
            Close,
        }

        DispatcherTimer dispatchTimer;
        int DisplayTime;
        Mode mode;
        public Bitmap ToastIcon = Properties.Resources.toast_success;
        public delegate void OnClick();
        public event OnClick OnClickEvent;

        public ToastView()
        {
            InitializeComponent();
            DisplayTime = 5;
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += DispatchTimer_Tick;
            dispatchTimer.Interval = new TimeSpan(0, 0, 1);
            dispatchTimer.Start();
            mode = Mode.Display;
            DisplayTime = 3;
        }

        private void Exit()
        {
            dispatchTimer.Stop();
            dispatchTimer = null;
            Toast.DecrementToastCount();
            Close();
        }

        private void DispatchTimer_Tick(object sender, EventArgs e)
        {
            switch (mode)
            {
                case Mode.Display:
                    if (DisplayTime <= 0)
                    {
                        dispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                        mode = Mode.Close;
                    }
                    DisplayTime -= 1;
                    break;
                case Mode.Close:
                    Opacity -= 0.1;
                    Left += 7;
                    if (Opacity <= 0)
                    {
                        Exit();
                    }
                    break;
                default:
                    break;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Exit();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            dispatchTimer?.Stop();
            mode = Mode.Display;
            Opacity = 1;
            DisplayTime = 2;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            dispatchTimer?.Start();
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OnClickEvent?.Invoke();
        }
    }
}
