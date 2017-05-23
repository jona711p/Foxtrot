using System;
using System.Collections.Generic;
using System.Data;

namespace Foxtrot.Classes
{
    public class CombiProduct
    {
        public int? ID { get; set; }//auto
        public DateTime CreationDate { get; set; }//auto
        public string Name { get; set; }
        public bool Availability { get; set; }
        public List<int?> ProductID { get; set; }//list
        public float? PackagePrice { get; set; } //auto
        public int? UserID { get; set; }//auto
        public DataTable CombiProductTable { get; set; }

        public CombiProduct()
        {
            Availability = true;
        }
    }
}
