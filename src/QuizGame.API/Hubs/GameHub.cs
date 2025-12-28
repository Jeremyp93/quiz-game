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

    public async Task StartGame()
    {
        _gameSessionService.StartGame();
        await BroadcastGameState();
    }

    public async Task SetPlayers(List<string> playerNames)
    {
        _gameSessionService.SetPlayers(playerNames);
        await BroadcastGameState();
    }

    public async Task CreateTeams()
    {
        _gameSessionService.CreateTeams();
        await BroadcastGameState();
    }

    public async Task RenameTeam(int teamIndex, string newName)
    {
        _gameSessionService.RenameTeam(teamIndex, newName);
        await BroadcastGameState();
    }

    public async Task MovePlayer(string playerName, int toTeamIndex)
    {
        _gameSessionService.MovePlayer(playerName, toTeamIndex);
        await BroadcastGameState();
    }

    public async Task AdjustScore(int teamIndex, int delta)
    {
        _gameSessionService.AdjustScore(teamIndex, delta);
        await BroadcastGameState();
    }

    public async Task ShowTeamsScene()
    {
        _gameSessionService.ShowTeamsScene();
        await BroadcastGameState();
    }

    public async Task ShowScoreboard()
    {
        _gameSessionService.ShowScoreboard();
        await BroadcastGameState();
    }

    public async Task BackToGame()
    {
        _gameSessionService.BackToGame();
        await BroadcastGameState();
    }

    // Phase 1 (Fast Buzzer) Methods

    public async Task StartPhase1()
    {
        await _gameSessionService.StartPhase1();
        await BroadcastGameState();
    }

    public async Task GetQuestion()
    {
        await _gameSessionService.GetQuestion();
        await BroadcastGameState();
    }

    public async Task ShowQuestion()
    {
        _gameSessionService.ShowQuestion();
        await BroadcastGameState();
    }

    public async Task ShowAnswer()
    {
        _gameSessionService.ShowAnswer();
        await BroadcastGameState();
    }

    public async Task ApplyBlocksForNextQuestion(List<int> teamIndices)
    {
        _gameSessionService.ApplyBlocksForNextQuestion(teamIndices);
        await BroadcastGameState();
    }

    public async Task EndPhase()
    {
        _gameSessionService.EndPhase();
        await BroadcastGameState();
    }

    // Phase 2 (List) Methods

    public async Task StartPhase2()
    {
        await _gameSessionService.StartPhase2();
        await BroadcastGameState();
    }

    public async Task LoadListQuestion()
    {
        await _gameSessionService.LoadListQuestion();
        await BroadcastGameState();
    }

    public async Task ShowListQuestion()
    {
        await _gameSessionService.ShowListQuestion();
        await BroadcastGameState();
    }

    public async Task StartListTimer()
    {
        _gameSessionService.StartListTimer();
        await BroadcastGameState();
    }

    public async Task PauseListTimer()
    {
        _gameSessionService.PauseListTimer();
        await BroadcastGameState();
    }

    public async Task ResumeListTimer()
    {
        _gameSessionService.ResumeListTimer();
        await BroadcastGameState();
    }

    public async Task ResetListTimer()
    {
        _gameSessionService.ResetListTimer();
        await BroadcastGameState();
    }

    // Phase 3 (Sabotage) Methods

    public async Task StartPhase3()
    {
        await _gameSessionService.StartPhase3();
        await BroadcastGameState();
    }

    public async Task AssignThemeToTeam(int teamIndex, string themeId)
    {
        _gameSessionService.AssignThemeToTeam(teamIndex, Guid.Parse(themeId));
        await BroadcastGameState();
    }

    public async Task UndoLastThemeAssignment()
    {
        _gameSessionService.UndoLastThemeAssignment();
        await BroadcastGameState();
    }

    public async Task StartMcqSubphase()
    {
        await _gameSessionService.StartMcqSubphase();
        await BroadcastGameState();
    }

    public async Task LoadNextMcqQuestion()
    {
        await _gameSessionService.LoadNextMcqQuestion();
        await BroadcastGameState();
    }

    public async Task ShowMcqQuestion()
    {
        _gameSessionService.ShowMcqQuestion();
        await BroadcastGameState();
    }

    public async Task SelectMcqAnswer(int choice)
    {
        _gameSessionService.SelectMcqAnswer((QuizGame.Domain.Enums.McqChoice)choice);
        await BroadcastGameState();
    }

    public async Task RevealMcqAnswer()
    {
        _gameSessionService.RevealMcqAnswer();
        await BroadcastGameState();
    }

    public async Task AdvanceToNextMcqQuestion()
    {
        await _gameSessionService.AdvanceToNextMcqQuestion();
        await BroadcastGameState();
    }

    private async Task BroadcastGameState()
    {
        var state = _gameSessionService.GetCurrentState();
        await Clients.All.SendAsync("GameStateUpdated", state);
    }
}
