using System.Collections.Generic;

namespace Classes
{
    public class User
    {
        public int? ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? WorkPhone { get; set; }
        public string WorkEmail { get; set; }
        public int? WorkFax { get; set; }
        public Dictionary<string, int> AdminActorDictionary { get; set; }
    }

    public class Administrator : User
    {
        public int? User_ID { get; set; }
        public int Permission { get; set; }
        public Administrator()
        {
            Permission = 1;
        }
    }

    public class Actor : User
    {
        public int? User_ID { get; set; }
        public int Permission { get; set; }
        public string CompanyName { get; set; }
        public Actor()
        {
            Permission = 2;
        }
    }
}
