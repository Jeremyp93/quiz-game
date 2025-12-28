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

    private async Task BroadcastGameState()
    {
        var state = _gameSessionService.GetCurrentState();
        await Clients.All.SendAsync("GameStateUpdated", state);
    }
}
