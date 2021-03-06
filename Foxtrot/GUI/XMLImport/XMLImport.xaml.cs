﻿using System;
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
    /// Jonas Lykke
    /// </summary>
    // Class to write a new XML fil to the database
    public partial class XMLImport : Window, INotifyPropertyChanged
    {
        private FileSystemWatcher fileSystemWatcher;
        private List<Classes.Product> productList;
        private ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        private XDocument xmlDocument;

        public string FullPathAndFileName { get; private set; }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; NotifyPropertyChanged(); }
        }

        private double progressPercentage;

        public double ProgressPercentage
        {
            get { return progressPercentage; }
            set { progressPercentage = value; NotifyPropertyChanged(); }
        }

        private string progressPoint;

        public string ProgressPoint
        {
            get { return progressPoint; }
            set { progressPoint = value; NotifyPropertyChanged(); }
        }

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

            fileSystemWatcher = new FileSystemWatcher { Path = @"INSERT_XML_HERE\", Filter = "*.xml" };
            fileSystemWatcher.EnableRaisingEvents = true;
            fileSystemWatcher.Created += ReadLoadedXMLFile;
        }

        private void ReadLoadedXMLFile(object sender, FileSystemEventArgs args)
        {
            FullPathAndFileName = args.FullPath;
            ReadFromXML();
        }

        private void btn_Open_XML_File_Click(object sender, RoutedEventArgs e)
        {
            OpenXMLFile();
        }

        private void OpenXMLFile()
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Filter = "XML Dokument (.xml)|*.xml";

            bool? result = fileDialog.ShowDialog();
            fileDialog.InitialDirectory = ".";

            if (result == true)
            {
                FullPathAndFileName = fileDialog.FileName;
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
                ReadFromXML();
            }

            else
            {
                MessageBox.Show("Du Skal Vælge en XML Fil Først!");
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

        private void ReadFromXML()
        {
            BackgroundWorker readFromXMLInThreads = new BackgroundWorker();

            readFromXMLInThreads.WorkerReportsProgress = true;
            readFromXMLInThreads.ProgressChanged += ProgressBarPercentage;

            readFromXMLInThreads.DoWork += ReadFromXMLInThreads;

            readFromXMLInThreads.RunWorkerCompleted += ReadFromXMLDone;

            readFromXMLInThreads.RunWorkerAsync();
        }

        private void ReadFromXMLInThreads(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker[] readFromXMLInThreadsArray = new BackgroundWorker[6];

            for (int i = 0; i < readFromXMLInThreadsArray.Length; i++)
            {
                readFromXMLInThreadsArray[i] = new BackgroundWorker();
                readFromXMLInThreadsArray[i].WorkerReportsProgress = true;
                readFromXMLInThreadsArray[i].ProgressChanged += ProgressBarPercentage;
            }

            readFromXMLInThreadsArray[0].DoWork += ReadCategoriesFromXML;
            readFromXMLInThreadsArray[1].DoWork += ReadCitiesFromXML;
            readFromXMLInThreadsArray[2].DoWork += ReadFilesFromXML;
            readFromXMLInThreadsArray[3].DoWork += ReadMainCategoriesFromXML;

            for (int i = 0; i < 4; i++)
            {
                readFromXMLInThreadsArray[i].RunWorkerAsync();
            }

            while (ProgressPercentage < 64)
            {
                Thread.Sleep(1000);
            }

            readFromXMLInThreadsArray[4].DoWork += ReadProductsFromXML;

            readFromXMLInThreadsArray[4].RunWorkerAsync();

            while (ProgressPercentage < 84)
            {
                Thread.Sleep(1000);
            }

            readFromXMLInThreadsArray[5].DoWork += ReadRelFilesFromXML;

            readFromXMLInThreadsArray[5].RunWorkerAsync();

            while (ProgressPercentage < 100)
            {
                Thread.Sleep(1000);
            }
        }

        private void ReadFromXMLDone(object sender, RunWorkerCompletedEventArgs e)
        {
            if (FullPathAndFileName.Contains("INSERT_XML_HERE"))
            {
                System.IO.File.Delete(FullPathAndFileName);
            }

            MessageBox.Show("En ny XML Fil Indlæst!" +
                            "\nDer blev Skrevet: " + productList.Count + " nye Produktet til Databasen!");
        }

        private void ReadCategoriesFromXML(object sender, DoWorkEventArgs e)
        {
            ProgressPoint = "Læser nu Kategoriger fra XML Filen";

            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<Category> categories = xmlDocument.XPathSelectElements("//*[name()='Category']").Select(x => new Category()
            {
                ID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            List<Category> dupeCheckList = categories.Where(x => !XMLDBReadLogic.DupeCheckList("ID", "Categories").Contains(x.ID.Value)).ToList(); // Removes any dupes found already in the DataBase

            ProgressPoint = "Skriver nu Kategoriger til Databasen";

            XMLDBWriteLogic.WriteCategories(dupeCheckList);

            (sender as BackgroundWorker).ReportProgress(16);
        }

        private void ReadCitiesFromXML(object sender, DoWorkEventArgs e)
        {
            ProgressPoint = "Læser nu Byer fra XML Filen";

            xmlDocument = XDocument.Load(FullPathAndFileName);

            List<City> cities = xmlDocument.XPathSelectElements("//*[name()='Municipality']").Select(x => new City()
            {
                ID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")),
                PostalCode = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("../*[name()='PostalCode']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            List<City> dupeCheckList = cities.Where(x => !XMLDBReadLogic.DupeCheckList("ID", "Cities").Contains(x.ID.Value)).ToList(); // Removes any dupes found already in the DataBase

            ProgressPoint = "Skriver nu Byer til Databasen";

            XMLDBWriteLogic.WriteCities(dupeCheckList);

            (sender as BackgroundWorker).ReportProgress(16);
        }

        private void ReadFilesFromXML(object sender, DoWorkEventArgs e)
        {
            ProgressPoint = "Læser nu Billeder fra XML Filen";

            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<Classes.File> files = xmlDocument.XPathSelectElements("//*[name()='File']").Select(x => new Classes.File()
            {
                XMLID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                URI = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Uri']"))
            }).Distinct().OrderBy(x => x.XMLID).ToList();

            List<Classes.File> dupeCheckList = files.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Files").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            ProgressPoint = "Skriver nu Billeder til Databasen";

            XMLDBWriteLogic.WriteFiles(dupeCheckList);

            (sender as BackgroundWorker).ReportProgress(16);
        }

        private void ReadMainCategoriesFromXML(object sender, DoWorkEventArgs e)
        {
            ProgressPoint = "Læser nu Hovedkategorier fra XML Filen";

            XDocument xmlDocument = XDocument.Load(FullPathAndFileName);

            List<MainCategory> mainCategories = xmlDocument.XPathSelectElements("//*[name()='MainCategory']").Select(x => new MainCategory()
            {
                ID = XMLSortingLogic.TryToConvertNodeValueToInt(x.XPathSelectElement("./*[name()='Id']")),
                Name = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']"))
            }).Distinct().OrderBy(x => x.ID).ToList();

            List<MainCategory> dupeCheckList = mainCategories.Where(x => !XMLDBReadLogic.DupeCheckList("ID", "MainCategories").Contains(x.ID.Value)).ToList(); // Removes any dupes found already in the DataBase

            ProgressPoint = "Skriver nu Hovedkategorier til Databasen";

            XMLDBWriteLogic.WriteMainCategories(dupeCheckList);

            (sender as BackgroundWorker).ReportProgress(16);
        }

        private void ReadProductsFromXML(object sender, DoWorkEventArgs e)
        {
            ProgressPoint = "Læser nu Produkter fra XML Filen";

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
                CanonicalUrl = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='CanonicalUrl']")),
                Website = XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement(".//*[name()='Url']")),

                Categories = x.XPathSelectElements(".//*[name()='Category']").Select(y => new Category()
                {
                    ID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
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
                    ID = XMLSortingLogic.TryToConvertNodeValueToInt(y.XPathSelectElement("./*[name()='Id']"))
                }).FirstOrDefault(),

                OpeningHours = x.XPathSelectElements(".//*[name()='Period']").Select(y => new OpeningHour()
                {
                    StartDate = XMLSortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement(".//*[name()='StartDate']")),
                    EndDate = XMLSortingLogic.TryToConvertNodeValueToDateTime(x.XPathSelectElement(".//*[name()='EndDate']")),
                    StartTime = XMLSortingLogic.TryToConvertNodeValueToTime(x.XPathSelectElement(".//*[name()='StartTime']")),
                    EndTime = XMLSortingLogic.TryToConvertNodeValueToTime(x.XPathSelectElement(".//*[name()='EndTime']")),

                    Monday = bool.Parse(x.XPathSelectElement(".//*[name()='Monday']").Value),
                    Tuesday = bool.Parse(x.XPathSelectElement(".//*[name()='Tuesday']").Value),
                    Wednesday = bool.Parse(x.XPathSelectElement(".//*[name()='Wednesday']").Value),
                    Thursday = bool.Parse(x.XPathSelectElement(".//*[name()='Thursday']").Value),
                    Friday = bool.Parse(x.XPathSelectElement(".//*[name()='Friday']").Value),
                    Saturday = bool.Parse(x.XPathSelectElement(".//*[name()='Saturday']").Value),
                    Sunday = bool.Parse(x.XPathSelectElement(".//*[name()='Sunday']").Value)
                }).FirstOrDefault(),

                UserID = ReadActorFromXML(XMLSortingLogic.TryToConvertNodeValueToString(x.XPathSelectElement("./*[name()='Name']")))
            }).OrderBy(x => x.XMLID).ToList();

            List<Classes.Product> dupeCheckList = products.Where(x => !XMLDBReadLogic.DupeCheckList("XMLID", "Products").Contains(x.XMLID.Value)).ToList(); // Removes any dupes found already in the DataBase

            ProgressPoint = "Skriver nu Produkter til Databasen";

            XMLDBWriteLogic.WriteProducts(dupeCheckList);

            productList = dupeCheckList.ToList();

            (sender as BackgroundWorker).ReportProgress(20);
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

        private void ReadRelFilesFromXML(object sender, DoWorkEventArgs e)
        {
            ProgressPoint = "Skriver nu Billed Relationer til Produkter i Databasen";

            XMLDBWriteLogic.WriteRelFiles(productList);

            (sender as BackgroundWorker).ReportProgress(16);
        }

        private void ProgressBarPercentage(object sender, ProgressChangedEventArgs e)
        {
            readerWriterLockSlim.EnterWriteLock();

            for (int i = 0; i < e.ProgressPercentage; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    ProgressPercentage++;
                }); // This is needed, so that another Thread can write to this varible
            }

            readerWriterLockSlim.ExitWriteLock();
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
