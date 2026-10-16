namespace RaceDayAPI.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int EventID { get; set; }
        public string CategoryName { get; set; } = "";
        public decimal DistanceKm { get; set; }
        public decimal EntryFee { get; set; }
        public int MaximumParticipants { get; set; }

        public Event? Event { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
