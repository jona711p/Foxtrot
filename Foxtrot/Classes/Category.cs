using System;

namespace Foxtrot.Classes
{
    public class Category : IEquatable<Category> // Used by "Distinct()" to find dupes in the list
    {
        public int? ID { get; set; }
        public int? XMLID { get; set; }
        public string Name { get; set; }

        public bool Equals(Category other) // Checks if the same "XMLID" already exists in the list
        {
            if (XMLID == other.XMLID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode() // Checks if the same "XMLID" with the equal HASHCODE already exists in the list
        {
            int hashXMLID = XMLID == null ? 0 : XMLID.GetHashCode();

            return hashXMLID;
        }
    }

    public class MainCategory : Category, IEquatable<MainCategory> // Used by "Distinct()" to find dupes in the list
    {
        public bool Equals(MainCategory other) // Checks if the same "XMLID" already exists in the list
        {
            if (XMLID == other.XMLID)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode() // Checks if the same "XMLID" with the equal HASHCODE already exists in the list
        {
            int hashXMLID = XMLID == null ? 0 : XMLID.GetHashCode();

            return hashXMLID;
        }
    }
}
