using System.ComponentModel.DataAnnotations;

namespace practice.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public int Number { get; set; }

        public string? Description { get; set; }

        public AnswerType AnswerType { get; set; }
        public bool IsScoring { get; set; }
        public int? MaxScore { get; set; }

        public int TestId { get; set; }

        // Навигация
        public Test Test { get; set; } = null!;
        public List<Answer> Answers { get; set; } = new();
        public List<UserAttemptAnswer> UserAttemptAnswers { get; set; } = new();
    }
}