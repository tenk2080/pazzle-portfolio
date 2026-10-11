using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TestType Type { get; set; }
        public bool IsPublic { get; set; }
        public bool IsRepeatable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
        public DateTimeOffset Deadline { get; set; }

        public int? DurationMinutes { get; set; }
        public int? PassingScore { get; set; }
        public int? MaxAttempts { get; set; }

        // Навигация
        public List<Question> Questions { get; set; } = new();
        public List<Attempt> Attempts { get; set; } = new();
        public List<TestResult> TestResults { get; set; } = new();
        public List<StudentTest> StudentTests { get; set; } = new();
        public List<GroupTest> GroupTests { get; set; } = new();
        public List<ProjectTest> ProjectTests { get; set; } = new();
        public List<CourseTest> CourseTests { get; set; } = new();
        public List<DirectionTest> DirectionTests { get; set; } = new();
    }
}