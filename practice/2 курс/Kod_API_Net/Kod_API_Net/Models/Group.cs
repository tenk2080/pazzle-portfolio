using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int DirectionId { get; set; }
        public int CourseId { get; set; }
        public int ProjectId { get; set; }

        // Навигация
        public Direction Direction { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Project Project { get; set; } = null!;
        public List<StudentGroup> StudentGroups { get; set; } = new();
        public List<GroupTest> GroupTests { get; set; } = new();
    }
}


