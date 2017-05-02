using System;

namespace Classes
{
    public class OpeningHours : IEquatable<OpeningHours>
    {
        public int? ID { get; set; }
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

        public bool Equals(OpeningHours other) // Checks if the same "ID" already exists in the list
        {
            if (ID == other.ID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        // Checks if the same "ID" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();

            return hashID;
        }
    }
}
