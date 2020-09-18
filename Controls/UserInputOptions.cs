using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using UtilityForms.ViewModel.Enums;

namespace UtilityForms.Controls
{
    public class UserInputOptions
    {
        public string Message { get; set; } = "Enter Text";
        public string DefaultText { get; set; } = "";
        public string Title { get; set; } = "Input";
        public NotificationIcon Icon { get; set; } = NotificationIcon.Question;
        public int Width { get; set; } = 360;
        public int Height { get; set; } = 190;
        internal Func<Key, bool> InputTest { get; set; } = TextAllowAll;

        internal static bool TextAllowAll(Key key) => true;

        internal static bool TextAllowIntOnly(Key key)
        {
            var re = new Regex("([0-9-])");
            Utils.KeyToChar(key, out var decode);
            var m = re.Match(decode.character.ToString()).Success;
            return m;
        }

        internal static bool TextAllowDoubleOnly(Key key)
        {
            var re = new Regex("([0-9-.])");
            Utils.KeyToChar(key, out var decode);
            var m = re.Match(decode.character.ToString()).Success;
            return m;
        }
    }
}
