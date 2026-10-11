using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        public string Email => User?.Email ?? string.Empty;

        [Required]
        public string VkProfileLink { get; set; } = string.Empty;

        // Навигация
        public User User { get; set; } = null!;
        public List<StudentGroup> StudentGroups { get; set; } = new();
        public List<StudentTest> StudentTests { get; set; } = new();
        public List<Attempt> Attempts { get; set; } = new();
        public List<TestResult> TestResults { get; set; } = new();
    }
}

