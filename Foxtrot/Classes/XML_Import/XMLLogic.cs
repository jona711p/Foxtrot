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
            watcher.Created += ReadFromNewXML;
            watcher.EnableRaisingEvents = true;
        }

        static void ReadFromNewXML(object sender, FileSystemEventArgs args)
        {
            ReadFromXMLInThreads(args.FullPath);
            //ReadProductsFromXML(args.FullPath);

            System.IO.File.Delete(args.FullPath);
            MessageBox.Show("Ny XML fil indlæst til Databasen!");
        }

        static void ReadFromXMLInThreads(string path)
        {
            Thread[] readFromXML = new Thread[]
            {
                new Thread(() =>
                {
                    ReadCitiesFromXML(path);
                }),

                new Thread(() =>
                {
                    ReadCategoriesFromXML(path);
                }),

                new Thread(() =>
                {
                    ReadFilesFromXML(path);
                }),

                new Thread(() =>
                {
                    ReadMainCategoriesFromXML(path);
                }),

                 new Thread(() =>
                {
                    ReadOpeningHoursFromXML(path);
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
        }
        
        static List<City> DupeChecking(List<City> cities)
        {
            List<int> dupeCheckList = DBLogic.DupeCheckingFromDB("Cities");
            List<City> copyOfCitiesList = cities.ToList();


            foreach (City city in copyOfCitiesList)
            {
                if (dupeCheckList.Contains(city.ID.Value))
                {
                    cities.Remove(city);
                }
            }

            return cities;
        }

        static List<Category> DupeChecking(List<Category> categories)
        {
            List<int> dupeCheckList = DBLogic.DupeCheckingFromDB("Categories");
            List<Category> copyOfCategoriesList = categories.ToList();


            foreach (Category category in copyOfCategoriesList)
            {
                if (dupeCheckList.Contains(category.ID.Value))
                {
                    categories.Remove(category);
                }
            }

            return categories;
        }

        static List<File> DupeChecking(List<File> files)
        {
            List<int> dupeCheckList = DBLogic.DupeCheckingFromDB("Files");
            List<File> copyOfFilesList = files.ToList();


            foreach (File file in copyOfFilesList)
            {
                if (dupeCheckList.Contains(file.ID.Value))
                {
                    files.Remove(file);
                }
            }

            return files;
        }

        static List<MainCategory> DupeChecking(List<MainCategory> mainCategories)
        {
            List<int> dupeCheckList = DBLogic.DupeCheckingFromDB("MainCategories");
            List<MainCategory> copyOfMainCategoriesList = mainCategories.ToList();


            foreach (MainCategory mainCategory in copyOfMainCategoriesList)
            {
                if (dupeCheckList.Contains(mainCategory.ID.Value))
                {
                    mainCategories.Remove(mainCategory);
                }
            }

            return mainCategories;
        }

        static List<OpeningHour> DupeChecking(List<OpeningHour> openingHours)
        {
            List<int> dupeCheckList = DBLogic.DupeCheckingFromDB("OpeningHours");
            List<OpeningHour> copyOfOpeningHoursList = openingHours.ToList();


            foreach (OpeningHour openingHour in copyOfOpeningHoursList)
            {
                if (dupeCheckList.Contains(openingHour.ID.Value))
                {
                    openingHours.Remove(openingHour);
                }
            }

            return openingHours;
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

            cities = DupeChecking(cities);
            DBLogic.WriteCitiesToDB(cities);
        }

        static void ReadCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                ID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            categories = DupeChecking(categories);
            DBLogic.WriteCategoriesToDB(categories);
        }

        static void ReadFilesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<File> files = xmlDocument.XPathSelectElements("//*[name()='File']").Select(x => new File()
            {
                ID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                URI = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Uri']"))

            }).OrderBy(x => x.ID).ToList();

            files = DupeChecking(files);
            DBLogic.WriteFilesToDB(files);
        }

        static void ReadMainCategoriesFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                ID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            mainCategories = DupeChecking(mainCategories);
            DBLogic.WriteMainCategoriesToDB(mainCategories);
        }

        static void ReadOpeningHoursFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<OpeningHour> openingHours = xmlDocument.XPathSelectElements("//*[name()='Period']").Select(x => new OpeningHour()
            {
                ID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
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
                Sunday = bool.Parse(x.XPathSelectElement("./*[name()='Sunday']").Value),

            }).OrderBy(x => x.ID).ToList();

            openingHours = DupeChecking(openingHours);
            DBLogic.WriteOpeningHoursToDB(openingHours);
        }

        static void ReadProductsFromXML(string path)
        {
            XDocument xmlDocument = XDocument.Load(path);

            List<Product> products = xmlDocument.XPathSelectElements("//*[name()='Product']").Select(x => new Product()
            {
                ID = SortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
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

                Cities = x.XPathSelectElements(".//*[name()='Municipality']").Select(y => new City()
                {
                    ID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                ExtraDesription = x.XPathSelectElements(".//*[name()='ProductMetaTag']").Select(y => new ExtraDesription()
                {
                    Description = SortingLogic.TryToConvertNodeValueToString(y.XPathSelectElement("./*[name()='Name']"))
                }).ToList(),

                MainCategories = x.XPathSelectElements("//*[name()='MainCategory']").Select(y => new MainCategory()
                {
                    ID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                Categories = x.XPathSelectElements("//*[name()='Category']").Select(y => new Category()
                {
                    ID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new File()
                {
                    ID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).OrderBy(y => y.ID).ToList(),

                OpeningHours = x.XPathSelectElements(".//*[name()='Period']").Select(y => new OpeningHour()
                {
                    ID = SortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).OrderBy(y => y.ID).ToList(),

                ActorID = ReadActorFromXML(SortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")))

            }).ToList();

            DBLogic.WriteProductsToDB(products);
            DBLogic.WriteRelFileTable(products);
            DBLogic.WriteRelOpeningHoursTable(products);
            DBLogic.WriteRelCombiTable(products);
            DBLogic.WriteRelEventTable(products);

        }

        static int? ReadActorFromXML(string name)
        {
            Actor actor = new Actor();

            actor.FirstName = null;
            actor.LastName = null;
            actor.WorkPhone = null;
            actor.WorkEmail = null;
            actor.WorkFax = null;
            actor.CompanyName = name;

            return DBLogic.CheckActorDuplicatesInDB(actor);
        }
    }
}
