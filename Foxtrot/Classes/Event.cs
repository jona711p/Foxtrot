
namespace Classes
{
    public class Event : Product
    {
        public int? ID { get; set; }
        public bool Availability { get; set; }

        public Event()
        {
            Availability = true;
        }
    }
}