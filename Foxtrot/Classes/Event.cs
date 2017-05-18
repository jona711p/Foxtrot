using System;
using System.Collections.Generic;
using System.Data;

namespace Foxtrot.Classes
{
    public class Event : Product
    {
        public int? ProductID { get; set; }
        public DataTable EventTable { get; set; }

        public Event()
        {
            Availability = true;
        }
    }
}