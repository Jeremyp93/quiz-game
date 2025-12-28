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
