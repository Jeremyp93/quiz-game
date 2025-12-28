import { useGameState } from '../hooks/useGameState';
import { Scene } from '../types';
import TeamsScene from '../components/TeamsScene';
import ScoreboardScene from '../components/ScoreboardScene';
import QuestionScene from '../components/QuestionScene';
import AnswerRevealScene from '../components/AnswerRevealScene';
import './DisplayPage.css';

export default function DisplayPage() {
  const { gameState, isConnected } = useGameState();

  if (!isConnected) {
    return (
      <div className="display-page">
        <div className="loading-display">Connecting...</div>
      </div>
    );
  }

  if (!gameState.isGameStarted) {
    return (
      <div className="display-page">
        <div className="waiting-display">
          <h1>Waiting for Game to Start...</h1>
        </div>
      </div>
    );
  }

  return (
    <div className="display-page">
      {gameState.currentScene === Scene.Teams && <TeamsScene teams={gameState.teams} />}
      {gameState.currentScene === Scene.Scoreboard && <ScoreboardScene teams={gameState.teams} />}
      {gameState.currentScene === Scene.Question && gameState.currentQuestion && gameState.isCurrentQuestionVisibleOnDisplay && (
        <QuestionScene
          question={gameState.currentQuestion}
          teams={gameState.teams}
          blockedTeamIndices={gameState.blockedTeamIdsForCurrentQuestion}
        />
      )}
      {gameState.currentScene === Scene.Answer && gameState.currentQuestion && gameState.isCurrentQuestionVisibleOnDisplay && (
        <AnswerRevealScene
          question={gameState.currentQuestion}
          teams={gameState.teams}
          blockedTeamIndices={gameState.blockedTeamIdsForCurrentQuestion}
        />
      )}
    </div>
  );
}
