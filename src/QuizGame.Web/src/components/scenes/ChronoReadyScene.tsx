import { motion } from 'framer-motion';
import { GameState } from '../../types';
import styles from './ChronoReadyScene.module.css';

interface Props {
  gameState: GameState;
}

export function ChronoReadyScene({ gameState }: Props) {
  const { chrono, teams } = gameState;

  if (chrono.activeTeamIndex === null || chrono.activeTeamIndex === undefined) {
    return <div>Loading...</div>;
  }

  const activeTeam = teams[chrono.activeTeamIndex];
  const isChasingBestTime = chrono.bestTimeMs !== null && chrono.bestTimeMs !== undefined;

  return (
    <div className={styles['ready-scene']}>
      <motion.div
        className={styles['ready-container']}
        initial={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{
          duration: 0.8,
          type: 'spring',
          stiffness: 100
        }}
      >
        <motion.div
          className={styles['team-name']}
          initial={{ opacity: 0, y: -50 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.3, duration: 0.6 }}
        >
          {activeTeam.name}
        </motion.div>

        <motion.div
          className={styles['ready-message']}
          initial={{ opacity: 0, scale: 0.5 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{
            delay: 0.6,
            duration: 0.8,
            type: 'spring',
            stiffness: 150
          }}
        >
          <div className={styles['ready-text-fr']}>
            Êtes-vous prêts ?
          </div>
          <div className={styles['ready-text-nl']}>
            Zijn jullie klaar?
          </div>
        </motion.div>

        {isChasingBestTime && (
          <motion.div
            className={styles['challenge-info']}
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.9, duration: 0.6 }}
          >
            <div className={styles['challenge-label-fr']}>
              Temps à battre
            </div>
            <div className={styles['best-time']}>
              {formatTime(chrono.bestTimeMs!)}
            </div>
            <div className={styles['challenge-label-nl']}>
              Tijd om te verslaan
            </div>
          </motion.div>
        )}

        <motion.div
          className={styles['icon-container']}
          initial={{ rotate: -180, scale: 0 }}
          animate={{ rotate: 0, scale: 1 }}
          transition={{
            delay: 1.2,
            duration: 0.8,
            type: 'spring',
            stiffness: 120
          }}
        >
          {isChasingBestTime ? '⏱️' : '🏁'}
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
