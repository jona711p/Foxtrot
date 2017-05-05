﻿using System;

namespace Classes
{
    public class CombiProducts
    {
        public int? ID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Availability { get; set; }
        public int? ProductID { get; set; }
        public int? ActorID { get; set; }
        public float? PackagePrice { get; set; }

        public CombiProducts()
        {
            Availability = true;
        }
    }
}