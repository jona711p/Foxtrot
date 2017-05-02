using System;

namespace Classes
{
    public class City : IEquatable<City> // Used by "Distinct()" to find dupes in the list
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? PostalCode { get; set; }

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
    }
}
