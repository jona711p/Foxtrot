using System;

namespace Classes
{
    public class Category : IEquatable<Category> // Used by "Distinct()" to find dupes in the list
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public bool Equals(Category other) // Checks if the same "ID" and "Name" already exists in the list
        {
            if (ID == other.ID && Name == other.Name)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode() // Checks if the same "ID" and "Name" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();
            int hashName = Name == null ? 0 : Name.GetHashCode();

            return hashID ^ hashName;
        }
    }

    public class MainCategory : Category, IEquatable<MainCategory> // Used by "Distinct()" to find dupes in the list
    {
        public bool Equals(MainCategory other) // Checks if the same "ID" and "Name" already exists in the list
        {
            if (ID == other.ID && Name == other.Name)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode() // Checks if the same "ID" and "Name" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();
            int hashName = Name == null ? 0 : Name.GetHashCode();

            return hashID ^ hashName;
        }
    }
}
