import { useState } from 'react';
import { useGameState } from '../hooks/useGameState';
import { gameService } from '../services/gameService';
import { Scene } from '../types';
import './ControlPage.css';

export default function ControlPage() {
  const { gameState, isConnected } = useGameState();
  const [playerInput, setPlayerInput] = useState('');
  const [playerNames, setPlayerNames] = useState<string[]>([]);

  const handleStartGame = async () => {
    await gameService.startGame();
  };

  const handleAddPlayer = () => {
    if (playerInput.trim()) {
      setPlayerNames([...playerNames, playerInput.trim()]);
      setPlayerInput('');
    }
  };

  const handleRemovePlayer = (index: number) => {
    setPlayerNames(playerNames.filter((_, i) => i !== index));
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

              <div className="player-input">
                <input
                  type="text"
                  value={playerInput}
                  onChange={e => setPlayerInput(e.target.value)}
                  onKeyPress={e => e.key === 'Enter' && handleAddPlayer()}
                  placeholder="Enter player name"
                />
                <button onClick={handleAddPlayer} className="btn-add">Add</button>
              </div>

              <div className="player-list">
                {playerNames.map((name, index) => (
                  <div key={index} className="player-item">
                    <span>{name}</span>
                    <button onClick={() => handleRemovePlayer(index)} className="btn-remove">×</button>
                  </div>
                ))}
              </div>

              {playerNames.length > 0 && (
                <button onClick={handleSetPlayers} className="btn-primary">
                  Confirm Players ({playerNames.length})
                </button>
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
