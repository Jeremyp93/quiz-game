import { useState } from 'react';
import { useGameState } from '../hooks/useGameState';
import { gameService } from '../services/gameService';
import { Scene, Phase } from '../types';
import './ControlPage.css';

export default function ControlPage() {
  const { gameState, isConnected } = useGameState();
  const [playerInput, setPlayerInput] = useState('');
  const [playerNames, setPlayerNames] = useState<string[]>([]);
  const [selectedBlockedTeams, setSelectedBlockedTeams] = useState<number[]>([]);

  const handleStartGame = async () => {
    await gameService.startGame();
  };

  const handleBulkPlayerInput = () => {
    // Parse textarea input line by line
    const lines = playerInput.split('\n').map(line => line.trim()).filter(line => line.length > 0);
    setPlayerNames(lines);
  };

  const handleRemovePlayer = (index: number) => {
    setPlayerNames(playerNames.filter((_, i) => i !== index));
    // Update textarea
    const updatedNames = playerNames.filter((_, i) => i !== index);
    setPlayerInput(updatedNames.join('\n'));
  };

  const handleSetPlayers = async () => {
    await gameService.setPlayers(playerNames);
  };

  const handleCreateTeams = async () => {
    await gameService.createTeams();
  };

  const handleRenameTeam = async (teamIndex: number, newName: string) => {
    await gameService.renameTeam(teamIndex, newName);
  };

  const handleMovePlayer = async (playerName: string, toTeamIndex: number) => {
    await gameService.movePlayer(playerName, toTeamIndex);
  };

  const handleAdjustScore = async (teamIndex: number, delta: number) => {
    await gameService.adjustScore(teamIndex, delta);
  };

  const handleShowTeams = async () => {
    await gameService.showTeamsScene();
  };

  const handleShowScoreboard = async () => {
    await gameService.showScoreboard();
  };

  const handleBackToGame = async () => {
    await gameService.backToGame();
  };

  // Phase 1 (Fast Buzzer) handlers
  const handleStartPhase1 = async () => {
    await gameService.startPhase1();
    setSelectedBlockedTeams([]);
  };

  const handleGetQuestion = async () => {
    await gameService.getQuestion();
  };

  const handleShowQuestion = async () => {
    await gameService.showQuestion();
  };

  const handleShowAnswer = async () => {
    await gameService.showAnswer();
  };

  const toggleTeamBlock = (teamIndex: number) => {
    setSelectedBlockedTeams(prev =>
      prev.includes(teamIndex)
        ? prev.filter(i => i !== teamIndex)
        : [...prev, teamIndex]
    );
  };

  const handleApplyBlocks = async () => {
    await gameService.applyBlocksForNextQuestion(selectedBlockedTeams);
  };

  const handleEndPhase = async () => {
    await gameService.endPhase();
    setSelectedBlockedTeams([]);
  };

  // Phase 2 (List) handlers
  const handleStartPhase2 = async () => {
    await gameService.startPhase2();
  };

  const handleLoadListQuestion = async () => {
    await gameService.loadListQuestion();
  };

  const handleShowListQuestion = async () => {
    await gameService.showListQuestion();
  };

  const handleStartTimer = async () => {
    await gameService.startListTimer();
  };

  const handlePauseTimer = async () => {
    await gameService.pauseListTimer();
  };

  const handleResumeTimer = async () => {
    await gameService.resumeListTimer();
  };

  const handleResetTimer = async () => {
    await gameService.resetListTimer();
  };

  // Phase 3 (Sabotage) handlers
  const handleStartPhase3 = async () => {
    await gameService.startPhase3();
  };

  const handleAssignTheme = async (teamIndex: number, themeId: string) => {
    await gameService.assignThemeToTeam(teamIndex, themeId);
  };

  const handleUndoThemeAssignment = async () => {
    await gameService.undoLastThemeAssignment();
  };

  const handleStartMcqSubphase = async () => {
    await gameService.startMcqSubphase();
  };

  // MCQ Question handlers
  const handleLoadNextMcqQuestion = async () => {
    await gameService.loadNextMcqQuestion();
  };

  const handleShowMcqQuestion = async () => {
    await gameService.showMcqQuestion();
  };

  const handleSelectMcqAnswer = async (choice: number) => {
    await gameService.selectMcqAnswer(choice);
  };

  const handleRevealMcqAnswer = async () => {
    await gameService.revealMcqAnswer();
  };

  const handleAdvanceToNextMcqQuestion = async () => {
    await gameService.advanceToNextMcqQuestion();
  };

  if (!isConnected) {
    return (
      <div className="control-page">
        <div className="loading">Connecting to server...</div>
      </div>
    );
  }

  return (
    <div className="control-page">
      <h1>Game Master Control Panel</h1>

      <div className="control-layout">
        <div className="left-panel">
          {!gameState.isGameStarted ? (
            <div className="setup-section">
              <h2>Start New Game</h2>
              <button onClick={handleStartGame} className="btn-start">
                Start Game
              </button>
            </div>
          ) : !gameState.teams.length ? (
            <div className="setup-section">
              <h2>Setup Players</h2>

              <div className="bulk-player-input">
                <label>Enter player names (one per line):</label>
                <textarea
                  value={playerInput}
                  onChange={e => setPlayerInput(e.target.value)}
                  onBlur={handleBulkPlayerInput}
                  placeholder="Alice&#10;Bob&#10;Charlie&#10;..."
                  rows={10}
                  className="player-textarea"
                />
              </div>

              {playerNames.length > 0 && (
                <>
                  <div className="player-count">
                    {playerNames.length} player{playerNames.length !== 1 ? 's' : ''} entered
                  </div>
                  <div className="player-list">
                    {playerNames.map((name, index) => (
                      <div key={index} className="player-item">
                        <span>{name}</span>
                        <button onClick={() => handleRemovePlayer(index)} className="btn-remove">×</button>
                      </div>
                    ))}
                  </div>
                  <button onClick={handleSetPlayers} className="btn-primary">
                    Confirm Players ({playerNames.length})
                  </button>
                </>
              )}

              {gameState.players.length > 0 && (
                <div className="players-confirmed">
                  <h3>Players Confirmed: {gameState.players.length}</h3>
                  <button onClick={handleCreateTeams} className="btn-create-teams">
                    Create Teams (Random)
                  </button>
                </div>
              )}
            </div>
          ) : (
            <div className="teams-management">
              <h2>Teams Management</h2>

              {gameState.teams.map((team, teamIndex) => (
                <div key={teamIndex} className="team-card">
                  <input
                    type="text"
                    value={team.name}
                    onChange={e => handleRenameTeam(teamIndex, e.target.value)}
                    className="team-name-input"
                  />

                  <div className="team-players">
                    {team.players.map((player, playerIndex) => (
                      <div key={playerIndex} className="team-player">
                        <span>{player}</span>
                        <select
                          onChange={e => handleMovePlayer(player, Number(e.target.value))}
                          value={teamIndex}
                        >
                          {gameState.teams.map((t, i) => (
                            <option key={i} value={i}>
                              Move to {t.name}
                            </option>
                          ))}
                        </select>
                      </div>
                    ))}
                  </div>
                </div>
              ))}
            </div>
          )}

          {gameState.teams.length > 0 && gameState.currentPhase === Phase.Setup && (
            <div className="phase-selector">
              <h2>Select Phase</h2>
              <button onClick={handleStartPhase1} className="btn-phase">
                Start Fast Buzzer Phase
              </button>
              <button onClick={handleStartPhase2} className="btn-phase" style={{ marginTop: '1rem' }}>
                Start List Phase
              </button>
              <button onClick={handleStartPhase3} className="btn-phase" style={{ marginTop: '1rem' }}>
                Start Sabotage Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.FastBuzzer && (
            <div className="phase-panel">
              <h2>Phase 1: Fast Buzzer</h2>

              {gameState.currentQuestion && (
                <div className="current-question-gm">
                  <h3>Current Question (GM View)</h3>
                  <div className={`difficulty-badge difficulty-${gameState.currentQuestion.difficulty}`}>
                    Difficulty: {gameState.currentQuestion.difficulty}
                  </div>
                  <div className="question-preview">
                    <div className="q-lang">
                      <strong>FR:</strong> {gameState.currentQuestion.textFr}
                    </div>
                    <div className="q-lang">
                      <strong>NL:</strong> {gameState.currentQuestion.textNl}
                    </div>
                    <div className="answer-preview">
                      <div className="q-lang">
                        <strong>Answer FR:</strong> {gameState.currentQuestion.answerFr}
                      </div>
                      <div className="q-lang">
                        <strong>Answer NL:</strong> {gameState.currentQuestion.answerNl}
                      </div>
                    </div>
                  </div>
                </div>
              )}

              <div className="phase-controls">
                <button onClick={handleGetQuestion} className="btn-phase-action btn-get-question">
                  Get Question (Preview)
                </button>
                <button
                  onClick={handleShowQuestion}
                  className="btn-phase-action btn-show-question"
                  disabled={!gameState.currentQuestion || gameState.isCurrentQuestionVisibleOnDisplay}
                >
                  Show Question on Display
                </button>
                <button
                  onClick={handleShowAnswer}
                  className="btn-phase-action btn-show-answer"
                  disabled={!gameState.currentQuestion || !gameState.isCurrentQuestionVisibleOnDisplay}
                >
                  Show Answer
                </button>
              </div>

              <div className="blocking-section">
                <h3>Block Team(s) For Next Question</h3>
                <div className="blocking-teams">
                  {gameState.teams.map((team, index) => (
                    <label key={index} className="blocking-team-item">
                      <input
                        type="checkbox"
                        checked={selectedBlockedTeams.includes(index)}
                        onChange={() => toggleTeamBlock(index)}
                      />
                      <span>{team.name}</span>
                    </label>
                  ))}
                </div>
                <button
                  onClick={handleApplyBlocks}
                  className="btn-apply-blocks"
                  disabled={selectedBlockedTeams.length === 0}
                >
                  Apply Blocks ({selectedBlockedTeams.length})
                </button>

                {gameState.blockedNextQuestionTeamIds.length > 0 && (
                  <div className="blocks-pending">
                    Blocked for next question: {gameState.blockedNextQuestionTeamIds.map(i => gameState.teams[i]?.name).join(', ')}
                  </div>
                )}
              </div>

              <button onClick={handleEndPhase} className="btn-end-phase">
                End Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.List && (
            <div className="phase-panel">
              <h2>Phase 2: List</h2>

              {gameState.currentListQuestion && (
                <div className="current-question-gm">
                  <h3>Current List Question (GM View)</h3>
                  <div className={`difficulty-badge difficulty-${gameState.currentListQuestion.difficulty}`}>
                    Difficulty: {gameState.currentListQuestion.difficulty}
                  </div>
                  <div className="question-preview">
                    <div className="q-lang">
                      <strong>FR:</strong> {gameState.currentListQuestion.textFr}
                    </div>
                    <div className="q-lang">
                      <strong>NL:</strong> {gameState.currentListQuestion.textNl}
                    </div>
                    <div className="answer-preview">
                      <h4>Accepted Answers ({gameState.currentListQuestion.answers.length})</h4>
                      {gameState.currentListQuestion.answers.map((answer, i) => (
                        <div key={i} className="list-answer-item">
                          <div className="q-lang">
                            <strong>FR:</strong> {answer.answerFr}
                          </div>
                          <div className="q-lang">
                            <strong>NL:</strong> {answer.answerNl}
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>
                </div>
              )}

              {gameState.listTimer && (
                <div className="timer-status-panel">
                  <h3>Timer Status</h3>
                  <div className="timer-info">
                    <div className="timer-state">
                      State: <strong>{['Idle', 'Running', 'Paused', 'Finished'][gameState.listTimer.state]}</strong>
                    </div>
                    <div className="timer-duration">
                      Duration: {gameState.listTimer.durationSeconds} seconds
                    </div>
                  </div>
                </div>
              )}

              <div className="phase-controls">
                <button onClick={handleLoadListQuestion} className="btn-phase-action btn-load-question">
                  Load Question
                </button>
                <button
                  onClick={handleShowListQuestion}
                  className="btn-phase-action btn-show-question"
                >
                  Show Question
                </button>
              </div>

              <div className="phase-controls">
                <button
                  onClick={handleStartTimer}
                  className="btn-phase-action btn-start-timer"
                  disabled={gameState.listTimer.state !== 0 || gameState.currentScene !== Scene.ListQuestion}
                >
                  Start Timer
                </button>
                <button
                  onClick={handlePauseTimer}
                  className="btn-phase-action btn-pause-timer"
                  disabled={gameState.listTimer.state !== 1}
                >
                  Pause Timer
                </button>
                <button
                  onClick={handleResumeTimer}
                  className="btn-phase-action btn-resume-timer"
                  disabled={gameState.listTimer.state !== 2}
                >
                  Resume Timer
                </button>
                <button
                  onClick={handleResetTimer}
                  className="btn-phase-action btn-reset-timer"
                  disabled={gameState.listTimer.state === 0}
                >
                  Reset Timer
                </button>
              </div>

              <button onClick={handleEndPhase} className="btn-end-phase">
                End Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.Sabotage && (
            <div className="phase-panel">
              <h2>Phase 3: Sabotage</h2>

              {gameState.sabotage.currentSubphase === 0 && (
                <>
                  <h3>Subphase 1: Theme Assignment</h3>

                  {gameState.sabotage.currentPickingTeamIndex !== null &&
                   gameState.sabotage.currentPickingTeamIndex !== undefined && (
                    <div className="current-picker">
                      <strong>Current Picking Team:</strong>{' '}
                      {gameState.teams[gameState.sabotage.currentPickingTeamIndex]?.name}
                      <br />
                      <strong>Pick Number:</strong>{' '}
                      {gameState.sabotage.currentPickNumber === 1 ? (
                        <span style={{ color: '#eab308' }}>✨ Pick 1 (Choose for yourself)</span>
                      ) : (
                        <span style={{ color: '#dc2626' }}>💣 Pick 2 (Sabotage another team)</span>
                      )}
                    </div>
                  )}

                  <div className="theme-assignment-grid">
                    {gameState.sabotage.selectedThemes.map((theme) => {
                      const isAssigned = gameState.sabotage.teamThemeAssignments.some(
                        ta => (ta.selfSelectedTheme?.id === theme.id) || (ta.sabotageTheme?.id === theme.id)
                      );

                      return (
                        <div key={theme.id} className={`theme-card ${isAssigned ? 'assigned' : ''}`}>
                          <div className="theme-name">
                            <span className="theme-icon-inline">{theme.icon}</span> {theme.nameFr} / {theme.nameNl}
                          </div>
                          {!isAssigned && gameState.sabotage.currentPickingTeamIndex !== null && (
                            <div className="theme-assign-buttons">
                              {gameState.teams.map((team, idx) => {
                                const teamAssignment = gameState.sabotage.teamThemeAssignments.find(
                                  ta => ta.teamIndex === idx
                                );

                                // Pick 1: can only assign to current picking team
                                if (gameState.sabotage.currentPickNumber === 1) {
                                  if (idx !== gameState.sabotage.currentPickingTeamIndex) return null;
                                  if (teamAssignment?.selfSelectedTheme) return null; // Already has self-selected
                                } else {
                                  // Pick 2: can only assign to OTHER teams
                                  if (idx === gameState.sabotage.currentPickingTeamIndex) return null;
                                  if (teamAssignment?.sabotageTheme) return null; // Already has sabotage
                                }

                                return (
                                  <button
                                    key={idx}
                                    onClick={() => handleAssignTheme(idx, theme.id)}
                                    className="btn-assign-theme"
                                  >
                                    {gameState.sabotage.currentPickNumber === 1 ? '✨' : '💣'} → {team.name}
                                  </button>
                                );
                              })}
                            </div>
                          )}
                        </div>
                      );
                    })}
                  </div>

                  <div className="team-assignments-grid">
                    {gameState.teams.map((team, idx) => {
                      const assignment = gameState.sabotage.teamThemeAssignments.find(ta => ta.teamIndex === idx);
                      return (
                        <div key={idx} className="team-assignment-card">
                          <h4>{team.name}</h4>
                          <div className="assigned-themes-list">
                            {assignment?.selfSelectedTheme && (
                              <div className="assigned-theme-badge self-selected">
                                ✨ {assignment.selfSelectedTheme.nameFr} / {assignment.selfSelectedTheme.nameNl}
                              </div>
                            )}
                            {assignment?.sabotageTheme && (
                              <div className="assigned-theme-badge sabotage">
                                💣 {assignment.sabotageTheme.nameFr} / {assignment.sabotageTheme.nameNl}
                              </div>
                            )}
                            {!assignment?.selfSelectedTheme && !assignment?.sabotageTheme && (
                              <div className="no-themes">No themes assigned yet</div>
                            )}
                          </div>
                        </div>
                      );
                    })}
                  </div>

                  <div className="phase-controls">
                    <button
                      onClick={handleUndoThemeAssignment}
                      className="btn-undo"
                      disabled={gameState.sabotage.isThemeAssignmentComplete}
                    >
                      ↶ Undo Last Assignment
                    </button>
                    <button
                      onClick={handleStartMcqSubphase}
                      className="btn-start-mcq"
                      disabled={!gameState.sabotage.isThemeAssignmentComplete}
                    >
                      Start MCQ Subphase →
                    </button>
                  </div>
                </>
              )}

              {gameState.sabotage.currentSubphase === 1 && (
                <>
                  <h3>Subphase 2: MCQ Questions</h3>

                  {gameState.sabotage.currentPlayingTeamIndex !== null &&
                   gameState.sabotage.currentPlayingTeamIndex !== undefined && (
                    <div className="mcq-current-info">
                      <div><strong>Current Team:</strong> {gameState.teams[gameState.sabotage.currentPlayingTeamIndex]?.name}</div>
                      <div>
                        <strong>Theme:</strong>{' '}
                        {gameState.sabotage.currentThemeIndex === 0 ? (
                          <span>✨ Self-Selected</span>
                        ) : (
                          <span>💣 {gameState.sabotage.currentMcqQuestion?.theme.nameNl} / {gameState.sabotage.currentMcqQuestion?.theme.nameFr} (Sabotage)</span>
                        )}
                        {' - '}Question {gameState.sabotage.currentQuestionInTheme + 1}/4
                      </div>
                    </div>
                  )}

                  {!gameState.sabotage.currentMcqQuestion && (
                    <div className="phase-controls">
                      <button
                        onClick={handleLoadNextMcqQuestion}
                        className="btn-phase-action btn-load-question"
                      >
                        📥 Load Question (Preview)
                      </button>
                    </div>
                  )}

                  {gameState.sabotage.currentMcqQuestion && (
                    <>
                      <div className="mcq-question-info">
                        <h4>Question Preview</h4>
                        <div className="mcq-question-text">
                          <div>{gameState.sabotage.currentMcqQuestion.textFr}</div>
                          <div>{gameState.sabotage.currentMcqQuestion.textNl}</div>
                        </div>
                        <div className="mcq-choices">
                          <div
                            className={`mcq-choice ${gameState.sabotage.selectedAnswer === 0 ? 'mcq-choice-selected' : ''} ${gameState.sabotage.isAnswerRevealed ? 'disabled' : ''}`}
                            onClick={() => !gameState.sabotage.isAnswerRevealed && handleSelectMcqAnswer(0)}
                          >
                            <strong>A:</strong> {gameState.sabotage.currentMcqQuestion.choiceAFr} / {gameState.sabotage.currentMcqQuestion.choiceANl}
                          </div>
                          <div
                            className={`mcq-choice ${gameState.sabotage.selectedAnswer === 1 ? 'mcq-choice-selected' : ''} ${gameState.sabotage.isAnswerRevealed ? 'disabled' : ''}`}
                            onClick={() => !gameState.sabotage.isAnswerRevealed && handleSelectMcqAnswer(1)}
                          >
                            <strong>B:</strong> {gameState.sabotage.currentMcqQuestion.choiceBFr} / {gameState.sabotage.currentMcqQuestion.choiceBNl}
                          </div>
                          <div
                            className={`mcq-choice ${gameState.sabotage.selectedAnswer === 2 ? 'mcq-choice-selected' : ''} ${gameState.sabotage.isAnswerRevealed ? 'disabled' : ''}`}
                            onClick={() => !gameState.sabotage.isAnswerRevealed && handleSelectMcqAnswer(2)}
                          >
                            <strong>C:</strong> {gameState.sabotage.currentMcqQuestion.choiceCFr} / {gameState.sabotage.currentMcqQuestion.choiceCNl}
                          </div>
                        </div>
                        <div className="mcq-meta">
                          <strong>Difficulty:</strong> {'⭐'.repeat(gameState.sabotage.currentMcqQuestion.difficulty)} |
                          <strong> Correct Answer:</strong> <span style={{ color: '#10b981', fontWeight: 'bold' }}>
                            {['A', 'B', 'C'][gameState.sabotage.currentMcqQuestion.correctChoice]}
                          </span>
                        </div>
                      </div>

                      <div className="mcq-control-buttons">
                        <button
                          onClick={handleShowMcqQuestion}
                          disabled={gameState.currentScene === Scene.SabotageMcqQuestion ||
                                    gameState.currentScene === Scene.SabotageMcqAnswer}
                          className="btn-phase-action btn-show-question"
                        >
                          📺 Show Question on Display
                        </button>

                        <button
                          onClick={handleRevealMcqAnswer}
                          disabled={gameState.sabotage.selectedAnswer === undefined ||
                                    gameState.sabotage.isAnswerRevealed}
                          className="btn-phase-action btn-reveal"
                        >
                          ✓ Reveal Answer
                        </button>

                        <button
                          onClick={handleAdvanceToNextMcqQuestion}
                          disabled={!gameState.sabotage.isAnswerRevealed}
                          className="btn-phase-action btn-next-question"
                        >
                          ➡️ Next Question
                        </button>
                      </div>
                    </>
                  )}

                  {gameState.sabotage.currentPlayingTeamIndex === null && (
                    <div className="mcq-complete-message">
                      <p>All teams have completed the MCQ subphase!</p>
                    </div>
                  )}
                </>
              )}

              <button onClick={handleEndPhase} className="btn-end-phase" style={{ marginTop: '2rem' }}>
                End Phase
              </button>
            </div>
          )}

          {gameState.teams.length > 0 && (
            <div className="scene-controls">
              <h2>Display Controls</h2>
              <div className="scene-buttons">
                <button onClick={handleShowTeams} className="btn-scene">
                  Display Teams
                </button>
                <button onClick={handleShowScoreboard} className="btn-scene">
                  Show Scoreboard
                </button>
                {gameState.currentScene === Scene.Scoreboard && (
                  <button onClick={handleBackToGame} className="btn-scene">
                    Back to Game
                  </button>
                )}
              </div>
              <div className="current-scene">
                Current Scene: <strong>{Scene[gameState.currentScene]}</strong>
              </div>
            </div>
          )}
        </div>

        <div className="right-panel">
          {gameState.teams.length > 0 && (
            <div className="teams-scores">
              <h2>Teams & Scores</h2>

              {gameState.teams.map((team, teamIndex) => (
                <div key={teamIndex} className="score-card">
                  <div className="score-header">
                    <h3>{team.name}</h3>
                    <div className="score-value">{team.score}</div>
                  </div>

                  <div className="score-controls">
                    <button onClick={() => handleAdjustScore(teamIndex, 1)} className="btn-plus">
                      +1
                    </button>
                    <button onClick={() => handleAdjustScore(teamIndex, -1)} className="btn-minus">
                      -1
                    </button>
                  </div>

                  <div className="team-members">
                    {team.players.map((player, i) => (
                      <span key={i} className="player-badge">
                        {player}
                      </span>
                    ))}
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
