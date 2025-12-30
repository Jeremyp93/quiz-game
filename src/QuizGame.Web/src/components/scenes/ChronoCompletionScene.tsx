import { motion } from 'framer-motion';
import { GameState } from '../../types';
import styles from './ChronoCompletionScene.module.css';

interface Props {
  gameState: GameState;
}

export function ChronoCompletionScene({ gameState }: Props) {
  const { chrono, teams } = gameState;

  if (chrono.activeTeamIndex === null || chrono.activeTeamIndex === undefined) {
    return <div>Loading...</div>;
  }

  const activeTeam = teams[chrono.activeTeamIndex];
  const result = chrono.teamResults[chrono.activeTeamIndex];

  if (!result || !result.timeMs) {
    return <div>Error: No result data</div>;
  }

  const isBestTime = result.timeMs === chrono.bestTimeMs;

  return (
    <div className={styles['completion-scene']}>
      <motion.div
        className={styles['success-container']}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{
          duration: 0.8,
          type: 'spring',
          stiffness: 100
        }}
      >
        <motion.div
          className={styles['trophy-icon']}
          initial={{ rotate: -180, scale: 0 }}
          animate={{ rotate: 0, scale: 1 }}
          transition={{
            delay: 0.3,
            duration: 0.8,
            type: 'spring',
            stiffness: 150
          }}
        >
          🏆
        </motion.div>

        <motion.div
          className={styles['completion-title']}
          initial={{ opacity: 0, y: 50 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.5, duration: 0.6 }}
        >
          <div className={styles['title-nl']}>
            {activeTeam.name} - Voltooid!
          </div>
          <div className={styles['title-fr']}>
            {activeTeam.name} - Terminé !
          </div>
        </motion.div>

        <motion.div
          className={styles['time-display']}
          initial={{ opacity: 0, scale: 0.8 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ delay: 0.7, duration: 0.6 }}
        >
          <div className={styles['final-time']}>
            {formatTime(result.timeMs)}
          </div>
          {isBestTime && (
            <motion.div
              className={styles['best-time-badge']}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 1.2, duration: 0.5 }}
            >
              <div>⭐ BESTE TIJD ⭐</div>
              <div>⭐ MEILLEUR TEMPS ⭐</div>
            </motion.div>
          )}
        </motion.div>

        <motion.div
          className={styles['milestone-complete']}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 1.0, duration: 0.5 }}
        >
          <div>10/10 Juiste Antwoorden</div>
          <div>10/10 Bonnes Réponses</div>
        </motion.div>
      </motion.div>
    </div>
  );
}

function formatTime(ms: number): string {
  const totalSeconds = Math.floor(ms / 1000);
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  const milliseconds = Math.floor((ms % 1000) / 10);

  return `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}.${String(milliseconds).padStart(2, '0')}`;
}
