using System;

namespace Foxtrot.Classes
{
    public class OpeningHour : IEquatable<OpeningHour>
    {
        public int? ID { get; set; }
        public int? XMLID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public bool Equals(OpeningHour other) // Checks if the same "XMLID" already exists in the list
        {
            if (XMLID == other.XMLID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        // Checks if the same "ID" with the equal HASHCODE already exists in the list
        {
            int hashXMLID = XMLID == null ? 0 : XMLID.GetHashCode();

            return hashXMLID;
        }
    }
}
