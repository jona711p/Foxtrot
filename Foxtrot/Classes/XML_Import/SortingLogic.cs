using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Classes
{
    public class SortingLogic
    {
        public static DateTime? TryToConvertNodeValueToDateTime(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return node == null ? null : (DateTime?)DateTime.Parse(node.Value);
        }

        public static float? TryToConvertNodeValueToFloat(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And with "." replaced by ",", because float needs "," to read it properly
        {
            return node == null || node.Value.Equals("") ? null : (float?)float.Parse(node.Value.Replace('.', ','));
        }

        public static int TryToConvertNodeValueToInt(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return (int) (node == null ? null : (int?)int.Parse(node.Value));
        }

        public static List<int?> TryToConvertNodeValueToIntList(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And if there is more than one number seperated by "/". It also removes "+45" and spaces between numbers, so that we end up with 8 digits!
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

        public static string TryToConvertNodeValueToString(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return node == null || node.Value.Equals("") ? null : node.Value;
        }

        public static List<string> TryToConvertNodeValueToStringList(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And if there is more than one string seperated by "/".
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

        public static DateTime? TryToConvertNodeValueToTime(XElement node) // If the output from the XML is "Empty", "NULL" or contains "S" it returns NULL, else it returns the right value in the right format, and removes "P", "T" and "H" and only gets the timed format
        {
            if (node == null || node.Value.Equals("") || node.Value.Contains("S"))
            {
                return null;
            }

            else
            {
                if (!node.Value.Contains("M"))
                {
                    DateTime timeTD = DateTime.Parse(node.Value.Replace("PT", "").Replace("H", "") + ":00:00");
                    string timeString = timeTD.ToString("HH:mm:ss");
                    return DateTime.Parse(timeString);
                }

                else
                {
                    string[] withMinuts = node.Value.Split('H');
                    DateTime timeTD = DateTime.Parse(withMinuts[0].Replace("PT", "").Replace("H", "") + ":" + withMinuts[1].Replace("M", "") + ":00");
                    string timeString = timeTD.ToString("HH:mm:ss");
                    return DateTime.Parse(timeString);
                }
            }
        }

        public static List<City> DupeChecking(List<City> cities)
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

        public static List<Category> DupeChecking(List<Category> categories)
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

        public static List<File> DupeChecking(List<File> files)
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

        public static List<MainCategory> DupeChecking(List<MainCategory> mainCategories)
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

        public static List<OpeningHour> DupeChecking(List<OpeningHour> openingHours)
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
    }
}