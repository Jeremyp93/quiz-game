using QuizGame.Application.DTOs;
using QuizGame.Domain.Enums;

namespace QuizGame.Application.Interfaces;

public interface IGameSessionService
{
    GameStateDto GetCurrentState();
    void StartGame();
    void SetPlayers(List<string> playerNames);
    void CreateTeams();
    void RenameTeam(int teamIndex, string newName);
    void MovePlayer(string playerName, int toTeamIndex);
    void AdjustScore(int teamIndex, int delta);
    void ShowTeamsScene();
    void ShowScoreboard();
    void BackToGame();

    // Phase 1 (Fast Buzzer)
    Task StartPhase1();
    Task ShowQuestion();
    void ShowAnswer();
    void ApplyBlocksForNextQuestion(List<int> teamIndices);
    void EndPhase();
}
