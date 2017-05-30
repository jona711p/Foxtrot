using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Foxtrot.GUI.About
{
    /// <summary>
    /// Thomas Nielsen
    /// </summary>
    public partial class Declaration_of_Consent : Window
    {
        public bool accept; //kan måske lave fejl hvis man skal bruge den flere gange
        public Declaration_of_Consent() //accept skal sættes til false hvis vinduet lukkes oppe i hjørnet
        {
            InitializeComponent();
            // Read the file and bind it to textbox and activate the scrollbar
            FileStream fileStream = File.Open("Samtykkeerklæring.rtf", FileMode.Open);
            richTextBox_DOC_AgreementBox.Selection.Load(fileStream, System.Windows.DataFormats.Rtf);
            richTextBox_DOC_AgreementBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            //this.OnClosing();
        }

        private void Btn_DOC_Accept_OnClick(object sender, RoutedEventArgs e)
        {
            accept = true;
            this.Hide();
        }

        private void Btn_DOC_Decline_OnClick(object sender, RoutedEventArgs e)
        {
            accept = false;
            this.Hide();
        }
        private void OnClosing() //accept skal sættes til false hvis vinduet lukkes oppe i hjørnet
        {
            accept = false;
            System.Windows.MessageBox.Show("heste hest: " + accept);
        }
    }
}
