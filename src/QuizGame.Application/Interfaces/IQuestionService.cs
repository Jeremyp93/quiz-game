using QuizGame.Application.DTOs;
using QuizGame.Domain.Enums;

namespace QuizGame.Application.Interfaces;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetAllQuestionsAsync();
    Task<List<QuestionDto>> GetFilteredQuestionsAsync(QuestionType? type, int? difficulty, bool? isActive, string? searchText);
    Task<QuestionDto?> GetQuestionByIdAsync(Guid id);
    Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createDto);
    Task<QuestionDto?> UpdateQuestionAsync(Guid id, CreateQuestionDto updateDto);
    Task<bool> DeleteQuestionAsync(Guid id);
    Task<bool> ToggleActiveAsync(Guid id);

    // Phase 1 (Fast Buzzer)
    Task<QuestionDto?> GetRandomRegularQuestionAsync(Guid? excludeQuestionId = null);

    // Phase 2 (List)
    Task<QuestionDto?> GetRandomListQuestionAsync(Guid? excludeQuestionId = null);
}
