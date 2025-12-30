using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuizGame.API.Hubs;
using QuizGame.Application.Interfaces;
using QuizGame.Domain.Enums;

namespace QuizGame.API.Services;

public class TimerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TimerBackgroundService> _logger;
    private const int CheckIntervalMs = 100; // Check every 100ms for accuracy

    public TimerBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<TimerBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timer Background Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckAndUpdateTimers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Timer Background Service");
            }

            await Task.Delay(CheckIntervalMs, stoppingToken);
        }

        _logger.LogInformation("Timer Background Service stopped");
    }

    private async Task CheckAndUpdateTimers()
    {
        using var scope = _serviceProvider.CreateScope();
        var gameSessionService = scope.ServiceProvider.GetRequiredService<IGameSessionService>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<GameHub>>();

        var state = gameSessionService.GetCurrentState();
        var timer = state.ListTimer;
        var now = DateTime.UtcNow;
        bool needsBroadcast = false;

        // Check if timer should finish (hit 0)
        if (timer.State == TimerState.Running && timer.StartedAtUtc.HasValue)
        {
            var elapsed = (now - timer.StartedAtUtc.Value).TotalMilliseconds - timer.AccumulatedPausedMs;
            var remaining = (timer.DurationSeconds * 1000) - elapsed;

            if (remaining <= 0)
            {
                // Timer finished - need to set Finished state
                // We'll do this through a public method on the service
                await FinishTimer(gameSessionService);
                needsBroadcast = true;
            }
        }

        // Check if BoardsUp overlay should be cleared
        if (timer.BoardsUpVisibleUntilUtc.HasValue && now >= timer.BoardsUpVisibleUntilUtc.Value)
        {
            // Clear the overlay
            await ClearBoardsUpOverlay(gameSessionService);
            needsBroadcast = true;
        }

        // Check Chrono countdown timer
        if (state.CurrentPhase == Phase.Chrono &&
            state.Chrono.TimerState == TimerState.Running &&
            state.Chrono.BestTimeMs.HasValue &&
            state.Chrono.TimerStartedAtUtc.HasValue)
        {
            var elapsed = (now - state.Chrono.TimerStartedAtUtc.Value).TotalMilliseconds -
                          state.Chrono.TimerAccumulatedPausedMs;
            var remaining = state.Chrono.BestTimeMs.Value - elapsed;

            if (remaining <= 0 && state.Chrono.CorrectCount < 10)
            {
                // Time expired without reaching 10 - auto-mark as NotFinished
                await FinishChronoTimerAsNotFinished(gameSessionService);
                needsBroadcast = true;
            }
        }

        if (needsBroadcast)
        {
            var updatedState = gameSessionService.GetCurrentState();
            await hubContext.Clients.All.SendAsync("GameStateUpdated", updatedState);
        }
    }

    private Task FinishTimer(IGameSessionService gameSessionService)
    {
        // We need to add a public method to finish the timer
        // For now, we'll use reflection or add a new interface method
        // Let's use a helper method that will be added to GameSessionService
        var method = gameSessionService.GetType().GetMethod("FinishListTimer");
        method?.Invoke(gameSessionService, null);
        return Task.CompletedTask;
    }

    private Task ClearBoardsUpOverlay(IGameSessionService gameSessionService)
    {
        var method = gameSessionService.GetType().GetMethod("ClearBoardsUpOverlay");
        method?.Invoke(gameSessionService, null);
        return Task.CompletedTask;
    }

    private Task FinishChronoTimerAsNotFinished(IGameSessionService gameSessionService)
    {
        var method = gameSessionService.GetType().GetMethod("FinishChronoTimerAsNotFinished");
        method?.Invoke(gameSessionService, null);
        return Task.CompletedTask;
    }
}
