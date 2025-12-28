import { useGameState } from '../hooks/useGameState';
import { Scene } from '../types';
import TeamsScene from '../components/TeamsScene';
import ScoreboardScene from '../components/ScoreboardScene';
import QuestionScene from '../components/QuestionScene';
import AnswerRevealScene from '../components/AnswerRevealScene';
import TeamCreationLoadingScene from '../components/TeamCreationLoadingScene';
import { ListQuestionScene } from '../components/scenes/ListQuestionScene';
import { BoardsUpOverlay } from '../components/scenes/BoardsUpOverlay';
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

  // Check if BoardsUp overlay should be shown
  const showBoardsUp = gameState.listTimer.boardsUpVisibleUntilUtc &&
                       new Date(gameState.listTimer.boardsUpVisibleUntilUtc).getTime() > Date.now();

  return (
    <div className="display-page">
      {gameState.currentScene === Scene.Teams && <TeamsScene teams={gameState.teams} />}
      {gameState.currentScene === Scene.Scoreboard && <ScoreboardScene teams={gameState.teams} />}
      {gameState.currentScene === Scene.TeamCreationLoading && <TeamCreationLoadingScene />}
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
      {gameState.currentScene === Scene.ListQuestion && gameState.currentListQuestion && gameState.isCurrentListQuestionVisibleOnDisplay && (
        <ListQuestionScene
          question={gameState.currentListQuestion}
          timer={gameState.listTimer}
        />
      )}
      {showBoardsUp && <BoardsUpOverlay />}
    </div>
  );
}
