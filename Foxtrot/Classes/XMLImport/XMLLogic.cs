using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;
using Foxtrot.Classes.XMLImport.XMLDB;

namespace Foxtrot.Classes.XMLImport
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    class XMLLogic
    {
        static FileSystemWatcher watcher;
        static XDocument xmlDocument;

        public static void WatchXMLDir() // Watches the "INSERT_XML_HERE" dir for XML files, if it finds one, it runs the entire program, and returns here and will keep watching for a new one
        {
            if (!Directory.Exists(@"INSERT_XML_HERE")) { Directory.CreateDirectory(@"INSERT_XML_HERE"); }

            watcher = new FileSystemWatcher { Path = @"INSERT_XML_HERE\", Filter = "*.xml" };
            watcher.Created += ReadLoadedXMLFile;
            watcher.EnableRaisingEvents = true;
        }

        static void ReadLoadedXMLFile(object sender, FileSystemEventArgs args)
        {
            ReadFromNewXML(args.FullPath);
        }

        static void ReadFromNewXML(string path)
        {
            ReadFromXMLInThreads(path);
            ReadProductsFromXML(path);

            System.IO.File.Delete(path);
            MessageBox.Show("Ny XML fil indlæst til Databasen!");
        }

        static void ReadFromXMLInThreads(string path)
        {
            Thread[] readFromXML = new Thread[]
            {
                new Thread(() => { ReadCitiesFromXML(path); }),
                new Thread(() => { ReadCategoriesFromXML(path); }),
                new Thread(() => { ReadFilesFromXML(path); }),
                new Thread(() => { ReadMainCategoriesFromXML(path); }),
                new Thread(() => { ReadOpeningHoursFromXML(path); })
            };

            foreach (Thread thread in readFromXML)
            {
                thread.Start();
            }

            foreach (Thread thread in readFromXML)
            {
                thread.Join();
            }
        }

        static int ReadActorFromXML(string name)
        {
            Actor actor = new Actor();

            actor.FirstName = null;
            actor.LastName = null;
            actor.WorkPhone = null;
            actor.WorkEmail = null;
            actor.WorkFax = null;
            actor.CompanyName = name;

            return XMLDBReadLogic.DupeCheckActors(actor);
        }

        static void ReadCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<Category> dupeCheckList = categories.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Categories").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteCategories(dupeCheckList);
        }

        static void ReadCitiesFromXML(string path)
        {
            xmlDocument = XDocument.Load(path);

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(x => new City()
            {
                ID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                PostalCode = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("../*[name()='PostalCode']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            List<City> dupeCheckList = cities.Where(x => !XMLDBReadLogic.DupeCheckList("ID", "Cities").Contains(x.ID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteCities(dupeCheckList);
        }

        static void ReadFilesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<File> files = xmlDocument.XPathSelectElements("//*[name()='File']").Select(x => new File()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                URI = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Uri']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<File> dupeCheckList = files.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Files").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteFiles(dupeCheckList);
        }

        static void ReadMainCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<MainCategory> dupeCheckList = mainCategories.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "MainCategories").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteMainCategories(dupeCheckList);
        }

        static void ReadOpeningHoursFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<OpeningHour> openingHours = xmlDocument.XPathSelectElements("//*[name()='Period']").Select(x => new OpeningHour()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                StartDate = XMLSortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement("./*[name()='StartDate']")),
                EndDate = XMLSortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement("./*[name()='EndDate']")),
                StartTime = XMLSortingLogic.TryToConvertNodeValueToTime(x.XPathSelectElement("./*[name()='StartTime']")),
                EndTime = XMLSortingLogic.TryToConvertNodeValueToTime(x.XPathSelectElement("./*[name()='EndTime']")),
                Monday = bool.Parse(x.XPathSelectElement("./*[name()='Monday']").Value),
                Tuesday = bool.Parse(x.XPathSelectElement("./*[name()='Tuesday']").Value),
                Wednesday = bool.Parse(x.XPathSelectElement("./*[name()='Wednesday']").Value),
                Thursday = bool.Parse(x.XPathSelectElement("./*[name()='Thursday']").Value),
                Friday = bool.Parse(x.XPathSelectElement("./*[name()='Friday']").Value),
                Saturday = bool.Parse(x.XPathSelectElement("./*[name()='Saturday']").Value),
                Sunday = bool.Parse(x.XPathSelectElement("./*[name()='Sunday']").Value)
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<OpeningHour> dupeCheckList = openingHours.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "OpeningHours").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteOpeningHours(dupeCheckList);
        }

        static void ReadProductsFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Product> products = xmlDocument.XPathSelectElements("//*[name()='Product']").Select(x => new Product()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                Address = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='AddressLine1']")),
                Latitude = XMLSortingLogic.TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Latitude']")),
                Longitude = XMLSortingLogic.TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Longitude']")),
                ContactPhone = XMLSortingLogic.TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Phone']")),
                ContactEmail = XMLSortingLogic.TryToConvertNodeValueToStringList(x.XPathSelectElement(".//*[name()='Email']")),
                ContactFax = XMLSortingLogic.TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Fax']")),
                CreationDate = XMLSortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement(".//*[name()='Created']")),
                Price = XMLSortingLogic.TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='PriceFrom']")),
                Description = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Text']")),
                Website = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Url']")),
                CanonicalUrl = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='CanonicalUrl']")),
                Categories = x.XPathSelectElements(".//*[name()='Category']").Select(y => new Category()
                {
                    XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),
                Cities = x.XPathSelectElements(".//*[name()='Municipality']").Select(y => new City()
                {
                    ID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),
                ExtraDescription = x.XPathSelectElements(".//*[name()='ProductMetaTag']").Select(y => new ExtraDescription()
                {
                    Description = XMLSortingLogic.TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Name']"))
                }).ToList(),
                Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new File()
                {
                    XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).OrderBy(y => y.XMLID).ToList(),
                MainCategories = x.XPathSelectElements(".//*[name()='MainCategory']").Select(y => new MainCategory()
                {
                    XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),
                OpeningHours = x.XPathSelectElements(".//*[name()='Period']").Select(y => new OpeningHour()
                {
                    XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).OrderBy(y => y.XMLID).ToList(),
                ActorID = ReadActorFromXML(XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")))
            }).OrderBy(x => x.XMLID).ToList();

            List<Product> dupeCheckList = products.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Products").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteProducts(dupeCheckList);
            XMLDBWriteLogic.WriteRelCategories(dupeCheckList);
            XMLDBWriteLogic.WriteRelCombiProducts(dupeCheckList);
            XMLDBWriteLogic.WriteRelEventsProducts(dupeCheckList);
            XMLDBWriteLogic.WriteRelFiles(dupeCheckList);
            XMLDBWriteLogic.WriteRelMainCategories(dupeCheckList);
            XMLDBWriteLogic.WriteRelOpeningHours(dupeCheckList);
        }
    }
}
