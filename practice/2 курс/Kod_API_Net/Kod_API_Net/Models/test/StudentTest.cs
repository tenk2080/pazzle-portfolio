namespace practice.Models
{
    public class StudentTest
    {
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public DateTimeOffset AssignedAt { get; set; }

        public Student Student { get; set; } = null!;
        public Test Test { get; set; } = null!;
    }
}
