using System;
using System.Collections.Generic;

namespace Foxtrot.Classes
{
    public class CombiProduct
    {
        public int? ID { get; set; }//auto
        public DateTime CreationDate { get; set; }//auto
        public string Name { get; set; }
        public bool Availability { get; set; }
        public List<int?> ProductID { get; set; }//list
        public int? UserID { get; set; }//auto
        public float? PackagePrice { get; set; } //auto

        public CombiProduct()
        {
            Availability = true;
        }
    }
}
