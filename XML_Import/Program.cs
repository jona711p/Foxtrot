using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XML_Import
{
    /// <summary>
    /// Jonas, Mikael & Thomas
    /// </summary>
    class Program
    {
        static XDocument xmlDocument;
        private static FileSystemWatcher watcher;
        static void Main(string[] args)
        {
            WatchXMLDir();
        }

        static void WatchXMLDir()
        {
            watcher = new FileSystemWatcher { Path = @"INSERT_XML_HERE\", Filter = "*.xml" };
            watcher.Created += ReadCitiesAndCategoriesAndMainCategoriesFromXML;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Systemet venter på en XML fil...");
            Console.ReadLine();
        }

        static void DeleteXMLFile(string path)
        {
            System.IO.File.Delete(path);
        }

        static void ReadCitiesAndCategoriesAndMainCategoriesFromXML(object sender, FileSystemEventArgs args)
        {
            Thread[] readFromXML = new Thread[]
            {
                new Thread(() =>
                {
                    ReadCitiesFromXML(args.FullPath);
                }),

                new Thread(() =>
                {
                    ReadCategoriesFromXML(args.FullPath);
                }),

                new Thread(() =>
                {
                    ReadMainCategoriesFromXML(args.FullPath);
                })
            };

            foreach (Thread thread in readFromXML)
            {
                thread.Start();
            }

            foreach (Thread thread in readFromXML)
            {
                thread.Join();
            }

            ReadAllFromXML(args.FullPath);
        }

        static void ReadCitiesFromXML(string path)
        {
            xmlDocument = XDocument.Load(path);

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(x => new City()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                PostalCode = TryToConvertNodeValueToInt(x.XPathSelectElement("../*[name()='PostalCode']"))
            }).Distinct().OrderBy(x => x.ID).ToList();
        }

        static void ReadCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();
        }

        static void ReadMainCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();
        }

        static void ReadAllFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);
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

            DeleteXMLFile(path);
        }

        static int? TryToConvertNodeValueToInt(XElement node)
        {
            return node == null ? null : (int?)int.Parse(node.Value);
        }

        static string TryToConvertNodeValueToString(XElement node)
        {
            return node == null || node.Value.Equals("") ? null : node.Value;
        }

        static float? TryToConvertNodeValueToFloat(XElement node)
        {
            return node == null || node.Value.Equals("") ? null : (float?)float.Parse(node.Value.Replace('.', ','));
        }

        static List<int?> TryToConvertNodeValueToIntList(XElement node)
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

        static List<string> TryToConvertNodeValueToStringList(XElement node)
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

        static DateTime? TryToConvertNodeValueToDateTime(XElement node)
        {
            return node == null ? null : (DateTime?)DateTime.Parse(node.Value);
        }
    }
}
