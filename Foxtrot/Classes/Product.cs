using System;
using System.Collections.Generic;

namespace Classes
{
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
}
