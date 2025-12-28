using QuizGame.Domain.Enums;

namespace QuizGame.Application.DTOs;

public class GameStateDto
{
    public bool IsGameStarted { get; set; }
    public List<string> Players { get; set; } = new();
    public List<TeamDto> Teams { get; set; } = new();
    public Phase CurrentPhase { get; set; }
    public Scene CurrentScene { get; set; }
    public Scene? LastSceneBeforeScoreboard { get; set; }
}

public class TeamDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> Players { get; set; } = new();
    public int Score { get; set; }
}
