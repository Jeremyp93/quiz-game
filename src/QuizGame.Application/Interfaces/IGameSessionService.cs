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
}
