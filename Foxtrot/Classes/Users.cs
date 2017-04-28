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
    }

    public class Administrators : Users
    {
        public int User_ID { get; set; }
        public int Permission { get; set; }

        public Administrators()
        {
            Permission = 1;
        }
    }

    public class Actors : Users
    {
        public int User_ID { get; set; }
        public int Permission { get; set; }
        public string CompanyName { get; set; }

        public Actors()
        {
            Permission = 2;
        }
    }
}
