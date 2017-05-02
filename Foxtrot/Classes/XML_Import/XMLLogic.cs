using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

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
            MessageBox.Show("Ny XML fil indlæst til Databasen!");
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
                    ReadMainCategoriesFromXML(args.FullPath);

                }),

                new Thread(() =>
                {
                    ReadCategoriesFromXML(args.FullPath);
                }),
                    
                new Thread(() =>
                {
                    ReadFilesFromXML(args.FullPath);
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

        static void ReadMainCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            DBLogic.WriteMainCategoriesToDB(mainCategories);
        }

        static void ReadCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            DBLogic.WriteCategoriesToDB(categories);
        }

        static void ReadFilesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<File> files = xmlDocument.XPathSelectElements("//*[name()='File']").Select(x => new File()
            {
                ID = TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                URI = TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Uri']"))
            }).OrderBy(x => x.ID).ToList();

            DBLogic.WriteFilesToDB(files);
        }
       static void ReadOpeningHoursFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);
            List<OpeningHours> OpeningHours = xmlDocument.XPathSelectElements("//*[name()='Period']").Select(x => new File()
            {

            }

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

                ContactPhone = TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Phone']")),
                ContactEmail = TryToConvertNodeValueToStringList(x.XPathSelectElement(".//*[name()='Email']")),
                ContactFax = TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Fax']")),
                
                CreationDate = TryToConvertNodeValueToDateTime(x.XPathSelectElement(".//*[name()='Created']")),
                Price = TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Price']")),

                Description = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Text']")),
                ExtraDesription = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Text']")),

                Website = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Url']")),
                CanonicalUrl = TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='CanonicalUrl']")),

                Cities = x.XPathSelectElements(".//*[name()='Municipality']").Select(y => new City()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                MainCategories = x.XPathSelectElements("//*[name()='MainCategory']").Select(y => new MainCategory()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                Categories = x.XPathSelectElements("//*[name()='Category']").Select(y => new Category()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new File()
                {
                    ID = TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']")),
                }).OrderBy(y => y.ID).ToList(),

                OpeningHours = x.XPathSelectElements("//*[name()='Period']").Select(y => new OpeningHours()
                {
                    
                }
                
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
