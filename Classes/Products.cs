using System;

namespace Classes
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public class Products
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public DateTime CreationDate { get; set; }
        public int ProductOwner_ID { get; set; }
        public int Category_ID { get; set; }
        public DateTime? Period { get; set; }
        public DateTime? OpeningHours { get; set; }
        public string Description { get; set; }
        public string ExtraDescription { get; set; }
        public int? Picture_ID { get; set; }
        public string Website { get; set; }
        public int? ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int? ContactFax { get; set; }
        public float? Price { get; set; }
    }
}