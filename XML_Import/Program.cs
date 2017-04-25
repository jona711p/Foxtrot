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
            Xmlimport import = new Xmlimport();

            XmlNodeList Product = xmlDoc.GetElementsByTagName("Product");
            XmlNodeList Adress = xmlDoc.GetElementsByTagName("AddressLine1");
            XmlNodeList City = xmlDoc.GetElementsByTagName("City");
            XmlNodeList Latitude = xmlDoc.GetElementsByTagName("Latitude");
            XmlNodeList Longitude = xmlDoc.GetElementsByTagName("Longitude");
            XmlNodeList Id = xmlDoc.GetElementsByTagName("Id");
            XmlNodeList Name = xmlDoc.GetElementsByTagName("Name");
            XmlNodeList PostalCode = xmlDoc.GetElementsByTagName("PostalCode");


            import.Product = Product.Count.ToString();
            import.Adress = Adress[0].InnerText.ToString();
            import.City = City[0].InnerText.ToString();
            import.Latitude = Latitude[0].InnerText.ToString();
            import.Longitude = Longitude[0].InnerText.ToString();
            import.Id = Id[0].InnerText.ToString();
            import.Name = Name[0].InnerText.ToString();
            import.PostalCode = PostalCode[0].InnerText.ToString();

            Console.WriteLine(import.Product);
            Console.WriteLine(import.Adress);
            Console.WriteLine(import.City);
            Console.WriteLine(import.Latitude);

            for (int i = 0; i < Adress.Count; i++)
            {
                Console.WriteLine(Adress[i].InnerXml);
            }
            Console.ReadLine();
        }
    }
}
