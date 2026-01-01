import { useEffect, useState } from 'react';
import { gameService } from '../services/gameService';
import { GameState, TimerState, SabotageSubphase, ChronoRunStatus } from '../types';

const initialState: GameState = {
  isGameStarted: false,
  players: [],
  teams: [],
  currentPhase: 0,
  currentScene: 0,
  lastSceneBeforeScoreboard: undefined,
  sessionVersion: 0,
  currentQuestion: undefined,
  isCurrentQuestionVisibleOnDisplay: false,
  lastQuestionId: undefined,
  blockedNextQuestionTeamIds: [],
  blockedTeamIdsForCurrentQuestion: [],
  currentListQuestion: undefined,
  isCurrentListQuestionVisibleOnDisplay: false,
  lastListQuestionId: undefined,
  listTimer: {
    durationSeconds: 45,
    state: TimerState.Idle,
    accumulatedPausedMs: 0,
  },
  sabotage: {
    currentSubphase: SabotageSubphase.ThemeAssignment,
    selectedThemes: [],
    teamThemeAssignments: [],
    currentPickNumber: 1,
    isThemeAssignmentComplete: false,
    isAnswerRevealed: false,
    currentQuestionInTheme: 0,
  },
  chrono: {
    runStatus: ChronoRunStatus.Idle,
    correctCount: 0,
    timerState: TimerState.Idle,
    timerAccumulatedPausedMs: 0,
    teamResults: {},
  },
};

export function useGameState() {
  const [gameState, setGameState] = useState<GameState>(initialState);
  const [isConnected, setIsConnected] = useState(false);

  useEffect(() => {
    gameService.connect().then(() => setIsConnected(true));
    const unsubscribe = gameService.onStateUpdated(setGameState);

    return () => {
      unsubscribe();
      gameService.disconnect();
    };
  }, []);

  return { gameState, isConnected };
}
