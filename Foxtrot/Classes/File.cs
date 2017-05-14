using System;

namespace Foxtrot.Classes
{
    public class File : IEquatable<File>
    {
        public int? ID { get; set; }
        public int? XMLID { get; set; }
        public string URI { get; set; }

        public bool Equals(File other) // Checks if the same "XMLID" already exists in the list
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
