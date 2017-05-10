using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Classes
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
            watcher.Created += ReadFromNewXML;
            watcher.EnableRaisingEvents = true;
        }

        static void ReadFromNewXML(object sender, FileSystemEventArgs args)
        {
            ReadFromXMLInThreads(args.FullPath);
            ReadProductsFromXML(args.FullPath);

            System.IO.File.Delete(args.FullPath);
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

            return DBReadLogic.DupeCheckActors(actor);
        }

        static void ReadCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                XMLID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<Category> dupeCheckList = categories.Where(x => !DBReadLogic.DupeCheckList("XMLID", "Categories").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            DBWriteLogic.WriteCategories(dupeCheckList);
        }

        static void ReadCitiesFromXML(string path)
        {
            xmlDocument = XDocument.Load(path);

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(x => new City()
            {
                ID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                PostalCode = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("../*[name()='PostalCode']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            List<City> dupeCheckList = cities.Where(x => !DBReadLogic.DupeCheckList("ID", "Cities").Contains(x.ID.Value)).ToList(); // Removes any dupes found already in the DataBase

            DBWriteLogic.WriteCities(dupeCheckList);
        }

        static void ReadFilesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<File> files = xmlDocument.XPathSelectElements("//*[name()='File']").Select(x => new File()
            {
                XMLID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                URI = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Uri']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<File> dupeCheckList = files.Where(x => !DBReadLogic.DupeCheckList("XMLID", "Files").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            DBWriteLogic.WriteFiles(dupeCheckList);
        }

        static void ReadMainCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                XMLID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<MainCategory> dupeCheckList = mainCategories.Where(x => !DBReadLogic.DupeCheckList("XMLID", "MainCategories").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            DBWriteLogic.WriteMainCategories(dupeCheckList);
        }

        static void ReadOpeningHoursFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<OpeningHour> openingHours = xmlDocument.XPathSelectElements("//*[name()='Period']").Select(x => new OpeningHour()
            {
                XMLID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                StartDate = SortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement("./*[name()='StartDate']")),
                EndDate = SortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement("./*[name()='EndDate']")),
                StartTime = SortingLogic.TryToConvertNodeValueToTime(x.XPathSelectElement("./*[name()='StartTime']")),
                EndTime = SortingLogic.TryToConvertNodeValueToTime(x.XPathSelectElement("./*[name()='EndTime']")),
                Monday = bool.Parse(x.XPathSelectElement("./*[name()='Monday']").Value),
                Tuesday = bool.Parse(x.XPathSelectElement("./*[name()='Tuesday']").Value),
                Wednesday = bool.Parse(x.XPathSelectElement("./*[name()='Wednesday']").Value),
                Thursday = bool.Parse(x.XPathSelectElement("./*[name()='Thursday']").Value),
                Friday = bool.Parse(x.XPathSelectElement("./*[name()='Friday']").Value),
                Saturday = bool.Parse(x.XPathSelectElement("./*[name()='Saturday']").Value),
                Sunday = bool.Parse(x.XPathSelectElement("./*[name()='Sunday']").Value)
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<OpeningHour> dupeCheckList = openingHours.Where(x => !DBReadLogic.DupeCheckList("XMLID", "OpeningHours").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            DBWriteLogic.WriteOpeningHours(dupeCheckList);
        }

        static void ReadProductsFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Product> products = xmlDocument.XPathSelectElements("//*[name()='Product']").Select(x => new Product()
            {
                XMLID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                Address = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='AddressLine1']")),
                Latitude = SortingLogic.TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Latitude']")),
                Longitude = SortingLogic.TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='Longitude']")),
                ContactPhone = SortingLogic.TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Phone']")),
                ContactEmail = SortingLogic.TryToConvertNodeValueToStringList(x.XPathSelectElement(".//*[name()='Email']")),
                ContactFax = SortingLogic.TryToConvertNodeValueToIntList(x.XPathSelectElement(".//*[name()='Fax']")),
                CreationDate = SortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement(".//*[name()='Created']")),
                Price = SortingLogic.TryToConvertNodeValueToFloat(x.XPathSelectElement(".//*[name()='PriceFrom']")),
                Description = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Text']")),
                Website = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Url']")),
                CanonicalUrl = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='CanonicalUrl']")),
                Categories = x.XPathSelectElements(".//*[name()='Category']").Select(y => new Category()
                {
                    XMLID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),
                Cities = x.XPathSelectElements(".//*[name()='Municipality']").Select(y => new City()
                {
                    ID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),
                ExtraDescription = x.XPathSelectElements(".//*[name()='ProductMetaTag']").Select(y => new ExtraDescription()
                {
                    Description = SortingLogic.TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Name']"))
                }).ToList(),
                Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new File()
                {
                    XMLID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).OrderBy(y => y.XMLID).ToList(),
                MainCategories = x.XPathSelectElements(".//*[name()='MainCategory']").Select(y => new MainCategory()
                {
                    XMLID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),
                OpeningHours = x.XPathSelectElements(".//*[name()='Period']").Select(y => new OpeningHour()
                {
                    XMLID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).OrderBy(y => y.XMLID).ToList(),
                ActorID = ReadActorFromXML(SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")))
            }).OrderBy(x => x.XMLID).ToList();

            List<Product> dupeCheckList = products.Where(x => !DBReadLogic.DupeCheckList("XMLID", "Products").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            DBWriteLogic.WriteProducts(dupeCheckList);
            DBWriteLogic.WriteRelCategories(dupeCheckList);
            DBWriteLogic.WriteRelCombiProducts(dupeCheckList);
            DBWriteLogic.WriteRelEventsProducts(dupeCheckList);
            DBWriteLogic.WriteRelFiles(dupeCheckList);
            DBWriteLogic.WriteRelMainCategories(dupeCheckList);
            DBWriteLogic.WriteRelOpeningHours(dupeCheckList);
        }
    }
}
