using System;
using System.Collections.Generic;

namespace Classes
{
    /// <summary>
    /// Jonas, Mikael & Thomas
    /// </summary>
    public class Product
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Actor { get; set; } // Name of product
        public string Address { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public List<int?> Phone { get; set; }
        public List<string> Email { get; set; }
        public List<int?> Fax { get; set; }
        public string CanonicalUrl { get; set; }
        public string Url { get; set; }
        public DateTime? Created { get; set; }
        public string Description { get; set; } //Text
        public List<File> Files { get; set; }
        public City Cities { get; set; }
        public Category Categories { get; set; }
        public MainCategory MainCategories { get; set; }
    }

    public class File
    {
        public int? ID { get; set; }
        public string Uri { get; set; }
    }

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
