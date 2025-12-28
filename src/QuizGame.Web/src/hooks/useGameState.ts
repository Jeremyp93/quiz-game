import { useEffect, useState } from 'react';
import { gameService } from '../services/gameService';
import { GameState } from '../types';

const initialState: GameState = {
  isGameStarted: false,
  players: [],
  teams: [],
  currentPhase: 0,
  currentScene: 0,
  lastSceneBeforeScoreboard: undefined,
  currentQuestion: undefined,
  lastQuestionId: undefined,
  blockedNextQuestionTeamIds: [],
  blockedTeamIdsForCurrentQuestion: [],
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
