using System;
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
        /// <summary>
        /// This block of code is used for removeing the "Escape Hatch"
        /// </summary>
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public bool Accept { get; set; }

        public Declaration_of_Consent()
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;

            // Read the file, bind it to the textbox and activate the scrollbar
            FileStream fileStream = File.Open("Samtykkeerklæring.rtf", FileMode.Open);
            richTextBox_DOC_AgreementBox.Selection.Load(fileStream, System.Windows.DataFormats.Rtf);
            richTextBox_DOC_AgreementBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void Btn_DOC_Accept_OnClick(object sender, RoutedEventArgs e)
        {
            Accept = true;
            this.Hide();
        }

        private void Btn_DOC_Decline_OnClick(object sender, RoutedEventArgs e)
        {
            Accept = false;
            this.Hide();
        }

        /// <summary>
        /// This block of code is used for removeing the "Escape Hatch"
        /// </summary>
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
