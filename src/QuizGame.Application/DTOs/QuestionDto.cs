using QuizGame.Domain.Enums;

namespace QuizGame.Application.DTOs;

public class QuestionDto
{
    public Guid Id { get; set; }
    public QuestionType Type { get; set; }
    public int Difficulty { get; set; }
    public bool IsActive { get; set; }
    public string? Category { get; set; }
    public string? Tags { get; set; }
    public string TextFr { get; set; } = string.Empty;
    public string TextNl { get; set; } = string.Empty;

    public RegularQuestionDetailsDto? RegularDetails { get; set; }
    public McqQuestionDetailsDto? McqDetails { get; set; }
    public List<ListQuestionAnswerDto>? ListAnswers { get; set; }
}

public class RegularQuestionDetailsDto
{
    public string AnswerFr { get; set; } = string.Empty;
    public string AnswerNl { get; set; } = string.Empty;
}

public class McqQuestionDetailsDto
{
    public string ChoiceAFr { get; set; } = string.Empty;
    public string ChoiceANl { get; set; } = string.Empty;
    public string ChoiceBFr { get; set; } = string.Empty;
    public string ChoiceBNl { get; set; } = string.Empty;
    public string ChoiceCFr { get; set; } = string.Empty;
    public string ChoiceCNl { get; set; } = string.Empty;
    public McqChoice CorrectChoice { get; set; }
}

public class ListQuestionAnswerDto
{
    public Guid Id { get; set; }
    public string AnswerFr { get; set; } = string.Empty;
    public string AnswerNl { get; set; } = string.Empty;
    public string? AltSpellings { get; set; }
}
