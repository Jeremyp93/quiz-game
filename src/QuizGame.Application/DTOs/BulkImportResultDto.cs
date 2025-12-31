namespace QuizGame.Application.DTOs;

public class BulkImportResultDto
{
    public int TotalQuestions { get; set; }
    public int SuccessCount { get; set; }
    public int FailureCount { get; set; }
    public List<BulkImportError> Errors { get; set; } = new();
    public List<QuestionDto> ImportedQuestions { get; set; } = new();
}

public class BulkImportError
{
    public int QuestionIndex { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string? QuestionTextFr { get; set; }
}
