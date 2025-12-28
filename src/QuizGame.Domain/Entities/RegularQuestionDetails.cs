namespace QuizGame.Domain.Entities;

public class RegularQuestionDetails
{
    public Guid QuestionId { get; set; }
    public string AnswerFr { get; set; } = string.Empty;
    public string AnswerNl { get; set; } = string.Empty;

    // Navigation property
    public Question Question { get; set; } = null!;
}
