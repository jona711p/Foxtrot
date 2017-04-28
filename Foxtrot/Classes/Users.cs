namespace Classes
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public class Users
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? WorkPhone { get; set; }
        public string WorkEmail { get; set; }
        public int? WorkFax { get; set; }
        public int Permission { get; set; }
    }

    public class Administrators : Users
    {
        public Administrators()
        {
            Permission = 1;
        }
    }

    public class Actors : Users
    {
        public Actors()
        {
            Permission = 2;
        }

        public string CompanyName { get; set; }
    }
}
