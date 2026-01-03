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
    private List<Guid> _usedPhase1QuestionIds = new();

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
    private List<Guid> _usedPhase2QuestionIds = new();

    // Phase 3 (Sabotage) state
    private SabotageSubphase _sabotageCurrentSubphase = SabotageSubphase.ThemeAssignment;
    private List<SabotageThemeDto> _sabotageSelectedThemes = new();
    private List<TeamThemeAssignmentDto> _sabotageTeamThemeAssignments = new();
    private int? _sabotageCurrentPickingTeamIndex;
    private int _sabotageCurrentPickNumber = 1; // 1 = self-select, 2 = sabotage another team
    private bool _sabotageIsThemeAssignmentComplete;
    private int? _sabotageCurrentPlayingTeamIndex;
    private int? _sabotageCurrentThemeIndex;
    private CurrentMcqQuestionDto? _sabotageCurrentMcqQuestion;
    private McqChoice? _sabotageSelectedAnswer;
    private bool _sabotageIsAnswerRevealed;
    private int _sabotageCurrentQuestionInTheme;
    private List<Guid> _sabotageThemeAssignmentHistory = new(); // For undo

    // Phase 4 (Chrono) state
    private int? _chronoActiveTeamIndex;
    private ChronoRunStatus _chronoRunStatus = ChronoRunStatus.Idle;
    private int _chronoCorrectCount;
    private CurrentQuestionDto? _chronoCurrentQuestion;
    private Guid? _chronoLastQuestionId;
    private TimerState _chronoTimerState = TimerState.Idle;
    private DateTime? _chronoTimerStartedAtUtc;
    private DateTime? _chronoTimerPausedAtUtc;
    private long _chronoTimerAccumulatedPausedMs;
    private DateTime? _chronoTimerFinishedAtUtc;
    private long? _chronoBestTimeMs;
    private Dictionary<int, ChronoTeamResult> _chronoTeamResults = new();
    private List<Guid> _usedPhase4QuestionIds = new();

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
                CurrentPickNumber = _sabotageCurrentPickNumber,
                IsThemeAssignmentComplete = _sabotageIsThemeAssignmentComplete,
                CurrentPlayingTeamIndex = _sabotageCurrentPlayingTeamIndex,
                CurrentThemeIndex = _sabotageCurrentThemeIndex,
                CurrentMcqQuestion = _sabotageCurrentMcqQuestion,
                SelectedAnswer = _sabotageSelectedAnswer,
                IsAnswerRevealed = _sabotageIsAnswerRevealed,
                CurrentQuestionInTheme = _sabotageCurrentQuestionInTheme
            },
            Chrono = new ChronoStateDto
            {
                ActiveTeamIndex = _chronoActiveTeamIndex,
                RunStatus = _chronoRunStatus,
                CorrectCount = _chronoCorrectCount,
                CurrentQuestion = _chronoCurrentQuestion,
                LastQuestionId = _chronoLastQuestionId,
                TimerState = _chronoTimerState,
                TimerStartedAtUtc = _chronoTimerStartedAtUtc,
                TimerPausedAtUtc = _chronoTimerPausedAtUtc,
                TimerAccumulatedPausedMs = _chronoTimerAccumulatedPausedMs,
                TimerFinishedAtUtc = _chronoTimerFinishedAtUtc,
                BestTimeMs = _chronoBestTimeMs,
                TeamResults = new Dictionary<int, ChronoTeamResult>(_chronoTeamResults)
            }
        };
    }

    public void StartGame()
    {
        _isGameStarted = true;
        _players.Clear();
        _teams.Clear();
        _currentPhase = Phase.Setup;
        _currentScene = Scene.Welcome;  // Changed from Teams to Scoreboard to prevent auto-showing teams
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
        _currentScene = Scene.Phase1Intro;
        _currentQuestion = null;
        _isCurrentQuestionVisibleOnDisplay = false;
        _lastQuestionId = null;
        _blockedNextQuestionTeamIds.Clear();
        _blockedTeamIdsForCurrentQuestion.Clear();
        _usedPhase1QuestionIds.Clear();
        return Task.CompletedTask;
    }

    public async Task GetQuestion()
    {
        // Create a scope to resolve scoped IQuestionService
        using var scope = _serviceProvider.CreateScope();
        var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();

        // Get random Regular question (avoid already used questions in this phase)
        var question = await questionService.GetRandomRegularQuestionAsync(_usedPhase1QuestionIds);

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
        _usedPhase1QuestionIds.Add(question.Id);
        _isCurrentQuestionVisibleOnDisplay = false;

        // Show transition scene
        _currentScene = Scene.QuestionTransition;
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
        _usedPhase1QuestionIds.Clear();

        // Clear Phase 2 state
        _currentListQuestion = null;
        _lastListQuestionId = null;
        _usedPhase2QuestionIds.Clear();
        ResetListTimerState();

        // Clear Phase 4 state
        _usedPhase4QuestionIds.Clear();

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
        _currentScene = Scene.Phase2Intro;
        _currentListQuestion = null;
        _isCurrentListQuestionVisibleOnDisplay = false;
        _lastListQuestionId = null;
        _usedPhase2QuestionIds.Clear();
        ResetListTimerState();
        return Task.CompletedTask;
    }

    public async Task LoadListQuestion()
    {
        using var scope = _serviceProvider.CreateScope();
        var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();

        var question = await questionService.GetRandomListQuestionAsync(_usedPhase2QuestionIds);

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
        _usedPhase2QuestionIds.Add(question.Id);
        _isCurrentListQuestionVisibleOnDisplay = false;

        // Reset timer when loading new question
        ResetListTimerState();

        // Show question transition scene
        if (_currentScene == Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = null;
        }
        _currentScene = Scene.QuestionTransition;
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

    public void SetListTimerDuration(int durationSeconds)
    {
        if (durationSeconds <= 0)
            throw new ArgumentException("Timer duration must be positive", nameof(durationSeconds));

        if (_listTimerState != TimerState.Idle)
            throw new InvalidOperationException("Timer duration can only be changed when timer is Idle");

        _listTimerDuration = durationSeconds;
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
            Code = t.Code,
            Icon = t.Icon
        }).ToList();

        // Initialize team assignments (4 teams, each gets 2 themes)
        _sabotageTeamThemeAssignments = Enumerable.Range(0, 4)
            .Select(i => new TeamThemeAssignmentDto
            {
                TeamIndex = i,
                SelfSelectedTheme = null,
                SabotageTheme = null
            })
            .ToList();

        // Determine turn order by score (highest first)
        var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                .OrderByDescending(x => x.Score)
                                .ToList();

        // Start with highest scoring team, pick 1 (self-select)
        _sabotageCurrentPickingTeamIndex = teamScores[0].TeamIndex;
        _sabotageCurrentPickNumber = 1;
        _sabotageIsThemeAssignmentComplete = false;
        _sabotageCurrentSubphase = SabotageSubphase.ThemeAssignment;
        _sabotageThemeAssignmentHistory.Clear();

        // Update phase and scene
        _currentPhase = Phase.Sabotage;
        _currentScene = Scene.Phase3Intro;
    }

    public void StartThemeAssignment()
    {
        if (_currentScene != Scene.Phase3Intro)
            throw new InvalidOperationException("Can only start theme assignment from Phase 3 intro scene");

        _currentScene = Scene.SabotageThemeAssignment;
    }

    public void AssignThemeToTeam(int teamIndex, Guid themeId)
    {
        // Transition from intro scene to theme assignment scene on first assignment
        if (_currentScene == Scene.Phase3Intro)
        {
            _currentScene = Scene.SabotageThemeAssignment;
        }

        if (_sabotageIsThemeAssignmentComplete)
            throw new InvalidOperationException("Theme assignment is already complete");

        if (_sabotageCurrentPickingTeamIndex == null)
            throw new InvalidOperationException("No team is currently picking");

        // Find the theme
        var theme = _sabotageSelectedThemes.FirstOrDefault(t => t.Id == themeId);
        if (theme == null)
            throw new ArgumentException("Theme not found in selected themes");

        // Check if theme is already assigned
        bool isThemeAssigned = _sabotageTeamThemeAssignments.Any(ta =>
            (ta.SelfSelectedTheme != null && ta.SelfSelectedTheme.Id == themeId) ||
            (ta.SabotageTheme != null && ta.SabotageTheme.Id == themeId));
        if (isThemeAssigned)
            throw new InvalidOperationException("Theme is already assigned");

        // Get team assignment
        var teamAssignment = _sabotageTeamThemeAssignments.FirstOrDefault(ta => ta.TeamIndex == teamIndex);
        if (teamAssignment == null)
            throw new ArgumentException("Team not found");

        if (_sabotageCurrentPickNumber == 1)
        {
            // Pick 1: Self-select - must assign to current picking team
            if (teamIndex != _sabotageCurrentPickingTeamIndex.Value)
                throw new InvalidOperationException("For pick 1, you must select a theme for your own team");

            if (teamAssignment.SelfSelectedTheme != null)
                throw new InvalidOperationException("Team already has a self-selected theme");

            teamAssignment.SelfSelectedTheme = theme;
            _sabotageThemeAssignmentHistory.Add(themeId);

            // Move to pick 2 (sabotage) for same team
            _sabotageCurrentPickNumber = 2;
        }
        else // _sabotageCurrentPickNumber == 2
        {
            // Pick 2: Sabotage - must assign to ANOTHER team (unless it's the only option)
            // Check if there are other teams without sabotage themes
            var teamsWithoutSabotage = _sabotageTeamThemeAssignments
                .Where(ta => ta.SabotageTheme == null)
                .ToList();

            // If there are multiple teams without sabotage, prevent self-sabotage
            // If only one team left (the current one), allow self-sabotage
            if (teamsWithoutSabotage.Count > 1 && teamIndex == _sabotageCurrentPickingTeamIndex.Value)
                throw new InvalidOperationException("For pick 2, you must select a theme to sabotage another team");

            if (teamAssignment.SabotageTheme != null)
                throw new InvalidOperationException("Team already has a sabotage theme");

            teamAssignment.SabotageTheme = theme;
            _sabotageThemeAssignmentHistory.Add(themeId);

            // Move to next team in score order
            var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                    .OrderByDescending(x => x.Score)
                                    .ToList();

            var currentPickerScoreIndex = teamScores.FindIndex(ts => ts.TeamIndex == _sabotageCurrentPickingTeamIndex.Value);

            // Find next team that hasn't completed their 2 picks yet
            bool foundNextTeam = false;
            for (int i = 1; i < teamScores.Count; i++)
            {
                var nextTeamIndex = teamScores[(currentPickerScoreIndex + i) % teamScores.Count].TeamIndex;
                var nextTeamAssignment = _sabotageTeamThemeAssignments.First(ta => ta.TeamIndex == nextTeamIndex);

                // Team hasn't completed if either theme slot is empty
                if (nextTeamAssignment.SelfSelectedTheme == null || nextTeamAssignment.SabotageTheme == null)
                {
                    _sabotageCurrentPickingTeamIndex = nextTeamIndex;
                    _sabotageCurrentPickNumber = nextTeamAssignment.SelfSelectedTheme == null ? 1 : 2;
                    foundNextTeam = true;
                    break;
                }
            }

            if (!foundNextTeam)
            {
                // All themes assigned
                _sabotageIsThemeAssignmentComplete = true;
                _sabotageCurrentPickingTeamIndex = null;
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
            if (teamAssignment.SelfSelectedTheme != null && teamAssignment.SelfSelectedTheme.Id == lastThemeId)
            {
                teamAssignment.SelfSelectedTheme = null;
                break;
            }
            if (teamAssignment.SabotageTheme != null && teamAssignment.SabotageTheme.Id == lastThemeId)
            {
                teamAssignment.SabotageTheme = null;
                break;
            }
        }

        // Recalculate current picking team and pick number
        _sabotageIsThemeAssignmentComplete = false;

        var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                .OrderByDescending(x => x.Score)
                                .ToList();

        // Find the first team (in score order) that hasn't completed both picks
        foreach (var ts in teamScores)
        {
            var teamAssignment = _sabotageTeamThemeAssignments.First(ta => ta.TeamIndex == ts.TeamIndex);

            if (teamAssignment.SelfSelectedTheme == null)
            {
                // Team hasn't done pick 1 yet
                _sabotageCurrentPickingTeamIndex = ts.TeamIndex;
                _sabotageCurrentPickNumber = 1;
                return;
            }
            else if (teamAssignment.SabotageTheme == null)
            {
                // Team has done pick 1 but not pick 2
                _sabotageCurrentPickingTeamIndex = ts.TeamIndex;
                _sabotageCurrentPickNumber = 2;
                return;
            }
        }

        // If we get here, all picks are complete (shouldn't happen after undo)
        _sabotageIsThemeAssignmentComplete = true;
        _sabotageCurrentPickingTeamIndex = null;
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

        // Don't auto-load first question - wait for manual load
    }

    public async Task LoadNextMcqQuestion()
    {
        if (!_sabotageCurrentPlayingTeamIndex.HasValue)
            throw new InvalidOperationException("No current playing team");

        if (!_sabotageCurrentThemeIndex.HasValue)
            throw new InvalidOperationException("No current theme");

        // Get team's current theme
        var teamAssignment = _sabotageTeamThemeAssignments.First(ta => ta.TeamIndex == _sabotageCurrentPlayingTeamIndex.Value);
        var currentTheme = _sabotageCurrentThemeIndex.Value == 0
            ? teamAssignment.SelfSelectedTheme
            : teamAssignment.SabotageTheme;

        if (currentTheme == null)
            throw new InvalidOperationException("Theme not assigned");

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

        // Filter by theme and order consistently
        var themeQuestions = questions.Where(q => q.ThemeId == currentTheme.Id)
                                      .OrderBy(q => q.Id)
                                      .ToList();

        if (themeQuestions.Count == 0)
            throw new InvalidOperationException($"No MCQ questions found for theme '{currentTheme.NameFr}' with difficulty {difficulty}");

        // For difficulty 1 (questions 0 and 1), select based on question index
        // For difficulty 2 and 3, take the first question
        int questionIndex = 0;
        if (difficulty == 1)
        {
            // Question 0 gets first easy question, Question 1 gets second easy question
            questionIndex = _sabotageCurrentQuestionInTheme; // 0 or 1
            if (questionIndex >= themeQuestions.Count)
                throw new InvalidOperationException($"Not enough MCQ questions for theme '{currentTheme.NameFr}' with difficulty {difficulty}");
        }

        var question = themeQuestions[questionIndex];

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

    public void ClearMcqAnswer()
    {
        if (_sabotageCurrentMcqQuestion == null)
            throw new InvalidOperationException("No MCQ question loaded");

        _sabotageSelectedAnswer = null;
    }

    public void RevealMcqAnswer()
    {
        if (_sabotageCurrentMcqQuestion == null)
            throw new InvalidOperationException("No MCQ question loaded");

        _sabotageIsAnswerRevealed = true;
        _currentScene = Scene.SabotageMcqAnswer;
    }

    public async Task AdvanceToNextMcqQuestion()
    {
        if (!_sabotageCurrentPlayingTeamIndex.HasValue)
            throw new InvalidOperationException("No current playing team");

        if (!_sabotageCurrentThemeIndex.HasValue)
            throw new InvalidOperationException("No current theme");

        // Increment question in theme
        _sabotageCurrentQuestionInTheme++;

        // Check if we've completed 4 questions for this theme
        if (_sabotageCurrentQuestionInTheme >= 4)
        {
            // Move to next theme
            _sabotageCurrentQuestionInTheme = 0;
            _sabotageCurrentThemeIndex++;

            // Check if we've completed both themes (0 = self-selected, 1 = sabotage)
            if (_sabotageCurrentThemeIndex >= 2)
            {
                // Move to next team
                var teamScores = _teams.Select((t, i) => new { TeamIndex = i, Score = t.Score })
                                        .OrderByDescending(x => x.Score)
                                        .ToList();

                var currentTeamScoreIndex = teamScores.FindIndex(ts => ts.TeamIndex == _sabotageCurrentPlayingTeamIndex.Value);

                // Check if there's a next team
                if (currentTeamScoreIndex < teamScores.Count - 1)
                {
                    // Move to next team
                    _sabotageCurrentPlayingTeamIndex = teamScores[currentTeamScoreIndex + 1].TeamIndex;
                    _sabotageCurrentThemeIndex = 0;
                    _sabotageCurrentQuestionInTheme = 0;
                }
                else
                {
                    // All teams completed - MCQ subphase is done
                    _sabotageCurrentPlayingTeamIndex = null;
                    _sabotageCurrentThemeIndex = null;
                    _sabotageCurrentMcqQuestion = null;
                    _currentScene = Scene.Scoreboard;
                    return;
                }
            }
        }

        // Load next question
        await LoadNextMcqQuestion();

        // Reset scene to QuestionTransition so GM can preview and show next question
        _currentScene = Scene.QuestionTransition;
    }

    // ============================================================
    // Phase 4 (Chrono) Methods
    // ============================================================

    public Task StartPhase4()
    {
        _currentPhase = Phase.Chrono;
        _currentScene = Scene.Phase4Intro;

        // Reset all state
        _chronoActiveTeamIndex = null;
        _chronoRunStatus = ChronoRunStatus.Idle;
        _chronoCorrectCount = 0;
        _chronoCurrentQuestion = null;
        _chronoLastQuestionId = null;
        _usedPhase4QuestionIds.Clear();
        ResetChronoTimerState();
        _chronoBestTimeMs = null;
        _chronoTeamResults.Clear();

        // Initialize team results
        for (int i = 0; i < _teams.Count; i++)
        {
            _chronoTeamResults[i] = new ChronoTeamResult
            {
                Status = ChronoResultStatus.NotStarted,
                TimeMs = null
            };
        }

        return Task.CompletedTask;
    }

    public void SelectTeamForRun(int teamIndex)
    {
        if (teamIndex < 0 || teamIndex >= _teams.Count)
            throw new ArgumentException("Invalid team index");

        // Always reset the run state when selecting a team (unless already Idle)
        if (_chronoRunStatus != ChronoRunStatus.Idle)
        {
            ResetCurrentChronoRun();
        }

        _chronoActiveTeamIndex = teamIndex;
        _chronoRunStatus = ChronoRunStatus.Idle;

        // Show "Are You Ready?" scene when selecting a new team
        _currentScene = Scene.ChronoReady;
    }

    public async Task ShowNextChronoQuestion()
    {
        if (!_chronoActiveTeamIndex.HasValue)
            throw new InvalidOperationException("No team selected for run");

        using var scope = _serviceProvider.CreateScope();
        var questionService = scope.ServiceProvider.GetRequiredService<IQuestionService>();

        // Get random Regular4 question (avoid already used questions in this phase)
        var question = await questionService.GetRandomRegular4QuestionAsync(_usedPhase4QuestionIds);

        if (question == null)
            throw new InvalidOperationException("No active Regular4 questions available");

        if (question.RegularDetails == null)
            throw new InvalidOperationException("Question missing Regular details");

        // Set current question
        _chronoCurrentQuestion = new CurrentQuestionDto
        {
            Id = question.Id,
            TextFr = question.TextFr,
            TextNl = question.TextNl,
            AnswerFr = question.RegularDetails.AnswerFr,
            AnswerNl = question.RegularDetails.AnswerNl,
            Difficulty = question.Difficulty
        };

        _chronoLastQuestionId = question.Id;
        _usedPhase4QuestionIds.Add(question.Id);

        // Auto-start timer on first question
        if (_chronoTimerState == TimerState.Idle)
        {
            _chronoTimerState = TimerState.Running;
            _chronoTimerStartedAtUtc = DateTime.UtcNow;
            _chronoTimerAccumulatedPausedMs = 0;
            _chronoRunStatus = ChronoRunStatus.Running;
        }

        // Show on display
        if (_currentScene == Scene.Scoreboard)
        {
            _lastSceneBeforeScoreboard = null;
        }
        _currentScene = Scene.ChronoQuestion;
    }

    public void MarkChronoCorrect()
    {
        if (_chronoRunStatus != ChronoRunStatus.Running && _chronoRunStatus != ChronoRunStatus.Paused)
            throw new InvalidOperationException("No run in progress");

        if (_chronoCorrectCount >= 10)
            throw new InvalidOperationException("Already at 10 correct answers");

        _chronoCorrectCount++;

        // Auto-detect success
        if (_chronoCorrectCount == 10)
        {
            AutoFinishChronoSuccess();
        }
    }

    public void PauseChronoTimer()
    {
        if (_chronoTimerState != TimerState.Running)
            throw new InvalidOperationException("Timer must be Running to pause");

        _chronoTimerState = TimerState.Paused;
        _chronoTimerPausedAtUtc = DateTime.UtcNow;
        _chronoRunStatus = ChronoRunStatus.Paused;
    }

    public void ResumeChronoTimer()
    {
        if (_chronoTimerState != TimerState.Paused)
            throw new InvalidOperationException("Timer must be Paused to resume");

        if (_chronoTimerPausedAtUtc.HasValue && _chronoTimerStartedAtUtc.HasValue)
        {
            var pauseDuration = (DateTime.UtcNow - _chronoTimerPausedAtUtc.Value).TotalMilliseconds;
            _chronoTimerAccumulatedPausedMs += (long)pauseDuration;
        }

        _chronoTimerState = TimerState.Running;
        _chronoTimerPausedAtUtc = null;
        _chronoRunStatus = ChronoRunStatus.Running;
    }

    public void ResetChronoRun()
    {
        if (_chronoRunStatus == ChronoRunStatus.Idle)
            throw new InvalidOperationException("No run to reset");

        ResetCurrentChronoRun();
        _currentScene = Scene.Scoreboard;
    }

    public void AbortChronoRun()
    {
        if (!_chronoActiveTeamIndex.HasValue)
            throw new InvalidOperationException("No team selected");

        if (_chronoRunStatus == ChronoRunStatus.Idle)
            throw new InvalidOperationException("No run to abort");

        // Save as aborted
        _chronoTeamResults[_chronoActiveTeamIndex.Value] = new ChronoTeamResult
        {
            Status = ChronoResultStatus.Aborted,
            TimeMs = null
        };

        ResetCurrentChronoRun();
        _currentScene = Scene.Scoreboard;
    }

    public void ForceFinishChronoRun()
    {
        if (!_chronoActiveTeamIndex.HasValue)
            throw new InvalidOperationException("No team selected");

        if (_chronoRunStatus == ChronoRunStatus.Idle)
            throw new InvalidOperationException("No run to finish");

        // Stop timer if running
        if (_chronoTimerState == TimerState.Running)
        {
            _chronoTimerState = TimerState.Finished;
            _chronoTimerFinishedAtUtc = DateTime.UtcNow;
        }
        else if (_chronoTimerState == TimerState.Paused)
        {
            _chronoTimerState = TimerState.Finished;
            _chronoTimerFinishedAtUtc = _chronoTimerPausedAtUtc;
        }

        long finalTimeMs = CalculateChronoElapsedMs();

        _chronoTeamResults[_chronoActiveTeamIndex.Value] = new ChronoTeamResult
        {
            Status = ChronoResultStatus.Finished,
            TimeMs = finalTimeMs
        };

        // Update best time if beaten
        if (!_chronoBestTimeMs.HasValue || finalTimeMs < _chronoBestTimeMs.Value)
        {
            _chronoBestTimeMs = finalTimeMs;
        }

        _chronoRunStatus = ChronoRunStatus.Finished;
        _currentScene = Scene.ChronoCompletion;
    }

    public void FinishChronoTimerAsNotFinished()
    {
        if (!_chronoActiveTeamIndex.HasValue)
            return;

        if (_chronoTimerState != TimerState.Running)
            return;

        _chronoTimerState = TimerState.Finished;
        _chronoTimerFinishedAtUtc = DateTime.UtcNow;

        _chronoTeamResults[_chronoActiveTeamIndex.Value] = new ChronoTeamResult
        {
            Status = ChronoResultStatus.NotFinished,
            TimeMs = null
        };

        _chronoRunStatus = ChronoRunStatus.NotFinished;
        _currentScene = Scene.ChronoFailure;
    }

    private void AutoFinishChronoSuccess()
    {
        if (!_chronoActiveTeamIndex.HasValue)
            return;

        // Stop timer
        if (_chronoTimerState == TimerState.Running)
        {
            _chronoTimerState = TimerState.Finished;
            _chronoTimerFinishedAtUtc = DateTime.UtcNow;
        }
        else if (_chronoTimerState == TimerState.Paused)
        {
            // If paused, use paused time as finish time
            _chronoTimerState = TimerState.Finished;
            _chronoTimerFinishedAtUtc = _chronoTimerPausedAtUtc;
        }

        // Calculate final time
        long finalTimeMs = CalculateChronoElapsedMs();

        // Save result
        _chronoTeamResults[_chronoActiveTeamIndex.Value] = new ChronoTeamResult
        {
            Status = ChronoResultStatus.Finished,
            TimeMs = finalTimeMs
        };

        // Update best time if this is better
        if (!_chronoBestTimeMs.HasValue || finalTimeMs < _chronoBestTimeMs.Value)
        {
            _chronoBestTimeMs = finalTimeMs;
        }

        _chronoRunStatus = ChronoRunStatus.Finished;
        _currentScene = Scene.ChronoCompletion;
    }

    private void ResetCurrentChronoRun()
    {
        _chronoCorrectCount = 0;
        _chronoCurrentQuestion = null;
        _chronoLastQuestionId = null;
        // Note: DO NOT clear _usedPhase4QuestionIds here - we want to prevent same questions across all teams in Phase 4
        ResetChronoTimerState();
        _chronoRunStatus = ChronoRunStatus.Idle;
    }

    private void ResetChronoTimerState()
    {
        _chronoTimerState = TimerState.Idle;
        _chronoTimerStartedAtUtc = null;
        _chronoTimerPausedAtUtc = null;
        _chronoTimerAccumulatedPausedMs = 0;
        _chronoTimerFinishedAtUtc = null;
    }

    private long CalculateChronoElapsedMs()
    {
        if (!_chronoTimerStartedAtUtc.HasValue)
            return 0;

        DateTime endTime;
        if (_chronoTimerFinishedAtUtc.HasValue)
            endTime = _chronoTimerFinishedAtUtc.Value;
        else if (_chronoTimerPausedAtUtc.HasValue)
            endTime = _chronoTimerPausedAtUtc.Value;
        else
            endTime = DateTime.UtcNow;

        var elapsed = (endTime - _chronoTimerStartedAtUtc.Value).TotalMilliseconds;
        return (long)(elapsed - _chronoTimerAccumulatedPausedMs);
    }
}
