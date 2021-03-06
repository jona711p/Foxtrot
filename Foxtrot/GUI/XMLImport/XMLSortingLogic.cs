﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Foxtrot.Classes;

namespace Foxtrot.GUI.XMLImport
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    // Class to sort different datas from a XML file
    public class XMLSortingLogic
    {
        public static DateTime? TryToConvertNodeValueToDateTime(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return node == null ? null : (DateTime?)DateTime.Parse(node.Value);
        }

        public static float? TryToConvertNodeValueToFloat(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And with "." replaced by ",", because float needs "," to read it properly
        {
            return node == null || node.Value.Length == 0 ? null : (float?)float.Parse(node.Value.Replace('.', ','));
        }

        public static int TryToConvertNodeValueToInt(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format
        {
            return (int) (node == null ? null : (int?)int.Parse(node.Value));
        }

        public static List<int?> TryToConvertNodeValueToIntList(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And if there is more than one number seperated by "/". It also removes "+45" and spaces between numbers, so that we end up with 8 digits!
        {
            List<int?> output = new List<int?>();

            if (node == null || node.Value.Length == 0)
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
            if (node == null || node.Value.Length == 0)
            {
                return null;
            }

            if (node.Value.Contains("http://") || node.Value.Contains("www."))
            {
                return node.Value.ToLower();
            }

            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(node.Value.ToLower());
        }

        public static List<string> TryToConvertNodeValueToStringList(XElement node) // If the output from the XML is "Empty" or "NULL" it returns NULL, else it returns the right value in the right format. And if there is more than one string seperated by "/".
        {
            List<string> output = new List<string>();

            if (node == null || node.Value.Length == 0)
            {
                return null;
            }

            else
            {
                string[] moreThanOneEmails = node.Value.Split('/');

                foreach (string emails in moreThanOneEmails)
                {
                    output.Add(emails.ToLower());
                }
            }

            return output;
        }

        public static DateTime? TryToConvertNodeValueToTime(XElement node) // If the output from the XML is "Empty", "NULL" or contains "S" it returns NULL, else it returns the right value in the right format, and removes "P", "T" and "H" and only gets the timed format
        {
            if (node == null || node.Value.Length == 0 || node.Value.Contains("S"))
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

        public static string TryToConvertNodeValueToStringBuilder(List<ExtraDescription> ExtraDesriptions)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ExtraDescription extraDesription in ExtraDesriptions)
            {
                sb.AppendLine(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(extraDesription.Description.ToLower()));
            }

            return sb.ToString();
        }
    }
}