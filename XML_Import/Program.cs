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

            XmlNodeList Product = xmlDoc.GetElementsByTagName("Product");
            int antal = Product.Count;
            // Get elements
            Xmlimport[] import = new Xmlimport[antal];

            XmlNodeList Adress = xmlDoc.GetElementsByTagName("AddressLine1");
            XmlNodeList City = xmlDoc.GetElementsByTagName("City");
            XmlNodeList Latitude = xmlDoc.GetElementsByTagName("Latitude");
            XmlNodeList Longitude = xmlDoc.GetElementsByTagName("Longitude");
            XmlNodeList Id = xmlDoc.GetElementsByTagName("Id");
            //XmlNodeList Name = xmlDoc.GetElementsByTagName("/Category/Name"); // forkert sti
            XmlNodeList PostalCode = xmlDoc.GetElementsByTagName("PostalCode");
            XmlNodeList Region = xmlDoc.GetElementsByTagName("Region");

            
            for (int i = 0; i < antal; i++)
            {
                import[i] = new Xmlimport();
                import[i].Adress = Adress[i].InnerText.ToString();
                import[i].City = City[i].InnerText.ToString();
                import[i].Latitude = Latitude[i].InnerText.ToString();
                import[i].Longitude = Longitude[i].InnerText.ToString();
                import[i].Id = Id[i].InnerText.ToString();
                //import[i].Name = Name[i].InnerText.ToString();
                import[i].PostalCode = PostalCode[i].InnerText.ToString();
                import[i].Region = Region[i].InnerText.ToString();

            }
            //for (int i = 0; i < antal; i++)  // Test purposes
            //{
            //    Console.WriteLine("Adress:  " + import[i].Adress);
            //    Console.WriteLine("City:  " +import[i].City);
            //    Console.WriteLine("Latitude:  " +import[i].Latitude);
            //    Console.WriteLine("Longtitude:  " +import[i].Longitude);
            //    Console.WriteLine("Id:  " +import[i].Id);
            //    //Console.WriteLine("Name:  " + import[i].Name);
            //    Console.WriteLine("PostalCode:  " +import[i].PostalCode);
            //    Console.WriteLine("Region:  " +import[i].Region);
            //    Console.WriteLine("");

            //}
            Console.ReadLine();
        }
    }
}
