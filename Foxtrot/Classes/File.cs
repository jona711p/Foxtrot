using System;

namespace Classes
{
    public class File : IEquatable<File>
    {
        public int? ID { get; set; }
        public string Uri { get; set; }

        public bool Equals(File other) // Checks if the same "ID" and "Uri" already exists in the list
        {
            if (ID == other.ID && Uri == other.Uri)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
            // Checks if the same "ID" and "Uri" with the equal HASHCODE already exists in the list
        {
            int hashID = ID == null ? 0 : ID.GetHashCode();
            int hashUri = Uri == null ? 0 : Uri.GetHashCode();

            return hashID ^ hashUri;
        }
    }
}
