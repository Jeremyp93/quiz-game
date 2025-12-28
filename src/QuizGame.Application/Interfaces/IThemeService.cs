using QuizGame.Application.DTOs;

namespace QuizGame.Application.Interfaces;

public interface IThemeService
{
    Task<List<ThemeDto>> GetAllThemesAsync();
    Task<List<ThemeDto>> GetFilteredThemesAsync(bool? isActive, string? searchText);
    Task<ThemeDto?> GetThemeByIdAsync(Guid id);
    Task<ThemeDto> CreateThemeAsync(CreateThemeDto createDto);
    Task<ThemeDto?> UpdateThemeAsync(Guid id, CreateThemeDto updateDto);
    Task<bool> DeleteThemeAsync(Guid id);
    Task<bool> ToggleActiveAsync(Guid id);
    Task<List<ThemeDto>> GetRandomActiveThemesAsync(int count);
}
