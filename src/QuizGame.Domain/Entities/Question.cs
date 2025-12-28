using QuizGame.Domain.Enums;

namespace QuizGame.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public QuestionType Type { get; set; }
    public int Difficulty { get; set; }
    public bool IsActive { get; set; }
    public string? Category { get; set; }
    public string? Tags { get; set; }

    // Bilingual base fields
    public string TextFr { get; set; } = string.Empty;
    public string TextNl { get; set; } = string.Empty;

    // Theme relationship (optional for Regular/List, required for MCQ in Phase 3)
    public Guid? ThemeId { get; set; }
    public Theme? Theme { get; set; }

    // Navigation properties (1:1 relationships, only one will be populated based on Type)
    public RegularQuestionDetails? RegularDetails { get; set; }
    public McqQuestionDetails? McqDetails { get; set; }

    // Navigation property (1:many for List type)
    public ICollection<ListQuestionAnswer> ListAnswers { get; set; } = new List<ListQuestionAnswer>();
}
