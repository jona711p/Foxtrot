
namespace Classes
{
    public class Event : Product
    {
        public int? ID { get; set; }
        public int? ProductID { get; set; }

        public Event()
        {
            Availability = true;
        }
    }
}