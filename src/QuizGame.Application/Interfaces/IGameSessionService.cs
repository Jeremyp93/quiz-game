using QuizGame.Application.DTOs;
using QuizGame.Domain.Enums;

namespace QuizGame.Application.Interfaces;

public interface IGameSessionService
{
    GameStateDto GetCurrentState();
    void StartGame();
    void CloseGame();
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
    Task GetQuestion();
    void ShowQuestion();
    void ShowAnswer();
    void ApplyBlocksForNextQuestion(List<int> teamIndices);
    void EndPhase();

    // Phase 2 (List)
    Task StartPhase2();
    Task LoadListQuestion();
    Task ShowListQuestion();
    void StartListTimer();
    void PauseListTimer();
    void ResumeListTimer();
    void ResetListTimer();
    void SetListTimerDuration(int durationSeconds);

    // Phase 3 (Sabotage)
    Task StartPhase3();
    void StartThemeAssignment();
    void AssignThemeToTeam(int teamIndex, Guid themeId);
    void UndoLastThemeAssignment();
    Task StartMcqSubphase();
    Task LoadNextMcqQuestion();
    void ShowMcqQuestion();
    void SelectMcqAnswer(McqChoice choice);
    void ClearMcqAnswer();
    void RevealMcqAnswer();
    Task AdvanceToNextMcqQuestion();

    // Phase 4 (Chrono)
    Task StartPhase4();
    void SelectTeamForRun(int teamIndex);
    Task ShowNextChronoQuestion();
    void MarkChronoCorrect();
    void PauseChronoTimer();
    void ResumeChronoTimer();
    void ResetChronoRun();
    void AbortChronoRun();
    void ForceFinishChronoRun();
    void FinishChronoTimerAsNotFinished();

    // Viewer authentication
    string? GetCurrentViewerCode();
    int GetCurrentSessionVersion();
    bool VerifyViewerCode(string code);
}
