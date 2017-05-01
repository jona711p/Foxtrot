using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Xml.XPath;
using Foxtrot.Classes;

namespace Classes
{
    class XMLLogic
    {
        static FileSystemWatcher watcher;
        static XDocument xmlDocument;

        public static void WatchXMLDir() // Watches the "INSERT_XML_HERE" dir for XML files, if it finds one, it runs the entire program, and returns here and will keep watching for a new one
        {
            watcher = new FileSystemWatcher { Path = @"INSERT_XML_HERE\", Filter = "*.xml" };
            watcher.Created += ReadCitiesAndCategoriesAndMainCategoriesFromXML;
            watcher.EnableRaisingEvents = true;
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

            DBLogic.WriteCitiesToDB(cities);
        }

        static void ReadCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            //WriteCategoriesToDB(categories);
        }

        static void ReadMainCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            //WriteMainCategoriesToDB(mainCategories);
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

            //WriteProductsToDB(products);

            DeleteXMLFile(path);
        }

        static int? TryToConvertNodeValueToInt(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return node == null ? null : (int?)int.Parse(node.Value);
        }

        static string TryToConvertNodeValueToString(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return node == null || node.Value.Equals("") ? null : node.Value;
        }

        static float? TryToConvertNodeValueToFloat(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And with "." replaced by ",", because float needs "," to read it properly
        {
            return node == null || node.Value.Equals("") ? null : (float?)float.Parse(node.Value.Replace('.', ','));
        }

        static List<int?> TryToConvertNodeValueToIntList(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And if there is more than one number seperated by "/". It also removes "+45" and spaces between numbers, so that we end up with 8 digits!
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
                    if (!number.Equals("Fur Fossiler 55.000.")) // <-- Come on S.E.T. :P Thats just sad :D
                    {
                        output.Add(int.Parse(number.Replace(" ", "").Replace("+45", "")));
                    }
                }
            }
            return output;
        }

        static List<string> TryToConvertNodeValueToStringList(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And if there is more than one string seperated by "/".
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

        static DateTime? TryToConvertNodeValueToDateTime(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return node == null ? null : (DateTime?)DateTime.Parse(node.Value);
        }
    }
}
