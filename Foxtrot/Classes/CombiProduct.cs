using System;

namespace Classes
{
    public class CombiProducts
    {
        public int ID { get; set; }
        public DateTime Period { get; set; }
        public int Product_ID { get; set; }
        public int ProductOwner_ID { get; set; }
        public DateTime CreationDate { get; set; }
        public float? PackagePrice { get; set; }
        public bool Availability { get; set; }
    }
}