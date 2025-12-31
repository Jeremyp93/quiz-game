using QuizGame.Domain.Enums;

namespace QuizGame.Application.DTOs;

public class CreateQuestionDto
{
    public QuestionType Type { get; set; }
    public int Difficulty { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsPriority { get; set; } = false;
    public string? Category { get; set; }
    public string? Tags { get; set; }
    public string TextFr { get; set; } = string.Empty;
    public string TextNl { get; set; } = string.Empty;
    public Guid? ThemeId { get; set; }

    public RegularQuestionDetailsDto? RegularDetails { get; set; }
    public McqQuestionDetailsDto? McqDetails { get; set; }
    public List<ListQuestionAnswerDto>? ListAnswers { get; set; }
}
