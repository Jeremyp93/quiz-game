using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using QuizGame.Application.Interfaces;

namespace QuizGame.API.Hubs;

public class GameHub : Hub
{
    private readonly IGameSessionService _gameSessionService;

    public GameHub(IGameSessionService gameSessionService)
    {
        _gameSessionService = gameSessionService;
    }

    public override async Task OnConnectedAsync()
    {
        // Send current state to newly connected client
        await Clients.Caller.SendAsync("GameStateUpdated", _gameSessionService.GetCurrentState());
        await base.OnConnectedAsync();
    }

    [Authorize(Policy = "GM")]

    public async Task StartGame()
    {
        _gameSessionService.StartGame();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task CloseGame()
    {
        _gameSessionService.CloseGame();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task SetPlayers(List<string> playerNames)
    {
        _gameSessionService.SetPlayers(playerNames);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task CreateTeams()
    {
        _gameSessionService.CreateTeams();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task RenameTeam(int teamIndex, string newName)
    {
        _gameSessionService.RenameTeam(teamIndex, newName);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task MovePlayer(string playerName, int toTeamIndex)
    {
        _gameSessionService.MovePlayer(playerName, toTeamIndex);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task AdjustScore(int teamIndex, int delta)
    {
        _gameSessionService.AdjustScore(teamIndex, delta);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task ShowTeamsScene()
    {
        _gameSessionService.ShowTeamsScene();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task ShowScoreboard()
    {
        _gameSessionService.ShowScoreboard();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]
    
    public async Task BackToGame()
    {
        _gameSessionService.BackToGame();
        await BroadcastGameState();
    }

    // Phase 1 (Fast Buzzer) Methods

    [Authorize(Policy = "GM")]

    public async Task StartPhase1()
    {
        await _gameSessionService.StartPhase1();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task GetQuestion()
    {
        await _gameSessionService.GetQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ShowQuestion()
    {
        _gameSessionService.ShowQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ShowAnswer()
    {
        _gameSessionService.ShowAnswer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ApplyBlocksForNextQuestion(List<int> teamIndices)
    {
        _gameSessionService.ApplyBlocksForNextQuestion(teamIndices);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task EndPhase()
    {
        _gameSessionService.EndPhase();
        await BroadcastGameState();
    }

    // Phase 2 (List) Methods

    [Authorize(Policy = "GM")]

    public async Task StartPhase2()
    {
        await _gameSessionService.StartPhase2();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task LoadListQuestion()
    {
        await _gameSessionService.LoadListQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ShowListQuestion()
    {
        await _gameSessionService.ShowListQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task SetListTimerDuration(int durationSeconds)
    {
        _gameSessionService.SetListTimerDuration(durationSeconds);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task StartListTimer()
    {
        _gameSessionService.StartListTimer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task PauseListTimer()
    {
        _gameSessionService.PauseListTimer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ResumeListTimer()
    {
        _gameSessionService.ResumeListTimer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ResetListTimer()
    {
        _gameSessionService.ResetListTimer();
        await BroadcastGameState();
    }

    // Phase 3 (Sabotage) Methods

    [Authorize(Policy = "GM")]

    public async Task StartPhase3()
    {
        await _gameSessionService.StartPhase3();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task StartThemeAssignment()
    {
        _gameSessionService.StartThemeAssignment();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task AssignThemeToTeam(int teamIndex, string themeId)
    {
        _gameSessionService.AssignThemeToTeam(teamIndex, Guid.Parse(themeId));
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task UndoLastThemeAssignment()
    {
        _gameSessionService.UndoLastThemeAssignment();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task StartMcqSubphase()
    {
        await _gameSessionService.StartMcqSubphase();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task LoadNextMcqQuestion()
    {
        await _gameSessionService.LoadNextMcqQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ShowMcqQuestion()
    {
        _gameSessionService.ShowMcqQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task SelectMcqAnswer(int choice)
    {
        _gameSessionService.SelectMcqAnswer((QuizGame.Domain.Enums.McqChoice)choice);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ClearMcqAnswer()
    {
        _gameSessionService.ClearMcqAnswer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task RevealMcqAnswer()
    {
        _gameSessionService.RevealMcqAnswer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task AdvanceToNextMcqQuestion()
    {
        await _gameSessionService.AdvanceToNextMcqQuestion();
        await BroadcastGameState();
    }

    // ============================================================
    // Phase 4 (Chrono) Hub Methods
    // ============================================================

    [Authorize(Policy = "GM")]

    public async Task StartPhase4()
    {
        await _gameSessionService.StartPhase4();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task SelectTeamForChronoRun(int teamIndex)
    {
        _gameSessionService.SelectTeamForRun(teamIndex);
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ShowNextChronoQuestion()
    {
        await _gameSessionService.ShowNextChronoQuestion();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task MarkChronoCorrect()
    {
        _gameSessionService.MarkChronoCorrect();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task PauseChronoTimer()
    {
        _gameSessionService.PauseChronoTimer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ResumeChronoTimer()
    {
        _gameSessionService.ResumeChronoTimer();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ResetChronoRun()
    {
        _gameSessionService.ResetChronoRun();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task AbortChronoRun()
    {
        _gameSessionService.AbortChronoRun();
        await BroadcastGameState();
    }

    [Authorize(Policy = "GM")]

    public async Task ForceFinishChronoRun()
    {
        _gameSessionService.ForceFinishChronoRun();
        await BroadcastGameState();
    }

    private async Task BroadcastGameState()
    {
        var state = _gameSessionService.GetCurrentState();
        await Clients.All.SendAsync("GameStateUpdated", state);
    }
}
