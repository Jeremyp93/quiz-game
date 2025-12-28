using Microsoft.AspNetCore.Mvc;
using QuizGame.Application.DTOs;
using QuizGame.Application.Interfaces;
using QuizGame.Domain.Enums;

namespace QuizGame.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<QuestionDto>>> GetQuestions(
        [FromQuery] QuestionType? type,
        [FromQuery] int? difficulty,
        [FromQuery] bool? isActive,
        [FromQuery] string? searchText)
    {
        if (type.HasValue || difficulty.HasValue || isActive.HasValue || !string.IsNullOrWhiteSpace(searchText))
        {
            var filtered = await _questionService.GetFilteredQuestionsAsync(type, difficulty, isActive, searchText);
            return Ok(filtered);
        }

        var questions = await _questionService.GetAllQuestionsAsync();
        return Ok(questions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionDto>> GetQuestion(Guid id)
    {
        var question = await _questionService.GetQuestionByIdAsync(id);
        if (question == null)
            return NotFound();

        return Ok(question);
    }

    [HttpPost]
    public async Task<ActionResult<QuestionDto>> CreateQuestion([FromBody] CreateQuestionDto createDto)
    {
        try
        {
            var question = await _questionService.CreateQuestionAsync(createDto);
            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<QuestionDto>> UpdateQuestion(Guid id, [FromBody] CreateQuestionDto updateDto)
    {
        try
        {
            var question = await _questionService.UpdateQuestionAsync(id, updateDto);
            if (question == null)
                return NotFound();

            return Ok(question);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        var result = await _questionService.DeleteQuestionAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id)
    {
        var result = await _questionService.ToggleActiveAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
