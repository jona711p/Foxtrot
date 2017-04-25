using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XML_Import
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.Load(@"c://skive_xml.xml"); // Load the XML document from the specified file

            // Get elements
            xmlimport import = new xmlimport();

            XmlNodeList Adress2 = xmlDoc.GetElementsByTagName("AddressLine1");
            for (int i = 0; i < Adress2.Count; i++)
            {
                Console.WriteLine(Adress2[i].InnerXml);
            }
            Console.ReadLine();
        }
     }
    class xmlimport
    {
        private string adress;

        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }

    }
}
