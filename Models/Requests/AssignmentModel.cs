namespace Models.Requests
{
    public class AssignmentModel
    {
        public string? Name { get; set; }
        public int TypeId { get; set; }
        public bool Repeated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Ended { get; set; }
        public string? Description { get; set; }
        public bool Archive { get; set; }
    }
}
