using System;

namespace Foxtrot.Classes
{
    public class City : IEquatable<City> // Used by "Distinct()" to find dupes in the list
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? PostalCode { get; set; }

        public bool Equals(City other) // Checks if the same "ID" and "Name" already exists in the list
        {
            if (ID == other.ID && Name == other.Name && PostalCode == other.PostalCode)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode() // Checks if the same "ID", "Name" and "PostalCode" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();
            int hashName = Name == null ? 0 : Name.GetHashCode();
            int hashPostalCode = PostalCode == null ? 0 : PostalCode.GetHashCode();

            return hashID ^ hashName ^ hashPostalCode;
        }
    }
}
