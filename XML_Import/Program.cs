using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XML_Import
{
    /// <summary>
    /// Skrevet af Jonas, Mikael & Thomas
    /// </summary>
    class Program
    {
        private static XDocument xmlDocument;
        static void Main(string[] args)
        {
            //ReadCitysFromXML();
            //ReadCategorysFromXML();
            ReadAllFromXML();
            Console.ReadLine();
        }

        public static void ReadCitysFromXML()
        {
            xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(y => new City()
            {
                ID = int.Parse(y.XPathSelectElement("./*[name()='Id']").Value),
                Name = y.XPathSelectElement("./*[name()='Name']").Value,
                PostalCode = int.Parse(y.XPathSelectElement("../*[name()='PostalCode']").Value)
            }).Distinct().OrderBy(y => y.ID).ToList();
        }

        public static void ReadCategorysFromXML()
        {
            XDocument xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(y => new Category()
            {
                ID = int.Parse(y.XPathSelectElement("./*[name()='Id']").Value),
                Name = y.XPathSelectElement("./*[name()='Name']").Value
            }).Distinct().OrderBy(y => y.ID).ToList();
        }

        public static void ReadAllFromXML()
        {
            XDocument xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");
            List<Product> products = xmlDocument.XPathSelectElements("//*[name()='Product']").Select(x => new Product()
            {
                ID = int.Parse(x.XPathSelectElement("./*[name()='Id']").Value),
                Name = x.XPathSelectElement("./*[name()='Name']").Value,
                Actor = x.XPathSelectElement("./*[name()='Name']").Value,
                Address = x.XPathSelectElement(".//*[name()='AddressLine1']").Value,
                Latitude = TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Latitude']")),
                Longitude = TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Longitude']")),
                Phone = TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Phone']")),
                Email = x.XPathSelectElement(".//*[name()='Email']").Value,
                Fax = TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Fax']")),
                CanonicalUrl = x.XPathSelectElement(".//*[name()='CanonicalUrl']").Value,
                Url = x.XPathSelectElement(".//*[name()='Url']").Value,
                //Created = DateTime.Parse(x.XPathSelectElement(".//*[name()='Created']").Value),
                //Description = x.XPathSelectElement(".//*[name()='Text']").Value,


                //Cities = x.XPathSelectElements(".//*[name()='Municipality']").Select(y => new City()
                //{
                //    ID = int.Parse(y.XPathSelectElement("./*[name()='Id']").Value)
                //}).FirstOrDefault(),

                //Categories = x.XPathSelectElements("//*[name()='Category']").Select(y => new Category()
                //{
                //    ID = int.Parse(y.XPathSelectElement("./*[name()='Id']").Value)
                //}).ToList(),

                //Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new File()
                //{
                //    ID = int.Parse(y.XPathSelectElement("./*[name()='Id']").Value),
                //    Uri = y.XPathSelectElement("./*[name()='Uri']").Value
                //}).OrderBy(y => y.ID).ToList()
            }).ToList();
        }

        public static float? TryToConvertNodeValueToFloat(XElement node)
        {
            return node == null || node.Value.Equals("") ? null : (float?)float.Parse(node.Value.Replace('.', ','));
        }

        public static List<int?> TryToConvertNodeValueToIntList(XElement node)
        {
            List<int?> output = new List<int?>();
            if (node == null || node.Value.Equals(""))
            {
                return null;
            }

            else
            {
                string[] moreNumbers = node.Value.Split('/');
                foreach (string number in moreNumbers)
                {
                    if (!number.Equals("Fur Fossiler 55.000."))
                    {
                        output.Add(int.Parse(number.Replace(" ", "").Replace("+45", "")));
                    }
                }
            }
            return output;
        }

        //public void orgKode()
        //{
        //    XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
        //    xmlDoc.Load(@"c://Temp//skive_xml.xml"); // Load the XML document from the specified file

        //    XmlNodeList Product = xmlDoc.GetElementsByTagName("Product");
        //    int Count = Product.Count;
        //    // Get elements
        //    Xmlimport[] import = new Xmlimport[Count];

        //    XmlNodeList Adress = xmlDoc.GetElementsByTagName("AddressLine1");
        //    XmlNodeList City = xmlDoc.GetElementsByTagName("City");
        //    XmlNodeList Latitude = xmlDoc.GetElementsByTagName("Latitude");
        //    XmlNodeList Longitude = xmlDoc.GetElementsByTagName("Longitude");
        //    XmlNodeList Id = xmlDoc.GetElementsByTagName("Id");
        //    //XmlNodeList Name = xmlDoc.GetElementsByTagName("/Category/Name"); // forkert sti
        //    XmlNodeList PostalCode = xmlDoc.GetElementsByTagName("PostalCode");
        //    XmlNodeList Region = xmlDoc.GetElementsByTagName("Region");

        //    for (int i = 0; i < Count; i++)
        //    {
        //        import[i] = new Xmlimport();
        //        import[i].Adress = Adress[i].InnerText.ToString();
        //        import[i].City = City[i].InnerText.ToString();
        //        import[i].Latitude = Latitude[i].InnerText.ToString();
        //        import[i].Longitude = Longitude[i].InnerText.ToString();
        //        import[i].Id = Id[i].InnerText.ToString();
        //        //import[i].Name = Name[i].InnerText.ToString();
        //        import[i].PostalCode = PostalCode[i].InnerText.ToString();
        //        import[i].Region = Region[i].InnerText.ToString();
        //    }

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
    }
}
