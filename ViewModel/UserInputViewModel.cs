using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityForms.ViewModel.Enums;

namespace UtilityForms.ViewModel
{
    public class UserInputViewModel : Observable
    {

        private string _Message;
        public string Message
        {
            get => _Message;
            set
            {
                if (value != _Message)
                {
                    _Message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        private string _Text;
        public string Text
        {
            get => _Text;
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        private NotificationIcon _Icon;
        public NotificationIcon Icon
        {
            get => _Icon;
            set
            {
                if (value != _Icon)
                {
                    _Icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public Func<Key, bool> InputTest;

        public bool CheckInput(Key key) => InputTest(key);

    }
}
