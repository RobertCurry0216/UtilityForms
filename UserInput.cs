using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityForms.View;
using UtilityForms.ViewModel;
using UtilityForms.ViewModel.Enums;

namespace UtilityForms
{
    public static class UserInput
    {
        private static string AskForInput(UserInputOptions options)
        {
            var viewModel = new UserInputViewModel()
            {
                Message = options.Message,
                Text = options.DefaultText,
                Icon = options.Icon,
                InputTest = options.InputTest
            };
            var window = new System.Windows.Window();
            var control = new UserInputWindow()
            {
                window = window,
                context = viewModel,
                DataContext = viewModel,
            };

            window.Content = control;
            window.Width = options.Width;
            window.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;
            window.Height = options.Height;
            window.Title = options.Title;
            window.ResizeMode = System.Windows.ResizeMode.NoResize;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            window.ShowDialog();

            return viewModel.Text;
        }

        public static string AskForString(UserInputOptions options) => AskForInput(options);
        public static string AskForString() => AskForInput(new UserInputOptions());

        public static int AskForInt(UserInputOptions options, int defaultOutput = 0)
        {
            options.InputTest = UserInputOptions.TextAllowIntOnly;
            var output = AskForInput(options);
            var returnValue = int.TryParse(output, out var number) ? number : defaultOutput;
            return returnValue;
        }
        public static int AskForInt() => AskForInt(new UserInputOptions() { 
            Message = "Please enter a number:",
        });

        public static double AskForDouble(UserInputOptions options, double defaultOutput = double.NaN)
        {
            options.InputTest = UserInputOptions.TextAllowDoubleOnly;
            var output = AskForInput(options);
            var returnValue = double.TryParse(output, out var number) ? number : defaultOutput;
            return returnValue;
        }
        public static double AskForDouble() => AskForDouble(new UserInputOptions()
        {
            Message = "Please enter a number:",
        });
    }
}
