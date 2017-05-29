using System;
using System.Collections.Generic;

namespace Foxtrot.Classes
{
    /// <summary>
    /// Jonas Lykke og Thomas Nielsen
    /// </summary>
    public class Category : IEquatable<Category> // Used by "Distinct()" to find dupes in the list
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public List<KeyValuePair<int, string>> CategoryList = new List<KeyValuePair<int, string>>();


        public bool Equals(Category other) // Checks if the same "ID" already exists in the list
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

    public class MainCategory : Category, IEquatable<MainCategory> // Used by "Distinct()" to find dupes in the list
    {
        public List<KeyValuePair<int, string>> MainCategoryList = new List<KeyValuePair<int, string>>();

        public bool Equals(MainCategory other) // Checks if the same "ID" already exists in the list
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
