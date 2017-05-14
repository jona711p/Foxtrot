using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Classes
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    class GUISortingLogic
    {
        public static void Message(string inputMessage)
        {
            MessageBox.Show(inputMessage);
        }

        public static string Name(TextBox inputName)
        {
            if (inputName.Text.Length != 0)
            {
                return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(inputName.Text); // Rewrites the text with UPPER CASE first letter
            }

            return null;
        }

        public static int? Number(TextBox inputNumber)
        {
            int tempInt;

            if (int.TryParse(inputNumber.Text, out tempInt) && inputNumber.Text.Length == 8)
            {
                return tempInt;
            }
            
            return null;
        }

        public static string Email(TextBox inputEmail)
        {
            if (inputEmail.Text.Length != 0 && inputEmail.Text.Contains("@"))
            {
                return inputEmail.Text.ToLower();
            }

            return null;
        }
    }
}
