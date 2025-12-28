using QuizGame.Domain.Enums;

namespace QuizGame.Domain.Entities;

public class McqQuestionDetails
{
    public Guid QuestionId { get; set; }

    public string ChoiceAFr { get; set; } = string.Empty;
    public string ChoiceANl { get; set; } = string.Empty;

    public string ChoiceBFr { get; set; } = string.Empty;
    public string ChoiceBNl { get; set; } = string.Empty;

    public string ChoiceCFr { get; set; } = string.Empty;
    public string ChoiceCNl { get; set; } = string.Empty;

    public McqChoice CorrectChoice { get; set; }

    // Navigation property
    public Question Question { get; set; } = null!;
}
