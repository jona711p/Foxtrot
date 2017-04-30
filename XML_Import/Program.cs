using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XML_Import
{
    /// <summary>
    /// Jonas, Mikael & Thomas
    /// </summary>
    class Program
    {
        private static XDocument xmlDocument;
        static void Main(string[] args)
        {
            ReadCitysFromXML();
            ReadCategorysFromXML();
            ReadMainCategorysFromXML();
            ReadAllFromXML();
            Console.ReadLine();
        }

        private static void ReadCitysFromXML()
        {
            xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(y => new City()
            {
                ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Name']")),
                PostalCode = TryToConvertNodeValueToInt(y.XPathSelectElement("../*[name()='PostalCode']"))
            }).Distinct().OrderBy(y => y.ID).ToList();
        }

        private static void ReadCategorysFromXML()
        {
            XDocument xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(y => new Category()
            {
                ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(y => y.ID).ToList();
        }

        private static void ReadMainCategorysFromXML()
        {
            XDocument xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(y => new MainCategory()
            {
                ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(y => y.ID).ToList();
        }

        public static void ReadAllFromXML()
        {
            XDocument xmlDocument = XDocument.Load(@"c://Temp//skive_xml.xml");
            List<Product> products = xmlDocument.XPathSelectElements("//*[name()='Product']").Select(x => new Product()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                Actor = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                Address = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='AddressLine1']")),
                Latitude = TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Latitude']")),
                Longitude = TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Longitude']")),
                Phone = TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Phone']")),
                Email = TryToConvertNodeValueToStringList(x.XPathSelectElement(".//*[name()='Email']")),
                Fax = TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Fax']")),
                CanonicalUrl = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='CanonicalUrl']")),
                Url = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Url']")),
                Created = TryToConvertNodeValueToDateTime(x.XPathSelectElement(".//*[name()='Created']")),
                Description = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Text']")),

                Cities = x.XPathSelectElements(".//*[name()='Municipality']").Select(y => new City()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                Categories = x.XPathSelectElements("//*[name()='Category']").Select(y => new Category()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                MainCategories = x.XPathSelectElements("//*[name()='MainCategory']").Select(y => new MainCategory()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new File()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']")),
                    Uri = TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Uri']"))
                }).OrderBy(y => y.ID).ToList()
            }).ToList();
        }

        private static int? TryToConvertNodeValueToInt(XElement node)
        {
            return node == null ? null : (int?)int.Parse(node.Value);
        }

        private static string TryToConvertNodeValueToString(XElement node)
        {
            return node == null || node.Value.Equals("") ? null : node.Value;
        }

        private static float? TryToConvertNodeValueToFloat(XElement node)
        {
            return node == null || node.Value.Equals("") ? null : (float?)float.Parse(node.Value.Replace('.', ','));
        }

        private static List<int?> TryToConvertNodeValueToIntList(XElement node)
        {
            List<int?> output = new List<int?>();

            if (node == null || node.Value.Equals(""))
            {
                return null;
            }

            else
            {
                string[] moreThanOneNumbers = node.Value.Split('/');

                foreach (string number in moreThanOneNumbers)
                {
                    if (!number.Equals("Fur Fossiler 55.000."))
                    {
                        output.Add(int.Parse(number.Replace(" ", "").Replace("+45", "")));
                    }
                }
            }
            return output;
        }

        private static List<string> TryToConvertNodeValueToStringList(XElement node)
        {
            List<string> output = new List<string>();

            if (node == null || node.Value.Equals(""))
            {
                return null;
            }

            else
            {
                string[] moreThanOneEmails = node.Value.Split('/');

                foreach (string emails in moreThanOneEmails)
                {
                        output.Add(emails);
                }
            }
            return output;
        }

        private static DateTime? TryToConvertNodeValueToDateTime(XElement node)
        {
            return node == null ? null : (DateTime?)DateTime.Parse(node.Value);
        }
    }
}
