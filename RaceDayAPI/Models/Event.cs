
namespace RaceDayAPI.Models
{
    public class Event
    {
        public int EventID { get; set; }

        public int OrganiserID { get; set; }

        public string EventName { get; set; } = "";

        public string EventType { get; set; } = "";

        public DateTime EventDate { get; set; }

        public string Location { get; set; } = "";

        public string? Description { get; set; }

        public string Status { get; set; } = "Upcoming";

        public User? Organiser { get; set; }

        public ICollection<Category> Categories { get; set; }
            = new List<Category>();

        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

        public Weather? Weather { get; set; }

        public Route? Route { get; set; }
    }
}

