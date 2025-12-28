using QuizGame.Application.DTOs;
using QuizGame.Application.Interfaces;
using QuizGame.Domain.Enums;

namespace QuizGame.Infrastructure.Services;

public class GameSessionService : IGameSessionService
{
    private bool _isGameStarted;
    private List<string> _players = new();
    private List<Team> _teams = new();
    private Phase _currentPhase = Phase.Setup;
    private Scene _currentScene = Scene.Teams;
    private Scene? _lastSceneBeforeScoreboard;

    private class Team
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Players { get; set; } = new();
        public int Score { get; set; }
    }

    public GameStateDto GetCurrentState()
    {
        return new GameStateDto
        {
            IsGameStarted = _isGameStarted,
            Players = new List<string>(_players),
            Teams = _teams.Select(t => new TeamDto
            {
                Name = t.Name,
                Players = new List<string>(t.Players),
                Score = t.Score
            }).ToList(),
            CurrentPhase = _currentPhase,
            CurrentScene = _currentScene,
            LastSceneBeforeScoreboard = _lastSceneBeforeScoreboard
        };
    }

    public void StartGame()
    {
        _isGameStarted = true;
        _players.Clear();
        _teams.Clear();
        _currentPhase = Phase.Setup;
        _currentScene = Scene.Teams;
        _lastSceneBeforeScoreboard = null;
    }

    public void SetPlayers(List<string> playerNames)
    {
        _players = new List<string>(playerNames);
    }

    public void CreateTeams()
    {
        if (!_players.Any())
            throw new InvalidOperationException("No players to create teams");

        _teams.Clear();

        // Create 4 teams
        _teams.Add(new Team { Name = "Team A", Players = new() });
        _teams.Add(new Team { Name = "Team B", Players = new() });
        _teams.Add(new Team { Name = "Team C", Players = new() });
        _teams.Add(new Team { Name = "Team D", Players = new() });

        // Shuffle and distribute players evenly
        var shuffledPlayers = _players.OrderBy(_ => Random.Shared.Next()).ToList();
        for (int i = 0; i < shuffledPlayers.Count; i++)
        {
            _teams[i % 4].Players.Add(shuffledPlayers[i]);
        }
    }

    public void RenameTeam(int teamIndex, string newName)
    {
        if (teamIndex < 0 || teamIndex >= _teams.Count)
            throw new ArgumentException("Invalid team index");

        _teams[teamIndex].Name = newName;
    }

    public void MovePlayer(string playerName, int toTeamIndex)
    {
        if (toTeamIndex < 0 || toTeamIndex >= _teams.Count)
            throw new ArgumentException("Invalid team index");

        // Remove from current team
        foreach (var team in _teams)
        {
            team.Players.Remove(playerName);
        }

        // Add to new team
        _teams[toTeamIndex].Players.Add(playerName);
    }

    public void AdjustScore(int teamIndex, int delta)
    {
        if (teamIndex < 0 || teamIndex >= _teams.Count)
            throw new ArgumentException("Invalid team index");

        _teams[teamIndex].Score += delta;
    }

    public void ShowTeamsScene()
    {
        if (_currentScene == Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = null;
        }
        _currentScene = Scene.Teams;
    }

    public void ShowScoreboard()
    {
        if (_currentScene != Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = _currentScene;
        }
        _currentScene = Scene.Scoreboard;
    }

    public void BackToGame()
    {
        if (_lastSceneBeforeScoreboard.HasValue)
        {
            _currentScene = _lastSceneBeforeScoreboard.Value;
            _lastSceneBeforeScoreboard = null;
        }
        else
        {
            _currentScene = Scene.Teams;
        }
    }
}
