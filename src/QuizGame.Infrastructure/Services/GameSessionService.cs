using Microsoft.Extensions.DependencyInjection;
using QuizGame.Application.DTOs;
using QuizGame.Application.Interfaces;
using QuizGame.Domain.Enums;

namespace QuizGame.Infrastructure.Services;

public class GameSessionService : IGameSessionService
{
    private readonly IServiceProvider _serviceProvider;

    private bool _isGameStarted;
    private List<string> _players = new();
    private List<Team> _teams = new();
    private Phase _currentPhase = Phase.Setup;
    private Scene _currentScene = Scene.Teams;
    private Scene? _lastSceneBeforeScoreboard;

    // Phase 1 (Fast Buzzer) state
    private CurrentQuestionDto? _currentQuestion;
    private bool _isCurrentQuestionVisibleOnDisplay;
    private Guid? _lastQuestionId;
    private List<int> _blockedNextQuestionTeamIds = new();
    private List<int> _blockedTeamIdsForCurrentQuestion = new();

    // Phase 2 (List) state
    private CurrentListQuestionDto? _currentListQuestion;
    private Guid? _lastListQuestionId;
    private int _listTimerDuration = 45;
    private TimerState _listTimerState = TimerState.Idle;
    private DateTime? _listTimerStartedAtUtc;
    private DateTime? _listTimerPausedAtUtc;
    private long _listTimerAccumulatedPausedMs;
    private DateTime? _listTimerFinishedAtUtc;
    private DateTime? _listTimerBoardsUpVisibleUntilUtc;

