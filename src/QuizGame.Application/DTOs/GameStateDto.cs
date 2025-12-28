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

    // Phase 1 (Fast Buzzer) state
    public CurrentQuestionDto? CurrentQuestion { get; set; }
    public bool IsCurrentQuestionVisibleOnDisplay { get; set; }
    public Guid? LastQuestionId { get; set; }
    public List<int> BlockedNextQuestionTeamIds { get; set; } = new();
    public List<int> BlockedTeamIdsForCurrentQuestion { get; set; } = new();

    // Phase 2 (List) state
    public CurrentListQuestionDto? CurrentListQuestion { get; set; }
    public bool IsCurrentListQuestionVisibleOnDisplay { get; set; }
    public Guid? LastListQuestionId { get; set; }
    public ListTimerDto ListTimer { get; set; } = new();

    // Phase 3 (Sabotage) state
    public SabotageStateDto Sabotage { get; set; } = new();
}

public class CurrentQuestionDto
{
    public Guid Id { get; set; }
    public string TextFr { get; set; } = string.Empty;
    public string TextNl { get; set; } = string.Empty;
    public string AnswerFr { get; set; } = string.Empty;
    public string AnswerNl { get; set; } = string.Empty;
    public int Difficulty { get; set; }
}

public class TeamDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> Players { get; set; } = new();
    public int Score { get; set; }
}

public class CurrentListQuestionDto
{
    public Guid Id { get; set; }
    public string TextFr { get; set; } = string.Empty;
    public string TextNl { get; set; } = string.Empty;
    public List<ListAnswerDto> Answers { get; set; } = new();
    public int Difficulty { get; set; }
}

public class ListAnswerDto
{
    public string AnswerFr { get; set; } = string.Empty;
    public string AnswerNl { get; set; } = string.Empty;
}

public class ListTimerDto
{
    public int DurationSeconds { get; set; } = 45;
    public TimerState State { get; set; } = TimerState.Idle;
    public DateTime? StartedAtUtc { get; set; }
    public DateTime? PausedAtUtc { get; set; }
    public long AccumulatedPausedMs { get; set; }
    public DateTime? FinishedAtUtc { get; set; }
    public DateTime? BoardsUpVisibleUntilUtc { get; set; }
}

public class SabotageStateDto
{
    // Subphase tracking
    public SabotageSubphase CurrentSubphase { get; set; } = SabotageSubphase.ThemeAssignment;

    // Subphase 1: Theme Assignment
    public List<SabotageThemeDto> SelectedThemes { get; set; } = new();
    public List<TeamThemeAssignmentDto> TeamThemeAssignments { get; set; } = new();
    public int? CurrentPickingTeamIndex { get; set; }
    public int CurrentPickNumber { get; set; } = 1; // 1 = self-select, 2 = sabotage another team
    public bool IsThemeAssignmentComplete { get; set; }

    // Subphase 2: MCQ Questions
    public int? CurrentPlayingTeamIndex { get; set; }
    public int? CurrentThemeIndex { get; set; } // Index within team's themes (0 or 1)
    public CurrentMcqQuestionDto? CurrentMcqQuestion { get; set; }
    public McqChoice? SelectedAnswer { get; set; }
    public bool IsAnswerRevealed { get; set; }
    public int CurrentQuestionInTheme { get; set; } // 0-3 (4 questions per theme)
}

public enum SabotageSubphase
{
    ThemeAssignment,
    McqQuestions
}

public class SabotageThemeDto
{
    public Guid Id { get; set; }
    public string NameFr { get; set; } = string.Empty;
    public string NameNl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class TeamThemeAssignmentDto
{
    public int TeamIndex { get; set; }
    public SabotageThemeDto? SelfSelectedTheme { get; set; }
    public SabotageThemeDto? SabotageTheme { get; set; }
}

public class CurrentMcqQuestionDto
{
    public Guid Id { get; set; }
    public string TextFr { get; set; } = string.Empty;
    public string TextNl { get; set; } = string.Empty;
    public string ChoiceAFr { get; set; } = string.Empty;
    public string ChoiceANl { get; set; } = string.Empty;
    public string ChoiceBFr { get; set; } = string.Empty;
    public string ChoiceBNl { get; set; } = string.Empty;
    public string ChoiceCFr { get; set; } = string.Empty;
    public string ChoiceCNl { get; set; } = string.Empty;
    public McqChoice CorrectChoice { get; set; }
    public int Difficulty { get; set; }
    public SabotageThemeDto Theme { get; set; } = new();
}
