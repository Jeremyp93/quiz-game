namespace QuizGame.Domain.Entities;

public class ListQuestionAnswer
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }

    public string AnswerFr { get; set; } = string.Empty;
    public string AnswerNl { get; set; } = string.Empty;
    public string? AltSpellings { get; set; }

    // Navigation property
    public Question Question { get; set; } = null!;
}
