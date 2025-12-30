import { motion } from 'framer-motion';
import { GameState } from '../../types';
import styles from './ChronoFailureScene.module.css';

interface Props {
  gameState: GameState;
}

export function ChronoFailureScene({ gameState }: Props) {
  const { chrono, teams } = gameState;

  if (chrono.activeTeamIndex === null || chrono.activeTeamIndex === undefined) {
    return <div>Loading...</div>;
  }

  const activeTeam = teams[chrono.activeTeamIndex];

  return (
    <div className={styles['failure-scene']}>
      <motion.div
        className={styles['failure-container']}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{
          duration: 0.8,
          type: 'spring',
          stiffness: 100
        }}
      >
        <motion.div
          className={styles['timeout-icon']}
          initial={{ scale: 0 }}
          animate={{ scale: 1 }}
          transition={{
            delay: 0.3,
            duration: 0.6,
            type: 'spring',
            stiffness: 150
          }}
        >
          ⏱️
        </motion.div>

        <motion.div
          className={styles['failure-title']}
          initial={{ opacity: 0, y: 50 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.5, duration: 0.6 }}
        >
          <div>Tijd is om!</div>
          <div>Le temps est écoulé !</div>
        </motion.div>

        <motion.div
          className={styles['team-name']}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.7, duration: 0.5 }}
        >
          {activeTeam.name}
        </motion.div>

        <motion.div
          className={styles['score-reached']}
          initial={{ opacity: 0, scale: 0.8 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ delay: 0.9, duration: 0.6 }}
        >
          <div className={styles['score-value']}>{chrono.correctCount}/10</div>
          <div className={styles['score-label']}>
            <div>Juiste Antwoorden</div>
            <div>Bonnes Réponses</div>
          </div>
        </motion.div>

        <motion.div
          className={styles['encouragement']}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 1.2, duration: 0.5 }}
        >
          <div>Volgende keer beter!</div>
          <div>Meilleure chance la prochaine fois !</div>
        </motion.div>
      </motion.div>
    </div>
  );
}
