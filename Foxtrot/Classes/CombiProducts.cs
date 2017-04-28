using System;

namespace Classes
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public class CombiProducts
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public int Product_ID { get; set; }
        public int ProductOwner_ID { get; set; }
        public DateTime CreationDate { get; set; }
        public float? Price { get; set; }
        public DateTime Availability { get; set; }
    }
}