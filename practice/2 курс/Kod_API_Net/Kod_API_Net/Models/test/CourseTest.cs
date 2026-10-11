namespace practice.Models
{
    public class CourseTest
    {
        public int CourseId { get; set; }
        public int TestId { get; set; }
        public DateTimeOffset AssignedAt { get; set; }

        public Course Course { get; set; } = null!;
        public Test Test { get; set; } = null!;
    }
}
