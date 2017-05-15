using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;

namespace Foxtrot.Classes.XMLImport.GUI
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public partial class XML_Import : Window, INotifyPropertyChanged
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; NotifyPropertyChanged(); }
        }

        public string FullPathAndFileName { get; private set; }

        public XML_Import()
        {
            ResizeMode = ResizeMode.NoResize; //locks the window to its inital size (600x300) and disables the ability to minimize

            InitializeComponent();

            DataContext = this;
        }

        private void btn_Open_XML_File_Click(object sender, RoutedEventArgs e)
        {
            OpenXMLFile();
        }

        private void OpenXMLFile()
        {
            FileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Dokument (.xml)|*.xml";

            bool? result = openFileDialog.ShowDialog();
            openFileDialog.InitialDirectory = ".";

            if (result == true)
            {
                FullPathAndFileName = openFileDialog.FileName;
                char[] param = { '\\' };
                string[] tempArray = FullPathAndFileName.Split(param);
                FileName = tempArray[tempArray.Length - 1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
