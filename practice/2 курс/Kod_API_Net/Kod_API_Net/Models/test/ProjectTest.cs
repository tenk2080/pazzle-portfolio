namespace practice.Models
{
    public class ProjectTest
    {
        public int ProjectId { get; set; }
        public int TestId { get; set; }
        public DateTimeOffset AssignedAt { get; set; }

        public Project Project { get; set; } = null!;
        public Test Test { get; set; } = null!;
    }
}
