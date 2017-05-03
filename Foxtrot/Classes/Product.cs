using System;
using System.Collections.Generic;

namespace Classes
{
    public class Product
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public List<int?> ContactPhone { get; set; }
        public List<string> ContactEmail { get; set; }
        public List<int?> ContactFax { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<OpeningHour> OpeningHours { get; set; }
        public float? Price { get; set; }
        public string Description { get; set; } //Text
        public List<ExtraDesription> ExtraDesription { get; set; }
        public string Website { get; set; }
        public string CanonicalUrl { get; set; }
        public List<File> Files { get; set; }
        public City Cities { get; set; }
        public MainCategory MainCategories { get; set; }
        public Category Categories { get; set; }
        public int? ActorID { get; set; }
        public List<Event> Event { get; set; }
    }

    public class ExtraDesription
    {
        public string Description { get; set; }
    }
}
