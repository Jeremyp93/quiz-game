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
    private bool _isCurrentListQuestionVisibleOnDisplay;
    private Guid? _lastListQuestionId;
    private int _listTimerDuration = 45;
    private TimerState _listTimerState = TimerState.Idle;
    private DateTime? _listTimerStartedAtUtc;
    private DateTime? _listTimerPausedAtUtc;
    private long _listTimerAccumulatedPausedMs;
    private DateTime? _listTimerFinishedAtUtc;
    private DateTime? _listTimerBoardsUpVisibleUntilUtc;

    // Phase 3 (Sabotage) state
    private SabotageSubphase _sabotageCurrentSubphase = SabotageSubphase.ThemeAssignment;
    private List<SabotageThemeDto> _sabotageSelectedThemes = new();
    private List<TeamThemeAssignmentDto> _sabotageTeamThemeAssignments = new();
    private int? _sabotageCurrentPickingTeamIndex;
    private bool _sabotageIsThemeAssignmentComplete;
    private int? _sabotageCurrentPlayingTeamIndex;
    private int? _sabotageCurrentThemeIndex;
    private CurrentMcqQuestionDto? _sabotageCurrentMcqQuestion;
    private McqChoice? _sabotageSelectedAnswer;
    private bool _sabotageIsAnswerRevealed;
    private int _sabotageCurrentQuestionInTheme;
    private List<Guid> _sabotageThemeAssignmentHistory = new(); // For undo

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
            IsCurrentListQuestionVisibleOnDisplay = _isCurrentListQuestionVisibleOnDisplay,
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
            },
            Sabotage = new SabotageStateDto
            {
                CurrentSubphase = _sabotageCurrentSubphase,
                SelectedThemes = new List<SabotageThemeDto>(_sabotageSelectedThemes),
                TeamThemeAssignments = new List<TeamThemeAssignmentDto>(_sabotageTeamThemeAssignments),
                CurrentPickingTeamIndex = _sabotageCurrentPickingTeamIndex,
                IsThemeAssignmentComplete = _sabotageIsThemeAssignmentComplete,
                CurrentPlayingTeamIndex = _sabotageCurrentPlayingTeamIndex,
                CurrentThemeIndex = _sabotageCurrentThemeIndex,
                CurrentMcqQuestion = _sabotageCurrentMcqQuestion,
                SelectedAnswer = _sabotageSelectedAnswer,
                IsAnswerRevealed = _sabotageIsAnswerRevealed,
                CurrentQuestionInTheme = _sabotageCurrentQuestionInTheme
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

        // Show team creation loading screen
        _currentScene = Scene.TeamCreationLoading;
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
        _isCurrentListQuestionVisibleOnDisplay = false;
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
        _isCurrentListQuestionVisibleOnDisplay = false;

        // Reset timer when loading new question
        ResetListTimerState();

        // Don't change scene - GM just sees it in their panel
    }

    public async Task ShowListQuestion()
    {
        // Auto-load if no question loaded
        if (_currentListQuestion == null)
        {
            await LoadListQuestion();
        }

        // Show the question on display
        _isCurrentListQuestionVisibleOnDisplay = true;

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

    // ==================== PHASE 3 (SABOTAGE) ====================

    public async Task StartPhase3()
    {
        using var scope = _serviceProvider.CreateScope();
        var themeService = scope.ServiceProvider.GetRequiredService<IThemeService>();

        // Get 8 random active themes
        var themes = await themeService.GetRandomActiveThemesAsync(8);

        // Convert to SabotageThemeDto
        _sabotageSelectedThemes = themes.Select(t => new SabotageThemeDto
        {
            Id = t.Id,
            NameFr = t.NameFr,
            NameNl = t.NameNl,
            Code = t.Code
        }).ToList();

        // Initialize team assignments (4 teams, each gets 2 themes)
        _sabotageTeamThemeAssignments = Enumerable.Range(0, 4)
            .Select(i => new TeamThemeAssignmentDto
            {
                TeamIndex = i,
                AssignedThemes = new List<SabotageThemeDto>()
            })
            .ToList();

        // Determine turn order by score (highest first)
        var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                .OrderByDescending(x => x.Score)
                                .ToList();

        // Start with highest scoring team
        _sabotageCurrentPickingTeamIndex = teamScores[0].TeamIndex;
        _sabotageIsThemeAssignmentComplete = false;
        _sabotageCurrentSubphase = SabotageSubphase.ThemeAssignment;
        _sabotageThemeAssignmentHistory.Clear();

        // Update phase and scene
        _currentPhase = Phase.Sabotage;
        _currentScene = Scene.SabotageThemeAssignment;
    }

    public void AssignThemeToTeam(int teamIndex, Guid themeId)
    {
        if (_sabotageIsThemeAssignmentComplete)
            throw new InvalidOperationException("Theme assignment is already complete");

        // Find the theme
        var theme = _sabotageSelectedThemes.FirstOrDefault(t => t.Id == themeId);
        if (theme == null)
            throw new ArgumentException("Theme not found in selected themes");

        // Check if theme is already assigned
        if (_sabotageTeamThemeAssignments.Any(ta => ta.AssignedThemes.Any(t => t.Id == themeId)))
            throw new InvalidOperationException("Theme is already assigned");

        // Get team assignment
        var teamAssignment = _sabotageTeamThemeAssignments.FirstOrDefault(ta => ta.TeamIndex == teamIndex);
        if (teamAssignment == null)
            throw new ArgumentException("Team not found");

        // Check if team already has 2 themes
        if (teamAssignment.AssignedThemes.Count >= 2)
            throw new InvalidOperationException("Team already has 2 themes");

        // Assign theme to team
        teamAssignment.AssignedThemes.Add(theme);
        _sabotageThemeAssignmentHistory.Add(themeId);

        // Determine next picking team
        var assignedCount = _sabotageTeamThemeAssignments.Sum(ta => ta.AssignedThemes.Count);
        var remainingThemes = 8 - assignedCount;

        if (remainingThemes == 0)
        {
            // All themes assigned
            _sabotageIsThemeAssignmentComplete = true;
            _sabotageCurrentPickingTeamIndex = null;
        }
        else if (remainingThemes == 1)
        {
            // Special rule: Last theme auto-assigned to team without 2 themes
            var teamNeedingTheme = _sabotageTeamThemeAssignments
                .FirstOrDefault(ta => ta.AssignedThemes.Count < 2);

            if (teamNeedingTheme != null)
            {
                var lastTheme = _sabotageSelectedThemes
                    .First(t => !_sabotageTeamThemeAssignments.Any(ta => ta.AssignedThemes.Any(at => at.Id == t.Id)));

                teamNeedingTheme.AssignedThemes.Add(lastTheme);
                _sabotageThemeAssignmentHistory.Add(lastTheme.Id);
                _sabotageIsThemeAssignmentComplete = true;
                _sabotageCurrentPickingTeamIndex = null;
            }
        }
        else
        {
            // Find next team to pick (by score order, skip teams with 2 themes)
            var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                    .OrderByDescending(x => x.Score)
                                    .ToList();

            var currentTeamScoreIndex = teamScores.FindIndex(ts => ts.TeamIndex == teamIndex);

            for (int i = currentTeamScoreIndex + 1; i < teamScores.Count + 10; i++) // +10 to handle wrap-around
            {
                var nextTeamIndex = teamScores[i % teamScores.Count].TeamIndex;
                var nextTeamAssignment = _sabotageTeamThemeAssignments.First(ta => ta.TeamIndex == nextTeamIndex);

                if (nextTeamAssignment.AssignedThemes.Count < 2)
                {
                    _sabotageCurrentPickingTeamIndex = nextTeamIndex;
                    break;
                }
            }
        }
    }

    public void UndoLastThemeAssignment()
    {
        if (_sabotageThemeAssignmentHistory.Count == 0)
            throw new InvalidOperationException("No theme assignments to undo");

        var lastThemeId = _sabotageThemeAssignmentHistory.Last();
        _sabotageThemeAssignmentHistory.RemoveAt(_sabotageThemeAssignmentHistory.Count - 1);

        // Find and remove the theme from whichever team has it
        foreach (var teamAssignment in _sabotageTeamThemeAssignments)
        {
            var themeToRemove = teamAssignment.AssignedThemes.FirstOrDefault(t => t.Id == lastThemeId);
            if (themeToRemove != null)
            {
                teamAssignment.AssignedThemes.Remove(themeToRemove);
                break;
            }
        }

        // Recalculate current picking team
        _sabotageIsThemeAssignmentComplete = false;

        var assignedCount = _sabotageTeamThemeAssignments.Sum(ta => ta.AssignedThemes.Count);
        if (assignedCount == 0)
        {
            // Back to start
            var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                    .OrderByDescending(x => x.Score)
                                    .ToList();
            _sabotageCurrentPickingTeamIndex = teamScores[0].TeamIndex;
        }
        else
        {
            // Find the team that should pick next
            var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                    .OrderByDescending(x => x.Score)
                                    .ToList();

            foreach (var ts in teamScores)
            {
                var teamAssignment = _sabotageTeamThemeAssignments.First(ta => ta.TeamIndex == ts.TeamIndex);
                if (teamAssignment.AssignedThemes.Count < 2)
                {
                    _sabotageCurrentPickingTeamIndex = ts.TeamIndex;
                    break;
                }
            }
        }
    }

    public async Task StartMcqSubphase()
    {
        if (!_sabotageIsThemeAssignmentComplete)
            throw new InvalidOperationException("Theme assignment must be complete first");

        _sabotageCurrentSubphase = SabotageSubphase.McqQuestions;

        // Start with best team
        var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                .OrderByDescending(x => x.Score)
                                .ToList();

        _sabotageCurrentPlayingTeamIndex = teamScores[0].TeamIndex;
        _sabotageCurrentThemeIndex = 0; // First of their 2 themes
        _sabotageCurrentQuestionInTheme = 0;

        // Load first question
        await LoadNextMcqQuestion();
    }

    public async Task LoadNextMcqQuestion()
    {
        if (!_sabotageCurrentPlayingTeamIndex.HasValue)
            throw new InvalidOperationException("No current playing team");

        if (!_sabotageCurrentThemeIndex.HasValue)
            throw new InvalidOperationException("No current theme");

        // Get team's current theme
        var teamAssignment = _sabotageTeamThemeAssignments.First(ta => ta.TeamIndex == _sabotageCurrentPlayingTeamIndex.Value);
        var currentTheme = teamAssignment.AssignedThemes[_sabotageCurrentThemeIndex.Value];

        // Determine difficulty based on question index (0-3)
        // Distribution: 2×Diff1, 1×Diff2, 1×Diff3
        int difficulty = _sabotageCurrentQuestionInTheme switch
        {
            0 => 1,
            1 => 1,
            2 => 2,
            3 => 3,
            _ => throw new InvalidOperationException("Invalid question index")
        };

        using var scope = _serviceProvider.CreateScope();
        var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();

        // Get MCQ questions for this theme and difficulty
        var questions = await questionService.GetFilteredQuestionsAsync(
            type: QuestionType.Mcq,
            difficulty: difficulty,
            isActive: true,
            searchText: null);

        // Filter by theme
        var themeQuestions = questions.Where(q => q.ThemeId == currentTheme.Id).ToList();

        if (themeQuestions.Count == 0)
            throw new InvalidOperationException($"No MCQ questions found for theme '{currentTheme.NameFr}' with difficulty {difficulty}");

        // Pick random question
        var random = new Random();
        var question = themeQuestions[random.Next(themeQuestions.Count)];

        _sabotageCurrentMcqQuestion = new CurrentMcqQuestionDto
        {
            Id = question.Id,
            TextFr = question.TextFr,
            TextNl = question.TextNl,
            ChoiceAFr = question.McqDetails!.ChoiceAFr,
            ChoiceANl = question.McqDetails.ChoiceANl,
            ChoiceBFr = question.McqDetails.ChoiceBFr,
            ChoiceBNl = question.McqDetails.ChoiceBNl,
            ChoiceCFr = question.McqDetails.ChoiceCFr,
            ChoiceCNl = question.McqDetails.ChoiceCNl,
            CorrectChoice = question.McqDetails.CorrectChoice,
            Difficulty = question.Difficulty,
            Theme = currentTheme
        };

        _sabotageSelectedAnswer = null;
        _sabotageIsAnswerRevealed = false;
    }

    public void ShowMcqQuestion()
    {
        if (_sabotageCurrentMcqQuestion == null)
            throw new InvalidOperationException("No MCQ question loaded");

        _currentScene = Scene.SabotageMcqQuestion;
    }

    public void SelectMcqAnswer(McqChoice choice)
    {
        if (_sabotageCurrentMcqQuestion == null)
            throw new InvalidOperationException("No MCQ question loaded");

        _sabotageSelectedAnswer = choice;
    }

    public void RevealMcqAnswer()
    {
        if (_sabotageCurrentMcqQuestion == null)
            throw new InvalidOperationException("No MCQ question loaded");

        _sabotageIsAnswerRevealed = true;
        _currentScene = Scene.SabotageMcqAnswer;
    }
}
