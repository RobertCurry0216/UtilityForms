using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace UtilityForms.View
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow
    {
        public NotificationWindow(TimeSpan showDuration)
        {
            InitializeComponent();
            DissappearAnimationTime.KeyTime = KeyTime.FromTimeSpan(showDuration.Add(TimeSpan.FromSeconds(0.5)));

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var presentationSource = PresentationSource.FromVisual(this);
                if (presentationSource == null) return;
                var target = presentationSource.CompositionTarget;
                if (target == null) return;

                var transform = target.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                Left = corner.X - ActualWidth - 100;
                Top = corner.Y - ActualHeight;
            }));
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            Close();
        }
    }
}
