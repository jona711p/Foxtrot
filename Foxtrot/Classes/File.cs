using System;

namespace Classes
{
    public class File : IEquatable<File>
    {
        public int? ID { get; set; }
        public string URI { get; set; }

        public bool Equals(File other) // Checks if the same "ID" and "URI" already exists in the list
        {
            if (ID == other.ID && URI == other.URI)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
            // Checks if the same "ID" and "Uri" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();
            int hashURI = URI == null ? 0 : URI.GetHashCode();

            return hashID ^ hashURI;
        }
    }
}