    public GameSessionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

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
            LastSceneBeforeScoreboard = _lastSceneBeforeScoreboard,
            CurrentQuestion = _currentQuestion,
            IsCurrentQuestionVisibleOnDisplay = _isCurrentQuestionVisibleOnDisplay,
            LastQuestionId = _lastQuestionId,
            BlockedNextQuestionTeamIds = new List<int>(_blockedNextQuestionTeamIds),
            BlockedTeamIdsForCurrentQuestion = new List<int>(_blockedTeamIdsForCurrentQuestion),
            CurrentListQuestion = _currentListQuestion,
            LastListQuestionId = _lastListQuestionId,
            ListTimer = new ListTimerDto
            {
                DurationSeconds = _listTimerDuration,
                State = _listTimerState,
                StartedAtUtc = _listTimerStartedAtUtc,
                PausedAtUtc = _listTimerPausedAtUtc,
                AccumulatedPausedMs = _listTimerAccumulatedPausedMs,
                FinishedAtUtc = _listTimerFinishedAtUtc,
                BoardsUpVisibleUntilUtc = _listTimerBoardsUpVisibleUntilUtc
            }
        };
    }

    public void StartGame()
    {
        _isGameStarted = true;
        _players.Clear();
        _teams.Clear();
        _currentPhase = Phase.Setup;
        _currentScene = Scene.Scoreboard;  // Changed from Teams to Scoreboard to prevent auto-showing teams
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

    // Phase 1 (Fast Buzzer) Methods

    public Task StartPhase1()
    {
        _currentPhase = Phase.FastBuzzer;
        _currentQuestion = null;
        _isCurrentQuestionVisibleOnDisplay = false;
        _lastQuestionId = null;
        _blockedNextQuestionTeamIds.Clear();
        _blockedTeamIdsForCurrentQuestion.Clear();
        return Task.CompletedTask;
    }

    public async Task GetQuestion()
    {
        // Create a scope to resolve scoped IQuestionService
        using var scope = _serviceProvider.CreateScope();
        var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();

        // Get random Regular question (avoid immediate repeat if possible)
        var question = await questionService.GetRandomRegularQuestionAsync(_lastQuestionId);

        if (question == null)
            throw new InvalidOperationException("No active Regular questions available");

        if (question.RegularDetails == null)
            throw new InvalidOperationException("Question missing Regular details");

        // Apply blocks for this question
        _blockedTeamIdsForCurrentQuestion = new List<int>(_blockedNextQuestionTeamIds);
        _blockedNextQuestionTeamIds.Clear();

        // Set current question (GM sees it, but not shown on display yet)
        _currentQuestion = new CurrentQuestionDto
        {
            Id = question.Id,
            TextFr = question.TextFr,
            TextNl = question.TextNl,
            AnswerFr = question.RegularDetails.AnswerFr,
            AnswerNl = question.RegularDetails.AnswerNl,
            Difficulty = question.Difficulty
        };

        _lastQuestionId = question.Id;
        _isCurrentQuestionVisibleOnDisplay = false;

        // Don't change scene - GM just sees it in their panel
    }

    public void ShowQuestion()
    {
        if (_currentQuestion == null)
            throw new InvalidOperationException("No question loaded. Use GetQuestion first.");

        // Show the question on display
        _isCurrentQuestionVisibleOnDisplay = true;

        // Switch to Question scene (leaves scoreboard if visible)
        if (_currentScene == Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = null;
        }
        _currentScene = Scene.Question;
    }

    public void ShowAnswer()
    {
        if (_currentQuestion == null)
            throw new InvalidOperationException("No current question to reveal answer");

        _currentScene = Scene.Answer;
    }

    public void ApplyBlocksForNextQuestion(List<int> teamIndices)
    {
        // Validate team indices
        foreach (var index in teamIndices)
        {
            if (index < 0 || index >= _teams.Count)
                throw new ArgumentException($"Invalid team index: {index}");
        }

        _blockedNextQuestionTeamIds = new List<int>(teamIndices);
    }

    public void EndPhase()
    {
        _currentPhase = Phase.Setup;
        _currentQuestion = null;
        _isCurrentQuestionVisibleOnDisplay = false;
        _lastQuestionId = null;
        _blockedNextQuestionTeamIds.Clear();
        _blockedTeamIdsForCurrentQuestion.Clear();

        // Clear Phase 2 state
        _currentListQuestion = null;
        _lastListQuestionId = null;
        ResetListTimerState();

        // Show scoreboard when ending phase
        if (_currentScene != Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = _currentScene;
        }
        _currentScene = Scene.Scoreboard;
    }

    // Phase 2 (List) Methods

    public Task StartPhase2()
    {
        _currentPhase = Phase.List;
        _currentListQuestion = null;
        _lastListQuestionId = null;
        ResetListTimerState();
        return Task.CompletedTask;
    }

    public async Task LoadListQuestion()
    {
        using var scope = _serviceProvider.CreateScope();
        var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();

        var question = await questionService.GetRandomListQuestionAsync(_lastListQuestionId);

        if (question == null)
            throw new InvalidOperationException("No active List questions available");

        if (question.ListAnswers == null || !question.ListAnswers.Any())
            throw new InvalidOperationException("Question missing List answers");

        _currentListQuestion = new CurrentListQuestionDto
        {
            Id = question.Id,
            TextFr = question.TextFr,
            TextNl = question.TextNl,
            Answers = question.ListAnswers.Select(a => new ListAnswerDto
            {
                AnswerFr = a.AnswerFr,
                AnswerNl = a.AnswerNl
            }).ToList(),
            Difficulty = question.Difficulty
        };

        _lastListQuestionId = question.Id;

        // Reset timer when loading new question
        ResetListTimerState();
    }

    public async Task ShowListQuestion()
    {
        // Auto-load if no question loaded
        if (_currentListQuestion == null)
        {
            await LoadListQuestion();
        }

        // Switch to ListQuestion scene
        if (_currentScene == Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = null;
        }
        _currentScene = Scene.ListQuestion;
    }

    public void StartListTimer()
    {
        if (_listTimerState != TimerState.Idle)
            throw new InvalidOperationException("Timer must be in Idle state to start");

        if (_currentScene != Scene.ListQuestion)
            throw new InvalidOperationException("Question must be shown on display first");

        _listTimerState = TimerState.Running;
        _listTimerStartedAtUtc = DateTime.UtcNow;
        _listTimerAccumulatedPausedMs = 0;
    }

    public void PauseListTimer()
    {
        if (_listTimerState != TimerState.Running)
            throw new InvalidOperationException("Timer must be Running to pause");

        _listTimerState = TimerState.Paused;
        _listTimerPausedAtUtc = DateTime.UtcNow;
    }

    public void ResumeListTimer()
    {
        if (_listTimerState != TimerState.Paused)
            throw new InvalidOperationException("Timer must be Paused to resume");

        if (_listTimerPausedAtUtc.HasValue && _listTimerStartedAtUtc.HasValue)
        {
            var pauseDuration = (DateTime.UtcNow - _listTimerPausedAtUtc.Value).TotalMilliseconds;
            _listTimerAccumulatedPausedMs += (long)pauseDuration;
        }

        _listTimerState = TimerState.Running;
        _listTimerPausedAtUtc = null;
    }

    public void ResetListTimer()
    {
        if (_listTimerState == TimerState.Idle)
            throw new InvalidOperationException("Timer is already in Idle state");

        ResetListTimerState();
    }

    private void ResetListTimerState()
    {
        _listTimerDuration = 45;
        _listTimerState = TimerState.Idle;
        _listTimerStartedAtUtc = null;
        _listTimerPausedAtUtc = null;
        _listTimerAccumulatedPausedMs = 0;
        _listTimerFinishedAtUtc = null;
        _listTimerBoardsUpVisibleUntilUtc = null;
    }

    // Called by TimerBackgroundService when timer hits 0
    public void FinishListTimer()
    {
        if (_listTimerState != TimerState.Running)
            return;

        _listTimerState = TimerState.Finished;
        _listTimerFinishedAtUtc = DateTime.UtcNow;
        _listTimerBoardsUpVisibleUntilUtc = DateTime.UtcNow.AddSeconds(10);
    }

    // Called by TimerBackgroundService to clear BoardsUp overlay after 10s
    public void ClearBoardsUpOverlay()
    {
        _listTimerBoardsUpVisibleUntilUtc = null;
    }
}
