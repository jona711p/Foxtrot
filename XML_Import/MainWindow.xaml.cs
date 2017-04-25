using System;
using System.Collections.Generic;
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
using System.Xml;

namespace XML_Import
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            XmlTextReader reader = new XmlTextReader("c:\\skive_xml.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        MessageBox.Show("<" + reader.Name + ">");
                        break;
                        
                    case XmlNodeType.Text: //Display the text in each element.
                        MessageBox.Show("Værdi: " + reader.Value);
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element.                    
                        MessageBox.Show("</" + reader.Name + ">");
                        break;

                }

                string[] element = new string[1000];
                string[] value = new string[1000];


                //https://support.microsoft.com/da-dk/help/307548/how-to-read-xml-from-a-file-by-using-visual-c
            }
        }
    }
}
