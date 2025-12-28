namespace QuizGame.Domain.Entities;

public class Theme
{
    public Guid Id { get; set; }
    public string NameFr { get; set; } = string.Empty;
    public string NameNl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // unique internal key e.g. "geo", "music_90s"
    public bool IsActive { get; set; } = true;
    public int? SortOrder { get; set; }

    // Navigation property
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
