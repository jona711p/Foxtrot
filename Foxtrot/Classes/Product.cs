using System;
using System.Collections.Generic;
using System.Data;

namespace Foxtrot.Classes
{
    public class Product 
    {
        public int? ID { get; set;  }
        public int? XMLID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City Cities { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        public List<int?> ContactPhone { get; set; }
        public List<string> ContactEmail { get; set; }
        public List<int?> ContactFax { get; set; }

        public DateTime? CreationDate { get; set; }
        public List<OpeningHour> OpeningHours { get; set; }
        public bool Availability { get; set; }

        public string Description { get; set; } //Text
        public List<ExtraDescription> ExtraDescription { get; set; }

        public string Website { get; set; }
        public string CanonicalUrl { get; set; }

        public MainCategory MainCategories { get; set; }
        public Category Categories { get; set; }

        public List<File> Files { get; set; }

        public int? UserID { get; set; }

        public List<CombiProduct> CombiProducts { get; set; }
        public List<Event> Events { get; set; }

        public float? Price { get; set; }

        public DataTable ProductTable { get; set; }

        public Product()
        {
            Availability = true;
        }
    }

    public class ExtraDescription
    {
        public string Description { get; set; }
    }
}
