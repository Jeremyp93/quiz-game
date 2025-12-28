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
