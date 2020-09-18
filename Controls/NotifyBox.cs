using System;
using System.Threading;
using System.Windows.Threading;
using UtilityForms.View;
using UtilityForms.ViewModel;
using UtilityForms.ViewModel.Enums;

namespace UtilityForms.Controls
{
    public static class NotifyBox
    {
        public static void Show(string title, string text, NotificationIcon notificationIcon, TimeSpan showDuration)
        {
            var model = new NotificationViewModel
                {
                    Icon = notificationIcon,
                    Text = text,
                    Title = title
                };

            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                var windowThread = new Thread(() =>
                {
                    SynchronizationContext
                        .SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
                    var window = new NotificationWindow(showDuration) { DataContext = model, ShowActivated = false };
                    window.Closed += (s, e) =>
                                     Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                    window.Show();
                    Dispatcher.Run();
                });
                windowThread.SetApartmentState(ApartmentState.STA);
                windowThread.Priority = ThreadPriority.BelowNormal;
                windowThread.Name = string.Format("Notifybox \"{0}\" thread", title);
                windowThread.IsBackground = true;
                windowThread.Start();
            }
            else
            {
                var window = new NotificationWindow(showDuration) { DataContext = model, ShowActivated = false };
                window.Show();
            }
        }

        public static void Show(string title, string text, NotificationIcon notificationIcon)
        {
            Show(title, text, notificationIcon, new TimeSpan(0, 0, 2));
        }

        public static void Show(string title, string text, TimeSpan showDuration)
        {
            Show(title, text, NotificationIcon.None, showDuration);
        }

        public static void Show(string title, string text)
        {
            Show(title, text, NotificationIcon.None);
        }

        public static void Show(string title, string text, NotificationIcon notificationIcon, NotificationDuration duration)
        {
            Show(title, text, notificationIcon, GetTimeFromDurationEnumerable(duration));
        }

        public static void Show(string title, string text, NotificationDuration duration)
        {
            Show(title, text, GetTimeFromDurationEnumerable(duration));
        }

        private static TimeSpan GetTimeFromDurationEnumerable(NotificationDuration duration)
        {
            switch (duration)
            {
                case NotificationDuration.VeryShort:
                    return TimeSpan.FromSeconds(2);
                case NotificationDuration.Short:
                    return TimeSpan.FromSeconds(4);
                case NotificationDuration.Medium:
                    return TimeSpan.FromSeconds(8);
                case NotificationDuration.Long:
                    return TimeSpan.FromSeconds(10);
                case NotificationDuration.VeryLong:
                    return TimeSpan.FromSeconds(12);
                case NotificationDuration.ExtraLong:
                    return TimeSpan.FromSeconds(15);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}