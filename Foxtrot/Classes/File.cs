using System;

namespace Classes
{
    public class File : IEquatable<File>
    {
        public int? ID { get; set; }
        public string URI { get; set; }

        public bool Equals(File other) // Checks if the same "ID" already exists in the list
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
