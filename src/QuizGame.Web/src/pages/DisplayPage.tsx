import { useState, useEffect } from 'react';
import { useGameState } from '../hooks/useGameState';
import { authService } from '../services/authService';
import ViewerCodeModal from '../components/ViewerCodeModal';
import { Scene } from '../types';
import TeamsScene from '../components/TeamsScene';
import ScoreboardScene from '../components/ScoreboardScene';
import QuestionScene from '../components/QuestionScene';
import AnswerRevealScene from '../components/AnswerRevealScene';
import TeamCreationLoadingScene from '../components/TeamCreationLoadingScene';
import { ListQuestionScene } from '../components/scenes/ListQuestionScene';
import { BoardsUpOverlay } from '../components/scenes/BoardsUpOverlay';
import SabotageThemeAssignmentScene from '../components/SabotageThemeAssignmentScene';
import SabotageMcqQuestionScene from '../components/SabotageMcqQuestionScene';
import SabotageMcqAnswerScene from '../components/SabotageMcqAnswerScene';
import QuestionTransitionScene from '../components/QuestionTransitionScene';
import { Phase1IntroScene } from '../components/scenes/Phase1IntroScene';
import { Phase2IntroScene } from '../components/scenes/Phase2IntroScene';
import { Phase3IntroScene } from '../components/scenes/Phase3IntroScene';
import { Phase4IntroScene } from '../components/scenes/Phase4IntroScene';
import { ChronoReadyScene } from '../components/scenes/ChronoReadyScene';
import { ChronoQuestionScene } from '../components/scenes/ChronoQuestionScene';
import { ChronoCompletionScene } from '../components/scenes/ChronoCompletionScene';
import { ChronoFailureScene } from '../components/scenes/ChronoFailureScene';
import { WelcomeScene } from '../components/scenes/WelcomeScene';
import styles from './DisplayPage.module.css';

export default function DisplayPage() {
  const { gameState, isConnected } = useGameState();
  const [showCodeModal, setShowCodeModal] = useState(false);
  const [isVerified, setIsVerified] = useState(false);

  useEffect(() => {
    checkVerification();
  }, []);

  // Watch for game closure (viewer code becomes null)
  useEffect(() => {
    if (!gameState.viewerCode && isVerified) {
      // Game was closed, invalidate viewer session
      authService.clearViewerVerification();
      setIsVerified(false);
      setShowCodeModal(true);
    }
  }, [gameState.viewerCode, isVerified]);

  const checkVerification = async () => {
    // Check if locally verified (localStorage flag)
    if (authService.isViewerVerifiedLocally()) {
      // Verify with server that session is still valid
      const user = await authService.getCurrentUser();

      if (user.authenticated && user.role === 'Viewer') {
        setIsVerified(true);
      } else if (user.reason === 'session_invalidated') {
        // Session was invalidated, need new code
        authService.clearViewerVerification();
        setShowCodeModal(true);
      } else {
        setShowCodeModal(true);
      }
    } else {
      setShowCodeModal(true);
    }
  };

  const handleVerified = () => {
    setIsVerified(true);
    setShowCodeModal(false);
  };

  if (showCodeModal) {
    return <ViewerCodeModal onVerified={handleVerified} />;
  }

  if (!isVerified) {
    return (
      <div className={styles['display-page']}>
        <div className={styles['loading-display']}>Checking access...</div>
      </div>
    );
  }

  if (!isConnected) {
    return (
      <div className={styles['display-page']}>
        <div className={styles['loading-display']}>Connecting...</div>
      </div>
    );
  }

  if (!gameState.isGameStarted || gameState.currentScene === Scene.Welcome) {
    return <WelcomeScene />;
  }

  // Check if BoardsUp overlay should be shown
  const showBoardsUp = gameState.listTimer.boardsUpVisibleUntilUtc &&
                       new Date(gameState.listTimer.boardsUpVisibleUntilUtc).getTime() > Date.now();

  return (
    <div className={styles['display-page']}>
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
      {gameState.currentScene === Scene.SabotageThemeAssignment && (
        <SabotageThemeAssignmentScene gameState={gameState} />
      )}
      {gameState.currentScene === Scene.SabotageMcqQuestion && (
        <SabotageMcqQuestionScene gameState={gameState} />
      )}
      {gameState.currentScene === Scene.SabotageMcqAnswer && (
        <SabotageMcqAnswerScene gameState={gameState} />
      )}
      {gameState.currentScene === Scene.QuestionTransition && (
        <QuestionTransitionScene currentPhase={gameState.currentPhase} />
      )}
      {gameState.currentScene === Scene.Phase1Intro && <Phase1IntroScene />}
      {gameState.currentScene === Scene.Phase2Intro && <Phase2IntroScene />}
      {gameState.currentScene === Scene.Phase3Intro && <Phase3IntroScene />}
      {gameState.currentScene === Scene.Phase4Intro && <Phase4IntroScene />}
      {gameState.currentScene === Scene.ChronoReady && (
        <ChronoReadyScene gameState={gameState} />
      )}
      {gameState.currentScene === Scene.ChronoQuestion && (
        <ChronoQuestionScene gameState={gameState} />
      )}
      {gameState.currentScene === Scene.ChronoCompletion && (
        <ChronoCompletionScene gameState={gameState} />
      )}
      {gameState.currentScene === Scene.ChronoFailure && (
        <ChronoFailureScene gameState={gameState} />
      )}
      {showBoardsUp && <BoardsUpOverlay />}
    </div>
  );
}
