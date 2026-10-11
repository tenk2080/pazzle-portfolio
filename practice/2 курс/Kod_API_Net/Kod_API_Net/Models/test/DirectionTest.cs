namespace practice.Models
{
    public class DirectionTest
    {
        public int DirectionId { get; set; }
        public int TestId { get; set; }
        public DateTimeOffset AssignedAt { get; set; }

        public Direction Direction { get; set; } = null!;
        public Test Test { get; set; } = null!;
    }
}
