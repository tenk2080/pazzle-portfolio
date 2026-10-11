using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        // Навигация
        public Student? Student { get; set; }
    }
}