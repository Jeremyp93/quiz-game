using Microsoft.EntityFrameworkCore;
using QuizGame.Application.DTOs;
using QuizGame.Application.Interfaces;
using QuizGame.Domain.Entities;
using QuizGame.Infrastructure.Data;

namespace QuizGame.Infrastructure.Services;

public class ThemeService : IThemeService
{
    private readonly QuizGameDbContext _context;

    public ThemeService(QuizGameDbContext context)
    {
        _context = context;
    }

    public async Task<List<ThemeDto>> GetAllThemesAsync()
    {
        var themes = await _context.Themes
            .Include(t => t.Questions)
            .OrderBy(t => t.SortOrder ?? int.MaxValue)
            .ThenBy(t => t.NameFr)
            .ToListAsync();

        return themes.Select(MapToDto).ToList();
    }

    public async Task<List<ThemeDto>> GetFilteredThemesAsync(bool? isActive, string? searchText)
    {
        var query = _context.Themes
            .Include(t => t.Questions)
            .AsQueryable();

        if (isActive.HasValue)
            query = query.Where(t => t.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            var search = searchText.ToLower();
            query = query.Where(t =>
                t.NameFr.ToLower().Contains(search) ||
                t.NameNl.ToLower().Contains(search) ||
                t.Code.ToLower().Contains(search));
        }

        var themes = await query
            .OrderBy(t => t.SortOrder ?? int.MaxValue)
            .ThenBy(t => t.NameFr)
            .ToListAsync();

        return themes.Select(MapToDto).ToList();
    }

    public async Task<ThemeDto?> GetThemeByIdAsync(Guid id)
    {
        var theme = await _context.Themes
            .Include(t => t.Questions)
            .FirstOrDefaultAsync(t => t.Id == id);

        return theme == null ? null : MapToDto(theme);
    }

    public async Task<ThemeDto> CreateThemeAsync(CreateThemeDto createDto)
    {
        ValidateCreateDto(createDto);

        // Check for duplicate code
        if (await _context.Themes.AnyAsync(t => t.Code == createDto.Code))
            throw new InvalidOperationException($"Theme with code '{createDto.Code}' already exists");

        var theme = new Theme
        {
            Id = Guid.NewGuid(),
            NameFr = createDto.NameFr,
            NameNl = createDto.NameNl,
            Code = createDto.Code,
            IsActive = createDto.IsActive,
            SortOrder = createDto.SortOrder
        };

        _context.Themes.Add(theme);
        await _context.SaveChangesAsync();

        return MapToDto(theme);
    }

    public async Task<ThemeDto?> UpdateThemeAsync(Guid id, CreateThemeDto updateDto)
    {
        ValidateCreateDto(updateDto);

        var theme = await _context.Themes
            .Include(t => t.Questions)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (theme == null)
            return null;

        // Check for duplicate code (excluding current theme)
        if (await _context.Themes.AnyAsync(t => t.Code == updateDto.Code && t.Id != id))
            throw new InvalidOperationException($"Theme with code '{updateDto.Code}' already exists");

        theme.NameFr = updateDto.NameFr;
        theme.NameNl = updateDto.NameNl;
        theme.Code = updateDto.Code;
        theme.IsActive = updateDto.IsActive;
        theme.SortOrder = updateDto.SortOrder;

        await _context.SaveChangesAsync();

        return MapToDto(theme);
    }

    public async Task<bool> DeleteThemeAsync(Guid id)
    {
        var theme = await _context.Themes
            .Include(t => t.Questions)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (theme == null)
            return false;

        // Check if theme has linked questions
        if (theme.Questions.Any())
            throw new InvalidOperationException($"Cannot delete theme '{theme.NameFr}' because it has {theme.Questions.Count} linked question(s). Remove the questions first.");

        _context.Themes.Remove(theme);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        var theme = await _context.Themes.FindAsync(id);
        if (theme == null)
            return false;

        theme.IsActive = !theme.IsActive;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ThemeDto>> GetRandomActiveThemesAsync(int count)
    {
        var activeThemes = await _context.Themes
            .Include(t => t.Questions)
            .Where(t => t.IsActive)
            .ToListAsync();

        if (activeThemes.Count < count)
            throw new InvalidOperationException($"Not enough active themes available. Need {count}, but only {activeThemes.Count} active themes exist.");

        // Randomize and take requested count
        var randomThemes = activeThemes
            .OrderBy(_ => Random.Shared.Next())
            .Take(count)
            .ToList();

        return randomThemes.Select(MapToDto).ToList();
    }

    private static void ValidateCreateDto(CreateThemeDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NameFr))
            throw new ArgumentException("French name is required");

        if (string.IsNullOrWhiteSpace(dto.NameNl))
            throw new ArgumentException("Dutch name is required");

        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new ArgumentException("Code is required");

        // Code should be lowercase alphanumeric with underscores
        if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Code, @"^[a-z0-9_]+$"))
            throw new ArgumentException("Code must contain only lowercase letters, numbers, and underscores");
    }

    private static ThemeDto MapToDto(Theme theme)
    {
        return new ThemeDto
        {
            Id = theme.Id,
            NameFr = theme.NameFr,
            NameNl = theme.NameNl,
            Code = theme.Code,
            IsActive = theme.IsActive,
            SortOrder = theme.SortOrder,
            QuestionCount = theme.Questions?.Count ?? 0
        };
    }
}
