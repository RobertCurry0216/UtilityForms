using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using UtilityForms.ViewModel;

namespace UtilityForms.View
{
    /// <summary>
    /// Interaction logic for UserInput.xaml
    /// </summary>
    public partial class UserInputWindow : UserControl
    {
        public Window window;
        public UserInputViewModel context;

        public UserInputWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            context.Text = null;
            window.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !context.CheckInput(e.Key);
        }
    }
}
