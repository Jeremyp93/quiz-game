using Microsoft.EntityFrameworkCore;
using QuizGame.Application.DTOs;
using QuizGame.Application.Interfaces;
using QuizGame.Domain.Entities;
using QuizGame.Domain.Enums;
using QuizGame.Infrastructure.Data;

namespace QuizGame.Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly QuizGameDbContext _context;

    public QuestionService(QuizGameDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await _context.Questions
            .Include(q => q.RegularDetails)
            .Include(q => q.McqDetails)
            .Include(q => q.ListAnswers)
            .Include(q => q.Theme)
            .OrderByDescending(q => q.Id)
            .ToListAsync();

        return questions.Select(MapToDto).ToList();
    }

    public async Task<List<QuestionDto>> GetFilteredQuestionsAsync(
        QuestionType? type,
        int? difficulty,
        bool? isActive,
        string? searchText)
    {
        var query = _context.Questions
            .Include(q => q.RegularDetails)
            .Include(q => q.McqDetails)
            .Include(q => q.ListAnswers)
            .Include(q => q.Theme)
            .AsQueryable();

        if (type.HasValue)
            query = query.Where(q => q.Type == type.Value);

        if (difficulty.HasValue)
            query = query.Where(q => q.Difficulty == difficulty.Value);

        if (isActive.HasValue)
            query = query.Where(q => q.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            var search = searchText.ToLower();
            query = query.Where(q =>
                q.TextFr.ToLower().Contains(search) ||
                q.TextNl.ToLower().Contains(search));
        }

        var questions = await query.OrderByDescending(q => q.Id).ToListAsync();
        return questions.Select(MapToDto).ToList();
    }

    public async Task<QuestionDto?> GetQuestionByIdAsync(Guid id)
    {
        var question = await _context.Questions
            .Include(q => q.RegularDetails)
            .Include(q => q.McqDetails)
            .Include(q => q.Theme)
            .Include(q => q.ListAnswers)
            .FirstOrDefaultAsync(q => q.Id == id);

        return question == null ? null : MapToDto(question);
    }

    public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createDto)
    {
        ValidateCreateDto(createDto);

        var question = new Question
        {
            Id = Guid.NewGuid(),
            Type = createDto.Type,
            Difficulty = createDto.Difficulty,
            IsActive = createDto.IsActive,
            Category = createDto.Category,
            Tags = createDto.Tags,
            TextFr = createDto.TextFr,
            TextNl = createDto.TextNl
        };

        _context.Questions.Add(question);

        // Add type-specific details
        switch (createDto.Type)
        {
            case QuestionType.Regular:
                if (createDto.RegularDetails == null)
                    throw new ArgumentException("Regular details required for Regular question type");

                _context.RegularQuestionDetails.Add(new RegularQuestionDetails
                {
                    QuestionId = question.Id,
                    AnswerFr = createDto.RegularDetails.AnswerFr,
                    AnswerNl = createDto.RegularDetails.AnswerNl
                });
                break;

            case QuestionType.Mcq:
                if (createDto.McqDetails == null)
                    throw new ArgumentException("MCQ details required for MCQ question type");

                _context.McqQuestionDetails.Add(new McqQuestionDetails
                {
                    QuestionId = question.Id,
                    ChoiceAFr = createDto.McqDetails.ChoiceAFr,
                    ChoiceANl = createDto.McqDetails.ChoiceANl,
                    ChoiceBFr = createDto.McqDetails.ChoiceBFr,
                    ChoiceBNl = createDto.McqDetails.ChoiceBNl,
                    ChoiceCFr = createDto.McqDetails.ChoiceCFr,
                    ChoiceCNl = createDto.McqDetails.ChoiceCNl,
                    CorrectChoice = createDto.McqDetails.CorrectChoice
                });
                break;

            case QuestionType.List:
                if (createDto.ListAnswers != null && createDto.ListAnswers.Any())
                {
                    foreach (var answer in createDto.ListAnswers)
                    {
                        _context.ListQuestionAnswers.Add(new ListQuestionAnswer
                        {
                            Id = Guid.NewGuid(),
                            QuestionId = question.Id,
                            AnswerFr = answer.AnswerFr,
                            AnswerNl = answer.AnswerNl,
                            AltSpellings = answer.AltSpellings
                        });
                    }
                }
                break;
        }

        await _context.SaveChangesAsync();

        return (await GetQuestionByIdAsync(question.Id))!;
    }

    public async Task<QuestionDto?> UpdateQuestionAsync(Guid id, CreateQuestionDto updateDto)
    {
        ValidateCreateDto(updateDto);

        var question = await _context.Questions
            .Include(q => q.RegularDetails)
            .Include(q => q.McqDetails)
            .Include(q => q.ListAnswers)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
            return null;

        // Update base fields
        question.Type = updateDto.Type;
        question.Difficulty = updateDto.Difficulty;
        question.IsActive = updateDto.IsActive;
        question.Category = updateDto.Category;
        question.Tags = updateDto.Tags;
        question.TextFr = updateDto.TextFr;
        question.TextNl = updateDto.TextNl;

        // Remove old type-specific details
        if (question.RegularDetails != null)
            _context.RegularQuestionDetails.Remove(question.RegularDetails);
        if (question.McqDetails != null)
            _context.McqQuestionDetails.Remove(question.McqDetails);
        if (question.ListAnswers.Any())
            _context.ListQuestionAnswers.RemoveRange(question.ListAnswers);

        // Add new type-specific details
        switch (updateDto.Type)
        {
            case QuestionType.Regular:
                if (updateDto.RegularDetails == null)
                    throw new ArgumentException("Regular details required");

                _context.RegularQuestionDetails.Add(new RegularQuestionDetails
                {
                    QuestionId = question.Id,
                    AnswerFr = updateDto.RegularDetails.AnswerFr,
                    AnswerNl = updateDto.RegularDetails.AnswerNl
                });
                break;

            case QuestionType.Mcq:
                if (updateDto.McqDetails == null)
                    throw new ArgumentException("MCQ details required");

                _context.McqQuestionDetails.Add(new McqQuestionDetails
                {
                    QuestionId = question.Id,
                    ChoiceAFr = updateDto.McqDetails.ChoiceAFr,
                    ChoiceANl = updateDto.McqDetails.ChoiceANl,
                    ChoiceBFr = updateDto.McqDetails.ChoiceBFr,
                    ChoiceBNl = updateDto.McqDetails.ChoiceBNl,
                    ChoiceCFr = updateDto.McqDetails.ChoiceCFr,
                    ChoiceCNl = updateDto.McqDetails.ChoiceCNl,
                    CorrectChoice = updateDto.McqDetails.CorrectChoice
                });
                break;

            case QuestionType.List:
                if (updateDto.ListAnswers != null && updateDto.ListAnswers.Any())
                {
                    foreach (var answer in updateDto.ListAnswers)
                    {
                        _context.ListQuestionAnswers.Add(new ListQuestionAnswer
                        {
                            Id = Guid.NewGuid(),
                            QuestionId = question.Id,
                            AnswerFr = answer.AnswerFr,
                            AnswerNl = answer.AnswerNl,
                            AltSpellings = answer.AltSpellings
                        });
                    }
                }
                break;
        }

        await _context.SaveChangesAsync();

        return await GetQuestionByIdAsync(id);
    }

    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return false;

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return false;

        question.IsActive = !question.IsActive;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<QuestionDto?> GetRandomRegularQuestionAsync(Guid? excludeQuestionId = null)
    {
        // Get all active Regular questions
        var query = _context.Questions
            .Include(q => q.RegularDetails)
            .Where(q => q.Type == QuestionType.Regular && q.IsActive);

        // Exclude the last question if provided and multiple questions exist
        if (excludeQuestionId.HasValue)
        {
            var totalCount = await query.CountAsync();
            if (totalCount > 1)
            {
                query = query.Where(q => q.Id != excludeQuestionId.Value);
            }
        }

        var questions = await query.ToListAsync();

        if (!questions.Any())
            return null;

        // Select random question
        var randomIndex = Random.Shared.Next(questions.Count);
        return MapToDto(questions[randomIndex]);
    }

    public async Task<QuestionDto?> GetRandomListQuestionAsync(Guid? excludeQuestionId = null)
    {
        // Get all active List questions
        var query = _context.Questions
            .Include(q => q.ListAnswers)
            .Where(q => q.Type == QuestionType.List && q.IsActive);

        // Exclude the last question if provided and multiple questions exist
        if (excludeQuestionId.HasValue)
        {
            var totalCount = await query.CountAsync();
            if (totalCount > 1)
            {
                query = query.Where(q => q.Id != excludeQuestionId.Value);
            }
        }

        var questions = await query.ToListAsync();

        if (!questions.Any())
            return null;

        // Select random question
        var randomIndex = Random.Shared.Next(questions.Count);
        return MapToDto(questions[randomIndex]);
    }

    private static void ValidateCreateDto(CreateQuestionDto dto)
    {
        if (dto.Difficulty < 1 || dto.Difficulty > 3)
            throw new ArgumentException("Difficulty must be between 1 and 3");

        if (string.IsNullOrWhiteSpace(dto.TextFr))
            throw new ArgumentException("French text is required");

        if (string.IsNullOrWhiteSpace(dto.TextNl))
            throw new ArgumentException("Dutch text is required");
    }

    private static QuestionDto MapToDto(Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            Type = question.Type,
            Difficulty = question.Difficulty,
            IsActive = question.IsActive,
            Category = question.Category,
            Tags = question.Tags,
            TextFr = question.TextFr,
            TextNl = question.TextNl,
            ThemeId = question.ThemeId,
            Theme = question.Theme == null ? null : new ThemeDto
            {
                Id = question.Theme.Id,
                NameFr = question.Theme.NameFr,
                NameNl = question.Theme.NameNl,
                Code = question.Theme.Code,
                IsActive = question.Theme.IsActive,
                SortOrder = question.Theme.SortOrder
            },
            RegularDetails = question.RegularDetails == null ? null : new RegularQuestionDetailsDto
            {
                AnswerFr = question.RegularDetails.AnswerFr,
                AnswerNl = question.RegularDetails.AnswerNl
            },
            McqDetails = question.McqDetails == null ? null : new McqQuestionDetailsDto
            {
                ChoiceAFr = question.McqDetails.ChoiceAFr,
                ChoiceANl = question.McqDetails.ChoiceANl,
                ChoiceBFr = question.McqDetails.ChoiceBFr,
                ChoiceBNl = question.McqDetails.ChoiceBNl,
                ChoiceCFr = question.McqDetails.ChoiceCFr,
                ChoiceCNl = question.McqDetails.ChoiceCNl,
                CorrectChoice = question.McqDetails.CorrectChoice
            },
            ListAnswers = question.ListAnswers.Select(a => new ListQuestionAnswerDto
            {
                Id = a.Id,
                AnswerFr = a.AnswerFr,
                AnswerNl = a.AnswerNl,
                AltSpellings = a.AltSpellings
            }).ToList()
        };
    }
}
