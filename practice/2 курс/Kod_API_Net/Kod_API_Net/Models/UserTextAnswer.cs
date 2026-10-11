using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class UserTextAnswer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(4000)]
        public string TextAnswer { get; set; } = string.Empty;

        public int UserAttemptAnswerId { get; set; }

        // Навигация
        public UserAttemptAnswer UserAttemptAnswer { get; set; } = null!;
    }
}
