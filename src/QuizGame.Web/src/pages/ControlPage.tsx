import { useState, useEffect } from 'react';
import { useGameState } from '../hooks/useGameState';
import { gameService } from '../services/gameService';
import { Scene, Phase, ChronoRunStatus, ChronoResultStatus } from '../types';
import { TEAM_COLORS } from '../constants/teamColors';
import styles from './ControlPage.module.css';

export default function ControlPage() {
  const { gameState, isConnected } = useGameState();
  const [playerInput, setPlayerInput] = useState('');
  const [playerNames, setPlayerNames] = useState<string[]>([]);
  const [selectedBlockedTeams, setSelectedBlockedTeams] = useState<number[]>([]);
  const [listTimerDuration, setListTimerDuration] = useState(45);

  // Sync timer duration from backend state
  useEffect(() => {
    if (gameState.listTimer) {
      setListTimerDuration(gameState.listTimer.durationSeconds);
    }
  }, [gameState.listTimer.durationSeconds]);

  const handleStartGame = async () => {
    await gameService.startGame();
  };

  const handleCloseGame = async () => {
    const confirmed = window.confirm(
      'WARNING: This will close the current game session and clear all data (players, teams, scores, phase progress).\n\n' +
      'All viewers will be disconnected and a new viewer code will be generated.\n\n' +
      'This action CANNOT be undone.\n\n' +
      'Continue?'
    );
    if (confirmed) {
      await gameService.closeGame();
    }
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

  const handleSetListTimerDuration = async () => {
    await gameService.setListTimerDuration(listTimerDuration);
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

  const handleClearMcqAnswer = async () => {
    await gameService.clearMcqAnswer();
  };

  const handleRevealMcqAnswer = async () => {
    await gameService.revealMcqAnswer();
  };

  const handleAdvanceToNextMcqQuestion = async () => {
    await gameService.advanceToNextMcqQuestion();
  };

  // Phase 4 (Chrono) handlers
  const handleStartPhase4 = async () => {
    await gameService.startPhase4();
  };

  const handleSelectChronoTeam = async (teamIndex: number) => {
    const chrono = gameState.chrono;

    // Confirm if switching mid-run
    if (chrono.runStatus === 1 || chrono.runStatus === 2) { // Running or Paused
      if (!confirm('Switching teams will reset the current run. Continue?')) {
        return;
      }
    }

    await gameService.selectTeamForChronoRun(teamIndex);
  };

  const handleShowNextChronoQuestion = async () => {
    await gameService.showNextChronoQuestion();
  };

  const handleMarkChronoCorrect = async () => {
    await gameService.markChronoCorrect();
  };

  const handlePauseChronoTimer = async () => {
    await gameService.pauseChronoTimer();
  };

  const handleResumeChronoTimer = async () => {
    await gameService.resumeChronoTimer();
  };

  const handleResetChronoRun = async () => {
    if (!confirm('Reset will clear this attempt but preserve all team results. Continue?')) {
      return;
    }
    await gameService.resetChronoRun();
  };

  const handleAbortChronoRun = async () => {
    if (!confirm('Abort will save this run as Aborted. This cannot be undone. Continue?')) {
      return;
    }
    await gameService.abortChronoRun();
  };

  const handleForceFinishChronoRun = async () => {
    if (!confirm('Force Finish will save the current time even if not at 10/10. Continue?')) {
      return;
    }
    await gameService.forceFinishChronoRun();
  };

  const formatTime = (ms: number): string => {
    const totalSeconds = Math.floor(ms / 1000);
    const minutes = Math.floor(totalSeconds / 60);
    const seconds = totalSeconds % 60;
    const milliseconds = Math.floor((ms % 1000) / 10);

    return `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}.${String(milliseconds).padStart(2, '0')}`;
  };

  if (!isConnected) {
    return (
      <div className={styles['control-page']}>
        <div className={styles['loading']}>Connecting to server...</div>
      </div>
    );
  }

  return (
    <div className={styles['control-page']}>
      <h1>Game Master Control Panel</h1>

      {gameState.viewerCode && gameState.teams.length === 0 && (
        <div style={{
          background: '#fff3cd',
          border: '2px solid #ffc107',
          borderRadius: '8px',
          padding: '1.5rem',
          marginBottom: '2rem',
          textAlign: 'center'
        }}>
          <h3 style={{ margin: '0 0 0.5rem 0', color: '#856404' }}>Display Viewer Code</h3>
          <div style={{
            fontSize: '3rem',
            fontWeight: 'bold',
            letterSpacing: '0.5rem',
            color: '#856404',
            fontFamily: 'monospace'
          }}>
            {gameState.viewerCode}
          </div>
          <p style={{ margin: '0.5rem 0 0 0', color: '#856404', fontSize: '0.9rem' }}>
            Share this code with viewers to access the display screen
          </p>
          {gameState.isGameStarted && (<button
            onClick={handleCloseGame}
            style={{
              background: '#dc3545',
              color: 'white',
              border: 'none',
              padding: '0.75rem 2rem',
              borderRadius: '4px',
              fontSize: '1rem',
              fontWeight: 'bold',
              cursor: 'pointer'
            }}
            onMouseOver={e => e.currentTarget.style.background = '#c82333'}
            onMouseOut={e => e.currentTarget.style.background = '#dc3545'}
          >
            Close Game Session
          </button>)}
        </div>
      )}

      <div className={styles['control-layout']}>
        <div className={styles['left-panel']}>
          {!gameState.isGameStarted ? (
            <div className={styles['setup-section']}>
              <h2>Start New Game</h2>
              <button onClick={handleStartGame} className={styles['btn-start']}>
                Start Game
              </button>
            </div>
          ) : !gameState.teams.length ? (
            <div className={styles['setup-section']}>
              <h2>Setup Players {gameState.currentScene}</h2>

              <div className={styles['bulk-player-input']}>
                <label>Enter player names (one per line):</label>
                <textarea
                  value={playerInput}
                  onChange={e => setPlayerInput(e.target.value)}
                  onBlur={handleBulkPlayerInput}
                  placeholder="Alice&#10;Bob&#10;Charlie&#10;..."
                  rows={10}
                  className={styles['player-textarea']}
                />
              </div>

              {playerNames.length > 0 && (
                <>
                  <div className={styles['player-count']}>
                    {playerNames.length} player{playerNames.length !== 1 ? 's' : ''} entered
                  </div>
                  <div className={styles['player-list']}>
                    {playerNames.map((name, index) => (
                      <div key={index} className={styles['player-item']}>
                        <span>{name}</span>
                        <button onClick={() => handleRemovePlayer(index)} className={styles['btn-remove']}>×</button>
                      </div>
                    ))}
                  </div>
                  <button onClick={handleSetPlayers} className={styles['btn-primary']}>
                    Confirm Players ({playerNames.length})
                  </button>
                </>
              )}

              {gameState.players.length > 0 && (
                <div className={styles['players-confirmed']}>
                  <h3>Players Confirmed: {gameState.players.length}</h3>
                  <button onClick={handleCreateTeams} className={styles['btn-create-teams']}>
                    Create Teams (Random)
                  </button>
                </div>
              )}
            </div>
          ) : null}

          {gameState.teams.length > 0 && gameState.currentPhase === Phase.Setup && (
            <div className={styles['phase-selector']}>
              <h2>Select Phase</h2>
              <button onClick={handleStartPhase1} className={styles['btn-phase']}>
                Start Fast Buzzer Phase
              </button>
              <button onClick={handleStartPhase2} className={styles['btn-phase']} style={{ marginTop: '1rem' }}>
                Start List Phase
              </button>
              <button onClick={handleStartPhase3} className={styles['btn-phase']} style={{ marginTop: '1rem' }}>
                Start Sabotage Phase
              </button>
              <button onClick={handleStartPhase4} className={styles['btn-phase']} style={{ marginTop: '1rem' }}>
                Start Chrono Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.FastBuzzer && (
            <div className={styles['phase-panel']}>
              <h2>Phase 1: Fast Buzzer</h2>

              {gameState.currentQuestion && (
                <div className={styles['current-question-gm']}>
                  <h3>Current Question (GM View)</h3>
                  <div className={`${styles['difficulty-badge']} ${styles[`difficulty-${gameState.currentQuestion.difficulty}`]}`}>
                    Difficulty: {gameState.currentQuestion.difficulty}
                  </div>
                  <div className={styles['question-preview']}>
                    <div className={styles['q-lang']}>
                      <strong>NL:</strong> {gameState.currentQuestion.textNl}
                    </div>
                    <div className={styles['q-lang']}>
                      <strong>FR:</strong> {gameState.currentQuestion.textFr}
                    </div>
                    <div className={styles['answer-preview']}>
                      <div className={styles['q-lang']}>
                        <strong>Answer FR:</strong> {gameState.currentQuestion.answerFr}
                      </div>
                      <div className={styles['q-lang']}>
                        <strong>Answer NL:</strong> {gameState.currentQuestion.answerNl}
                      </div>
                    </div>
                  </div>
                </div>
              )}

              <div className={styles['phase-controls']}>
                <button onClick={handleGetQuestion} className={`${styles['btn-phase-action']} ${styles['btn-get-question']}`}>
                  Get Question (Preview)
                </button>
                <button
                  onClick={handleShowQuestion}
                  className={`${styles['btn-phase-action']} ${styles['btn-show-question']}`}
                  disabled={!gameState.currentQuestion || gameState.isCurrentQuestionVisibleOnDisplay}
                >
                  Show Question on Display
                </button>
                <button
                  onClick={handleShowAnswer}
                  className={`${styles['btn-phase-action']} ${styles['btn-show-answer']}`}
                  disabled={!gameState.currentQuestion || !gameState.isCurrentQuestionVisibleOnDisplay}
                >
                  Show Answer
                </button>
              </div>

              <div className={styles['blocking-section']}>
                <h3>Block Team(s) For Next Question</h3>
                <div className={styles['blocking-teams']}>
                  {gameState.teams.map((team, index) => (
                    <label key={index} className={styles['blocking-team-item']}>
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
                  className={styles['btn-apply-blocks']}
                  disabled={selectedBlockedTeams.length === 0}
                >
                  Apply Blocks ({selectedBlockedTeams.length})
                </button>

                {gameState.blockedNextQuestionTeamIds.length > 0 && (
                  <div className={styles['blocks-pending']}>
                    Blocked for next question: {gameState.blockedNextQuestionTeamIds.map(i => gameState.teams[i]?.name).join(', ')}
                  </div>
                )}
              </div>

              <button onClick={handleEndPhase} className={styles['btn-end-phase']}>
                End Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.List && (
            <div className={styles['phase-panel']}>
              <h2>Phase 2: List</h2>

              {gameState.currentListQuestion && (
                <div className={styles['current-question-gm']}>
                  <h3>Current List Question (GM View)</h3>
                  <div className={`${styles['difficulty-badge']} ${styles[`difficulty-${gameState.currentListQuestion.difficulty}`]}`}>
                    Difficulty: {gameState.currentListQuestion.difficulty}
                  </div>
                  <div className={styles['question-preview']}>
                    <div className={styles['q-lang']}>
                      <strong>NL:</strong> {gameState.currentListQuestion.textNl}
                    </div>
                    <div className={styles['q-lang']}>
                      <strong>FR:</strong> {gameState.currentListQuestion.textFr}
                    </div>
                    <div className={styles['answer-preview']}>
                      <h4>Accepted Answers ({gameState.currentListQuestion.answers.length})</h4>
                      {gameState.currentListQuestion.answers.map((answer, i) => (
                        <div key={i} className={styles['list-answer-item']}>
                          <div className={styles['q-lang']}>
                            <strong>FR:</strong> {answer.answerFr}
                          </div>
                          <div className={styles['q-lang']}>
                            <strong>NL:</strong> {answer.answerNl}
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>
                </div>
              )}

              {gameState.listTimer && (
                <div className={styles['timer-status-panel']}>
                  <h3>Timer Status</h3>
                  <div className={styles['timer-info']}>
                    <div className={styles['timer-state']}>
                      State: <strong>{['Idle', 'Running', 'Paused', 'Finished'][gameState.listTimer.state]}</strong>
                    </div>
                    <div className={styles['timer-duration']}>
                      Duration: {gameState.listTimer.durationSeconds} seconds
                    </div>
                  </div>
                </div>
              )}

              <div className={styles['phase-controls']}>
                <button onClick={handleLoadListQuestion} className={`${styles['btn-phase-action']} ${styles['btn-load-question']}`}>
                  Load Question
                </button>
                <button
                  onClick={handleShowListQuestion}
                  className={`${styles['btn-phase-action']} ${styles['btn-show-question']}`}
                >
                  Show Question
                </button>
              </div>

              <div className={styles['timer-duration-control']}>
                <label htmlFor="timer-duration">Timer Duration (seconds):</label>
                <input
                  id="timer-duration"
                  type="number"
                  min="1"
                  max="300"
                  value={listTimerDuration}
                  onChange={(e) => setListTimerDuration(Number(e.target.value))}
                  disabled={gameState.listTimer.state !== 0}
                  className={styles['timer-duration-input']}
                />
                <button
                  onClick={handleSetListTimerDuration}
                  className={styles['btn-set-duration']}
                  disabled={gameState.listTimer.state !== 0}
                >
                  Set Duration
                </button>
              </div>

              <div className={styles['phase-controls']}>
                <button
                  onClick={handleStartTimer}
                  className={`${styles['btn-phase-action']} ${styles['btn-start-timer']}`}
                  disabled={gameState.listTimer.state !== 0 || gameState.currentScene !== Scene.ListQuestion}
                >
                  Start Timer
                </button>
                <button
                  onClick={handlePauseTimer}
                  className={`${styles['btn-phase-action']} ${styles['btn-pause-timer']}`}
                  disabled={gameState.listTimer.state !== 1}
                >
                  Pause Timer
                </button>
                <button
                  onClick={handleResumeTimer}
                  className={`${styles['btn-phase-action']} ${styles['btn-resume-timer']}`}
                  disabled={gameState.listTimer.state !== 2}
                >
                  Resume Timer
                </button>
                <button
                  onClick={handleResetTimer}
                  className={`${styles['btn-phase-action']} ${styles['btn-reset-timer']}`}
                  disabled={gameState.listTimer.state === 0}
                >
                  Reset Timer
                </button>
              </div>

              <button onClick={handleEndPhase} className={styles['btn-end-phase']}>
                End Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.Sabotage && (
            <div className={styles['phase-panel']}>
              <h2>Phase 3: Sabotage</h2>

              {gameState.currentScene === Scene.Phase3Intro && (
                <div className={styles['phase-intro-controls']}>
                  <p>Phase 3 introduction is showing on display.</p>
                  <button
                    onClick={async () => await gameService.startThemeAssignment()}
                    className={styles['btn-start-assignment']}
                  >
                    Start Theme Assignment →
                  </button>
                </div>
              )}

              {gameState.sabotage.currentSubphase === 0 && gameState.currentScene !== Scene.Phase3Intro && (
                <>
                  <h3>Subphase 1: Theme Assignment</h3>

                  {gameState.sabotage.currentPickingTeamIndex !== null &&
                   gameState.sabotage.currentPickingTeamIndex !== undefined && (
                    <div className={styles['current-picker']}>
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

                  <div className={styles['theme-assignment-grid']}>
                    {gameState.sabotage.selectedThemes.map((theme) => {
                      const isAssigned = gameState.sabotage.teamThemeAssignments.some(
                        ta => (ta.selfSelectedTheme?.id === theme.id) || (ta.sabotageTheme?.id === theme.id)
                      );

                      return (
                        <div key={theme.id} className={`${styles['theme-card']} ${isAssigned ? styles['assigned'] : ''}`}>
                          <div className={styles['theme-name']}>
                            <span className={styles['theme-icon-inline']}>{theme.icon}</span> {theme.nameFr} / {theme.nameNl}
                          </div>
                          {!isAssigned && gameState.sabotage.currentPickingTeamIndex !== null && (
                            <div className={styles['theme-assign-buttons']}>
                              {gameState.teams.map((team, idx) => {
                                const teamAssignment = gameState.sabotage.teamThemeAssignments.find(
                                  ta => ta.teamIndex === idx
                                );

                                // Pick 1: can only assign to current picking team
                                if (gameState.sabotage.currentPickNumber === 1) {
                                  if (idx !== gameState.sabotage.currentPickingTeamIndex) return null;
                                  if (teamAssignment?.selfSelectedTheme) return null; // Already has self-selected
                                } else {
                                  // Pick 2: can assign to OTHER teams (or self if it's the only option)
                                  if (teamAssignment?.sabotageTheme) return null; // Already has sabotage

                                  // Check if there are any other teams without sabotage themes
                                  const teamsWithoutSabotage = gameState.teams.filter((_, tIdx) => {
                                    const ta = gameState.sabotage.teamThemeAssignments.find(a => a.teamIndex === tIdx);
                                    return !ta?.sabotageTheme;
                                  });

                                  // If current team is the only one without a sabotage theme, allow self-sabotage
                                  // Otherwise, exclude current team
                                  if (teamsWithoutSabotage.length > 1 && idx === gameState.sabotage.currentPickingTeamIndex) {
                                    return null;
                                  }
                                }

                                return (
                                  <button
                                    key={idx}
                                    onClick={() => handleAssignTheme(idx, theme.id)}
                                    className={styles['btn-assign-theme']}
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

                  <div className={styles['team-assignments-grid']}>
                    {gameState.teams.map((team, idx) => {
                      const assignment = gameState.sabotage.teamThemeAssignments.find(ta => ta.teamIndex === idx);
                      return (
                        <div key={idx} className={styles['team-assignment-card']}>
                          <h4>{team.name}</h4>
                          <div className={styles['assigned-themes-list']}>
                            {assignment?.selfSelectedTheme && (
                              <div className={`${styles['assigned-theme-badge']} ${styles['self-selected']}`}>
                                ✨ {assignment.selfSelectedTheme.nameFr} / {assignment.selfSelectedTheme.nameNl}
                              </div>
                            )}
                            {assignment?.sabotageTheme && (
                              <div className={`${styles['assigned-theme-badge']} ${styles['sabotage']}`}>
                                💣 {assignment.sabotageTheme.nameFr} / {assignment.sabotageTheme.nameNl}
                              </div>
                            )}
                            {!assignment?.selfSelectedTheme && !assignment?.sabotageTheme && (
                              <div className={styles['no-themes']}>No themes assigned yet</div>
                            )}
                          </div>
                        </div>
                      );
                    })}
                  </div>

                  <div className={styles['phase-controls']}>
                    <button
                      onClick={handleUndoThemeAssignment}
                      className={styles['btn-undo']}
                      disabled={gameState.sabotage.isThemeAssignmentComplete}
                    >
                      ↶ Undo Last Assignment
                    </button>
                    <button
                      onClick={handleStartMcqSubphase}
                      className={styles['btn-start-mcq']}
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
                    <div className={styles['mcq-current-info']}>
                      <div><strong>Current Team:</strong> {gameState.teams[gameState.sabotage.currentPlayingTeamIndex]?.name}</div>
                      <div>
                        <strong>Theme:</strong>{' '}
                        {gameState.sabotage.currentThemeIndex === 0 ? (
                          <span>✨ {gameState.sabotage.currentMcqQuestion?.theme.nameNl} / {gameState.sabotage.currentMcqQuestion?.theme.nameFr} (Self-Selected)</span>
                        ) : (
                          <span>💣 {gameState.sabotage.currentMcqQuestion?.theme.nameNl} / {gameState.sabotage.currentMcqQuestion?.theme.nameFr} (Sabotage)</span>
                        )}
                        {' - '}Question {gameState.sabotage.currentQuestionInTheme + 1}/4
                      </div>
                    </div>
                  )}

                  {!gameState.sabotage.currentMcqQuestion && (
                    <div className={styles['phase-controls']}>
                      <button
                        onClick={handleLoadNextMcqQuestion}
                        className={`${styles['btn-phase-action']} ${styles['btn-load-question']}`}
                      >
                        📥 Load Question (Preview)
                      </button>
                    </div>
                  )}

                  {gameState.sabotage.currentMcqQuestion && (
                    <>
                      <div className={styles['mcq-question-info']}>
                        <h4>Question Preview</h4>
                        <div className={styles['mcq-question-text']}>
                          <div>{gameState.sabotage.currentMcqQuestion.textNl}</div>
                          <div>{gameState.sabotage.currentMcqQuestion.textFr}</div>
                        </div>
                        <div className={styles['mcq-choices']}>
                          <div
                            className={`${styles['mcq-choice']} ${gameState.sabotage.selectedAnswer === 0 ? styles['mcq-choice-selected'] : ''} ${gameState.sabotage.isAnswerRevealed ? styles['disabled'] : ''}`}
                            onClick={() => !gameState.sabotage.isAnswerRevealed && handleSelectMcqAnswer(0)}
                          >
                            <strong>A:</strong> {gameState.sabotage.currentMcqQuestion.choiceAFr} / {gameState.sabotage.currentMcqQuestion.choiceANl}
                          </div>
                          <div
                            className={`${styles['mcq-choice']} ${gameState.sabotage.selectedAnswer === 1 ? styles['mcq-choice-selected'] : ''} ${gameState.sabotage.isAnswerRevealed ? styles['disabled'] : ''}`}
                            onClick={() => !gameState.sabotage.isAnswerRevealed && handleSelectMcqAnswer(1)}
                          >
                            <strong>B:</strong> {gameState.sabotage.currentMcqQuestion.choiceBFr} / {gameState.sabotage.currentMcqQuestion.choiceBNl}
                          </div>
                          <div
                            className={`${styles['mcq-choice']} ${gameState.sabotage.selectedAnswer === 2 ? styles['mcq-choice-selected'] : ''} ${gameState.sabotage.isAnswerRevealed ? styles['disabled'] : ''}`}
                            onClick={() => !gameState.sabotage.isAnswerRevealed && handleSelectMcqAnswer(2)}
                          >
                            <strong>C:</strong> {gameState.sabotage.currentMcqQuestion.choiceCFr} / {gameState.sabotage.currentMcqQuestion.choiceCNl}
                          </div>
                          <button
                            onClick={handleClearMcqAnswer}
                            className={styles['btn-clear-answer']}
                            disabled={gameState.sabotage.isAnswerRevealed || gameState.sabotage.selectedAnswer === undefined}
                          >
                            🔄 Clear Selection
                          </button>
                        </div>
                        <div className={styles['mcq-meta']}>
                          <strong>Difficulty:</strong> {'⭐'.repeat(gameState.sabotage.currentMcqQuestion.difficulty)} |
                          <strong> Correct Answer:</strong> <span style={{ color: '#10b981', fontWeight: 'bold' }}>
                            {['A', 'B', 'C'][gameState.sabotage.currentMcqQuestion.correctChoice]}
                          </span> | <button
                            onClick={handleClearMcqAnswer}
                            className={styles['btn-clear-answer']}
                            disabled={gameState.sabotage.isAnswerRevealed || gameState.sabotage.selectedAnswer === undefined}
                          >
                            🔄 Clear Selection
                          </button>
                        </div>
                      </div>

                      <div className={styles['mcq-control-buttons']}>
                        <button
                          onClick={handleShowMcqQuestion}
                          disabled={gameState.currentScene === Scene.SabotageMcqQuestion ||
                                    gameState.currentScene === Scene.SabotageMcqAnswer}
                          className={`${styles['btn-phase-action']} ${styles['btn-show-question']}`}
                        >
                          📺 Show Question on Display
                        </button>

                        <button
                          onClick={handleRevealMcqAnswer}
                          disabled={gameState.sabotage.selectedAnswer === undefined ||
                                    gameState.sabotage.isAnswerRevealed}
                          className={`${styles['btn-phase-action']} ${styles['btn-reveal']}`}
                        >
                          ✓ Reveal Answer
                        </button>

                        <button
                          onClick={handleAdvanceToNextMcqQuestion}
                          disabled={!gameState.sabotage.isAnswerRevealed}
                          className={`${styles['btn-phase-action']} ${styles['btn-next-question']}`}
                        >
                          ➡️ Next Question
                        </button>
                      </div>
                    </>
                  )}

                  {gameState.sabotage.currentPlayingTeamIndex === null && (
                    <div className={styles['mcq-complete-message']}>
                      <p>All teams have completed the MCQ subphase!</p>
                    </div>
                  )}
                </>
              )}

              <button onClick={handleEndPhase} className={styles['btn-end-phase']} style={{ marginTop: '2rem' }}>
                End Phase
              </button>
            </div>
          )}

          {gameState.currentPhase === Phase.Chrono && (
            <div className={styles['phase-panel']}>
              <h2>Phase 4: Chrono</h2>

              {/* Team Selection */}
              <div className={styles['chrono-team-selection']}>
                <h3>Select Team for Run</h3>
                <div className={styles['team-selector-grid']}>
                  {gameState.teams.map((team, idx) => {
                    const result = gameState.chrono.teamResults[idx];
                    return (
                      <button
                        key={idx}
                        onClick={() => handleSelectChronoTeam(idx)}
                        className={`${styles['team-select-btn']} ${
                          gameState.chrono.activeTeamIndex === idx ? styles['active'] : ''
                        }`}
                        disabled={gameState.chrono.runStatus === ChronoRunStatus.Running}
                      >
                        {team.name}
                        {result && result.status !== ChronoResultStatus.NotStarted && (
                          <div className={styles['team-result-badge']}>
                            {result.status === ChronoResultStatus.Finished && result.timeMs && (
                              <span className={styles['finished']}>✓ {formatTime(result.timeMs)}</span>
                            )}
                            {result.status === ChronoResultStatus.NotFinished && (
                              <span className={styles['not-finished']}>✗ Not Finished</span>
                            )}
                            {result.status === ChronoResultStatus.Aborted && (
                              <span className={styles['aborted']}>⊘ Aborted</span>
                            )}
                          </div>
                        )}
                      </button>
                    );
                  })}
                </div>
              </div>

              {/* Current Question Preview (GM only) */}
              {gameState.chrono.currentQuestion && (
                <div className={styles['current-question-gm']}>
                  <h3>Current Question (GM View)</h3>
                  <div className={styles['question-preview']}>
                    <div className={styles['q-lang']}>
                      <strong>NL:</strong> {gameState.chrono.currentQuestion.textNl}
                    </div>
                    <div className={styles['q-lang']}>
                      <strong>FR:</strong> {gameState.chrono.currentQuestion.textFr}
                    </div>
                    <div className={styles['answer-preview']}>
                      <div className={styles['q-lang']}>
                        <strong>Answer NL:</strong> {gameState.chrono.currentQuestion.answerNl}
                      </div>
                      <div className={styles['q-lang']}>
                        <strong>Answer FR:</strong> {gameState.chrono.currentQuestion.answerFr}
                      </div>
                    </div>
                  </div>
                </div>
              )}

              {/* Run Controls */}
              {gameState.chrono.activeTeamIndex !== null && gameState.chrono.activeTeamIndex !== undefined && (
                <div className={styles['chrono-controls']}>
                  <div className={styles['run-status']}>
                    <h3>
                      Run Status: {ChronoRunStatus[gameState.chrono.runStatus]} ({gameState.chrono.correctCount}/10)
                    </h3>
                    {gameState.chrono.bestTimeMs && (
                      <div className={styles['best-time-indicator']}>
                        Time to Beat: {formatTime(gameState.chrono.bestTimeMs)}
                      </div>
                    )}
                  </div>

                  <div className={styles['phase-controls']}>
                    <button
                      onClick={handleShowNextChronoQuestion}
                      className={`${styles['btn-phase-action']} ${styles['btn-show-question']}`}
                      disabled={
                        [ChronoRunStatus.Finished, ChronoRunStatus.NotFinished, ChronoRunStatus.Aborted].includes(
                          gameState.chrono.runStatus
                        ) || gameState.chrono.correctCount >= 10
                      }
                    >
                      Show Next Question
                    </button>

                    <button
                      onClick={handleMarkChronoCorrect}
                      className={`${styles['btn-phase-action']} ${styles['btn-mark-correct']}`}
                      disabled={
                        ![ChronoRunStatus.Running, ChronoRunStatus.Paused].includes(gameState.chrono.runStatus) ||
                        !gameState.chrono.currentQuestion ||
                        gameState.chrono.correctCount >= 10
                      }
                    >
                      Mark Correct ({gameState.chrono.correctCount + 1}/10)
                    </button>

                    <button
                      onClick={handlePauseChronoTimer}
                      className={`${styles['btn-phase-action']} ${styles['btn-pause']}`}
                      disabled={gameState.chrono.timerState !== 1} // TimerState.Running
                    >
                      Pause Timer
                    </button>

                    <button
                      onClick={handleResumeChronoTimer}
                      className={`${styles['btn-phase-action']} ${styles['btn-resume']}`}
                      disabled={gameState.chrono.timerState !== 2} // TimerState.Paused
                    >
                      Resume Timer
                    </button>
                  </div>

                  {/* Override Controls */}
                  <div className={styles['chrono-overrides']}>
                    <h4>Override Actions</h4>
                    <button
                      onClick={handleResetChronoRun}
                      className={styles['btn-reset']}
                      disabled={gameState.chrono.runStatus === ChronoRunStatus.Idle}
                    >
                      Reset Run
                    </button>
                    <button
                      onClick={handleAbortChronoRun}
                      className={styles['btn-abort']}
                      disabled={gameState.chrono.runStatus === ChronoRunStatus.Idle}
                    >
                      Abort Run
                    </button>
                    <button
                      onClick={handleForceFinishChronoRun}
                      className={styles['btn-force-finish']}
                      disabled={
                        ![ChronoRunStatus.Running, ChronoRunStatus.Paused].includes(gameState.chrono.runStatus)
                      }
                    >
                      Force Finish
                    </button>
                  </div>
                </div>
              )}

              {/* Team Results Summary */}
              <div className={styles['chrono-results-summary']}>
                <h3>Team Results</h3>
                <table className={styles['results-table']}>
                  <thead>
                    <tr>
                      <th>Team</th>
                      <th>Status</th>
                      <th>Time</th>
                    </tr>
                  </thead>
                  <tbody>
                    {gameState.teams.map((team, idx) => {
                      const result = gameState.chrono.teamResults[idx];
                      return (
                        <tr key={idx}>
                          <td>{team.name}</td>
                          <td>{result ? ChronoResultStatus[result.status] : 'NotStarted'}</td>
                          <td>{result?.timeMs ? formatTime(result.timeMs) : '-'}</td>
                        </tr>
                      );
                    })}
                  </tbody>
                </table>
              </div>

              <button onClick={handleEndPhase} className={styles['btn-end-phase']} style={{ marginTop: '2rem' }}>
                End Phase
              </button>
            </div>
          )}

          {gameState.teams.length > 0 && (
            <div className={styles['scene-controls']}>
              <h2>Display Controls</h2>
              <div className={styles['scene-buttons']}>
                <button onClick={handleShowTeams} className={styles['btn-scene']}>
                  Display Teams
                </button>
                <button onClick={handleShowScoreboard} className={styles['btn-scene']}>
                  Show Scoreboard
                </button>
                {gameState.currentScene === Scene.Scoreboard && (
                  <button onClick={handleBackToGame} className={styles['btn-scene']}>
                    Back to Game
                  </button>
                )}
              </div>
              <div className={styles['current-scene']}>
                Current Scene: <strong>{Scene[gameState.currentScene]}</strong>
              </div>
            </div>
          )}
        </div>

        <div className={styles['right-panel']}>
          {gameState.teams.length > 0 && (
            <div className={styles['teams-scores']}>
              <h2>Teams & Scores</h2>

              {gameState.teams.map((team, teamIndex) => {
                const teamColor = TEAM_COLORS[teamIndex % TEAM_COLORS.length];
                return (
                <div
                  key={teamIndex}
                  className={styles['score-card']}
                  style={{ backgroundColor: teamColor.solid }}
                >
                  <div className={styles['score-header']}>
                    <h3>{team.name}</h3>
                    <div className={styles['score-value']}>{team.score}</div>
                  </div>

                  <div className={styles['score-controls']}>
                    <button onClick={() => handleAdjustScore(teamIndex, 1)} className={styles['btn-plus']}>
                      +1
                    </button>
                    <button onClick={() => handleAdjustScore(teamIndex, -1)} className={styles['btn-minus']}>
                      -1
                    </button>
                  </div>

                  <div className={styles['team-members']}>
                    {team.players.map((player, i) => (
                      <span key={i} className={styles['player-badge']}>
                        {player}
                      </span>
                    ))}
                  </div>
                </div>
                );
              })}
            </div>
          )}
        </div>
      </div>

      {/* Teams Management - Moved to Bottom */}
      {gameState.teams.length > 0 && (
        <div className={styles['teams-management-bottom']}>
          <h2>Teams Management</h2>

          <div className={styles['teams-grid']}>
            {gameState.teams.map((team, teamIndex) => (
              <div key={teamIndex} className={styles['team-card']}>
                <input
                  type="text"
                  value={team.name}
                  onChange={e => handleRenameTeam(teamIndex, e.target.value)}
                  className={styles['team-name-input']}
                />

                <div className={styles['team-players']}>
                  {team.players.map((player, playerIndex) => (
                    <div key={playerIndex} className={styles['team-player']}>
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
        </div>
      )}

      {/* Viewer Code - Shown at bottom once teams are created */}
      {gameState.viewerCode && gameState.teams.length > 0 && (
        <div style={{
          background: '#fff3cd',
          border: '2px solid #ffc107',
          borderRadius: '8px',
          padding: '1.5rem',
          marginTop: '2rem',
          textAlign: 'center'
        }}>
          <h3 style={{ margin: '0 0 0.5rem 0', color: '#856404' }}>Display Viewer Code</h3>
          <div style={{
            fontSize: '3rem',
            fontWeight: 'bold',
            letterSpacing: '0.5rem',
            color: '#856404',
            fontFamily: 'monospace'
          }}>
            {gameState.viewerCode}
          </div>
          <p style={{ margin: '0.5rem 0 0 0', color: '#856404', fontSize: '0.9rem' }}>
            Share this code with viewers to access the display screen
          </p>
          {gameState.isGameStarted && (<button
            onClick={handleCloseGame}
            style={{
              background: '#dc3545',
              color: 'white',
              border: 'none',
              padding: '0.75rem 2rem',
              borderRadius: '4px',
              fontSize: '1rem',
              fontWeight: 'bold',
              cursor: 'pointer'
            }}
            onMouseOver={e => e.currentTarget.style.background = '#c82333'}
            onMouseOut={e => e.currentTarget.style.background = '#dc3545'}
          >
            Close Game Session
          </button>)}
        </div>
      )}
    </div>
  );
}
