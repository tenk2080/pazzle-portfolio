namespace practice.Models
{
    public class UserAttemptAnswer
    {
        public int Id { get; set; }

        public bool IsCorrect { get; set; }
        public int ScoreAwarded { get; set; }

        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedAnswerId { get; set; }

        // Навигация
        public Attempt Attempt { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public Answer? SelectedAnswer { get; set; }
        public List<UserSelectedOption> SelectedOptions { get; set; } = new();
        public UserTextAnswer? UserTextAnswer { get; set; }
    }
}
