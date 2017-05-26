using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Foxtrot.Classes
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public class City : IEquatable<City>, INotifyPropertyChanged // Used by "Distinct()" to find dupes in the list
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? PostalCode { get; set; }

        private static List<KeyValuePair<int, string>> cityList = new List<KeyValuePair<int, string>>();

        public List<KeyValuePair<int, string>> CityList
        {
            get { return cityList; }
            set { cityList = value; NotifyPropertyChanged(); }
        }

        public bool Equals(City other) // Checks if the same "ID" already exists in the list
        {
            if (ID == other.ID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode() // Checks if the same "ID" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();

            return hashID;
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
