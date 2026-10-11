namespace practice.Models
{
    public class StudentGroup
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateTimeOffset EnrolledAt { get; set; }
        public bool IsActive { get; set; } = true;

        public Student Student { get; set; } = null!;
        public Group Group { get; set; } = null!;
    }
}
