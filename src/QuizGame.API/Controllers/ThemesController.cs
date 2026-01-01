using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Application.DTOs;
using QuizGame.Application.Interfaces;

namespace QuizGame.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "GM")]
public class ThemesController : ControllerBase
{
    private readonly IThemeService _themeService;

    public ThemesController(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ThemeDto>>> GetThemes(
        [FromQuery] bool? isActive = null,
        [FromQuery] string? search = null)
    {
        try
        {
            var themes = string.IsNullOrWhiteSpace(search) && !isActive.HasValue
                ? await _themeService.GetAllThemesAsync()
                : await _themeService.GetFilteredThemesAsync(isActive, search);

            return Ok(themes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving themes", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ThemeDto>> GetTheme(Guid id)
    {
        try
        {
            var theme = await _themeService.GetThemeByIdAsync(id);
            if (theme == null)
                return NotFound(new { message = $"Theme with ID {id} not found" });

            return Ok(theme);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving theme", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ThemeDto>> CreateTheme([FromBody] CreateThemeDto createDto)
    {
        try
        {
            var theme = await _themeService.CreateThemeAsync(createDto);
            return CreatedAtAction(nameof(GetTheme), new { id = theme.Id }, theme);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating theme", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ThemeDto>> UpdateTheme(Guid id, [FromBody] CreateThemeDto updateDto)
    {
        try
        {
            var theme = await _themeService.UpdateThemeAsync(id, updateDto);
            if (theme == null)
                return NotFound(new { message = $"Theme with ID {id} not found" });

            return Ok(theme);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating theme", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTheme(Guid id)
    {
        try
        {
            var success = await _themeService.DeleteThemeAsync(id);
            if (!success)
                return NotFound(new { message = $"Theme with ID {id} not found" });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting theme", error = ex.Message });
        }
    }

    [HttpPatch("{id}/toggle")]
    public async Task<ActionResult> ToggleActive(Guid id)
    {
        try
        {
            var success = await _themeService.ToggleActiveAsync(id);
            if (!success)
                return NotFound(new { message = $"Theme with ID {id} not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error toggling theme active status", error = ex.Message });
        }
    }
}
