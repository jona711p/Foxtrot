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
            //XmlTextReader reader = new XmlTextReader("c://skive_xml.xml");
            //while (reader.Read())
            //{
            //    switch (reader.NodeType)
            //    {
            //        case XmlNodeType.Element: // The node is an element.
            //            Console.Write("Start:  " + reader.Name);
            //            Console.WriteLine(">");
            //            break;
            //        case XmlNodeType.Text: //Display the text in each element.
            //            Console.WriteLine("Value:  " + reader.Value);
            //            break;
            //        case XmlNodeType.EndElement: //Display the end of the element.
            //            Console.Write("Slut:   " + reader.Name);
            //            Console.WriteLine(">");
            //            break;
            //    }           
            //}

            d();

            Console.ReadLine();
        }
        static void d()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"c://skive_xml.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/ArrayOfProduct/Product/Address/AddressLine1");
            List<Node> noods = new List<Node>();

            foreach (XmlNode node in nodes)
            {
                Node nodess = new Node();
                nodess.Adress = node.SelectSingleNode("Address").InnerText;
            }
        }
    }
    class Node
    {
        public string Adress;
        public string City;
        public int ID;
    }
}
