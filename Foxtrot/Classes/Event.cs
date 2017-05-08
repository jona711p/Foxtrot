
namespace Classes
{
    public class Event : Product
    {
        public int? ProductID { get; set; }

        public Event()
        {
            Availability = true;
        }
    }
}