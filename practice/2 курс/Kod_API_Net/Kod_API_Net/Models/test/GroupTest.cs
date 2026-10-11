namespace practice.Models
{
    public class GroupTest
    {
        public int GroupId { get; set; }
        public int TestId { get; set; }
        public DateTimeOffset AssignedAt { get; set; }

        public Group Group { get; set; } = null!;
        public Test Test { get; set; } = null!;
    }
}
