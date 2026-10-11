using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        // Навигация
        public Question Question { get; set; } = null!;
        public List<UserSelectedOption> UserSelectedOptions { get; set; } = new();
    }
}
