namespace QuizGame.Application.DTOs;

public class ThemeDto
{
    public Guid Id { get; set; }
    public string NameFr { get; set; } = string.Empty;
    public string NameNl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Icon { get; set; } = "📚";
    public bool IsActive { get; set; }
    public int? SortOrder { get; set; }
    public int QuestionCount { get; set; } // For displaying usage count
}

public class CreateThemeDto
{
    public string NameFr { get; set; } = string.Empty;
    public string NameNl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Icon { get; set; } = "📚";
    public bool IsActive { get; set; } = true;
    public int? SortOrder { get; set; }
}
