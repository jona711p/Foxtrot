using System;
using System.Windows;
using Microsoft.Win32;

namespace Foxtrot.Classes.XMLImport.GUI
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public partial class XML_Import : Window
    {
        private string FileName, ShortFileName;

        public XML_Import()
        {
            ResizeMode = ResizeMode.NoResize; //locks the window to its inital size (600x300) and disables the ability to minimize

            InitializeComponent();
        }

        private void btn_Open_XML_File_Click(object sender, RoutedEventArgs e)
        {
            OpenXMLFile();
        }

        private void OpenXMLFile()
        {
            FileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Dokument (.xml)|*.xml";
            Nullable<bool> gotFile = openFileDialog.ShowDialog();
            openFileDialog.InitialDirectory = ".";
            if (gotFile == true)
            {
                FileName = openFileDialog.FileName;
                char[] param = { '\\' };
                string[] tempArray = FileName.Split(param);
                ShortFileName = tempArray[tempArray.Length - 1];
            }
        }
    }
}
