using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using Microsoft.Win32;

namespace Foxtrot.GUI.XMLImport
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    public partial class XMLImport : Window, INotifyPropertyChanged
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; NotifyPropertyChanged(); }
        }

        static FileSystemWatcher watcher;
        public string FullPathAndFileName { get; private set; }

        private XDocument xmlDocument;

        public XMLImport()
        {
            WatchXMLDir();

            ResizeMode = ResizeMode.NoResize; //locks the window to its inital size (600x300) and disables the ability to minimize

            InitializeComponent();

            DataContext = this;
        }

        private void WatchXMLDir() // Watches the "INSERT_XML_HERE" dir for XML files, if it finds one, it runs the entire program, and returns here and will keep watching for a new one
        {
            if (!Directory.Exists(@"INSERT_XML_HERE")) { Directory.CreateDirectory(@"INSERT_XML_HERE"); }

            watcher = new FileSystemWatcher { Path = @"INSERT_XML_HERE\", Filter = "*.xml" };
            watcher.Created += ReadLoadedXMLFile;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadLoadedXMLFile(object sender, FileSystemEventArgs args)
        {
            FullPathAndFileName = args.FullPath;
            ReadFromNewXML();

            System.IO.File.Delete(FullPathAndFileName);
        }

        private void btn_Open_XML_File_Click(object sender, RoutedEventArgs e)
        {
            OpenXMLFile();
        }

        private void OpenXMLFile()
        {
            FileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Dokument (.xml)|*.xml";

            bool? result = openFileDialog.ShowDialog();
            openFileDialog.InitialDirectory = ".";

            if (result == true)
            {
                FullPathAndFileName = openFileDialog.FileName;
                char[] param = { '\\' };
                string[] tempArray = FullPathAndFileName.Split(param);
                FileName = tempArray[tempArray.Length - 1];
            }
        }

        private void btn_Start_Reading_From_XML_Click(object sender, RoutedEventArgs e)
        {
            if (FullPathAndFileName != null)
            {
                DisableButtoms();
                ReadFromNewXML();
            }

            else
            {
                MessageBox.Show("Du skal vælge en XML Fil først!");
            }

            EnableButtoms();
        }

        private void EnableButtoms()
        {
            btn_Open_XML_File.IsEnabled = true;
            btn_Start_Reading_From_XML.IsEnabled = true;
        }

        private void DisableButtoms()
        {
            btn_Open_XML_File.IsEnabled = false;
            btn_Start_Reading_From_XML.IsEnabled = false;
        }

        private void ReadFromNewXML()
        {
            ReadFromXMLInThreads();
            ReadProductsFromXML();

            MessageBox.Show("Ny XML fil indlæst til Databasen!");
        }

        private void ReadFromXMLInThreads()
        {
            Thread[] readFromXML = new Thread[]
            {
                new Thread(() => { ReadCategoriesFromXML(); }),
                new Thread(() => { ReadCitiesFromXML(); }),
                new Thread(() => { ReadFilesFromXML(); }),
                new Thread(() => { ReadMainCategoriesFromXML(); }),
                new Thread(() => { ReadOpeningHoursFromXML(); })
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

        private void ProgressBarPercentage(int percentage)
        {
            for (int i = 0; i < percentage; i++)
            {
                pb_Status.Value++;
            }
        }

        private int ReadActorFromXML(string name)
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

        private void ReadCategoriesFromXML()
        {
            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<Category> dupeCheckList = categories.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Categories").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteCategories(dupeCheckList);
        }

        private void ReadCitiesFromXML()
        {
            xmlDocument = XDocument.Load(FullPathAndFileName);

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(x => new City()
            {
                ID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                PostalCode = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("../*[name()='PostalCode']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            List<City> dupeCheckList = cities.Where(x => !XMLDBReadLogic.DupeCheckList("ID", "Cities").Contains(x.ID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteCities(dupeCheckList);
        }

        private void ReadFilesFromXML()
        {
            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<Classes.File> files = xmlDocument.XPathSelectElements("//*[name()='File']").Select(x => new Classes.File()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                URI = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Uri']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<Classes.File> dupeCheckList = files.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Files").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteFiles(dupeCheckList);
        }

        private void ReadMainCategoriesFromXML()
        {
            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<MainCategory> dupeCheckList = mainCategories.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "MainCategories").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteMainCategories(dupeCheckList);
        }

        private void ReadOpeningHoursFromXML()
        {
            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

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

        private void ReadProductsFromXML()
        {
            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<Classes.Product> products = xmlDocument.XPathSelectElements("//*[name()='Product']").Select(x => new Classes.Product()
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

                Files = x.XPathSelectElements(".//*[name()='File']").Select(y => new Classes.File()
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

            List<Classes.Product> dupeCheckList = products.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Products").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            XMLDBWriteLogic.WriteProducts(dupeCheckList);
            XMLDBWriteLogic.WriteRelCategories(dupeCheckList);
            XMLDBWriteLogic.WriteRelCombiProducts(dupeCheckList);
            XMLDBWriteLogic.WriteRelEventsProducts(dupeCheckList);
            XMLDBWriteLogic.WriteRelFiles(dupeCheckList);
            XMLDBWriteLogic.WriteRelMainCategories(dupeCheckList);
            XMLDBWriteLogic.WriteRelOpeningHours(dupeCheckList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
