import { motion } from 'framer-motion';
import { Team } from '../types';
import styles from './TeamsScene.module.css';

interface Props {
  teams: Team[];
}

export default function TeamsScene({ teams }: Props) {
  const container = {
    hidden: { opacity: 0 },
    show: {
      opacity: 1,
      transition: {
        staggerChildren: 0.2,
      },
    },
  };

  const item = {
    hidden: { opacity: 0, y: 50, scale: 0.8 },
    show: {
      opacity: 1,
      y: 0,
      scale: 1,
      transition: {
        type: 'spring',
        damping: 12,
        stiffness: 100,
      },
    },
  };

  const playerItem = {
    hidden: { opacity: 0, x: -20 },
    show: {
      opacity: 1,
      x: 0,
      transition: {
        type: 'spring',
        damping: 15,
        stiffness: 120,
      },
    },
  };

  return (
    <div className={styles['teams-scene']}>
      <motion.h1
        initial={{ opacity: 0, y: -50 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.8, type: 'spring' }}
        className={styles['scene-title']}
      >
        <div>Teams</div>
        <div>Équipes</div>
      </motion.h1>

      <motion.div
        variants={container}
        initial="hidden"
        animate="show"
        className={styles['teams-grid']}
      >
        {teams.map((team, index) => (
          <motion.div key={index} variants={item} className={styles['team-display-card']}>
            <div className={styles['team-display-header']}>
              <h2>{team.name}</h2>
            </div>

            <motion.div
              className={styles['team-display-players']}
              variants={container}
              initial="hidden"
              animate="show"
            >
              {team.players.map((player, playerIndex) => (
                <motion.div key={playerIndex} variants={playerItem} className={styles['player-display-item']}>
                  {player}
                </motion.div>
              ))}
            </motion.div>
          </motion.div>
        ))}
      </motion.div>
    </div>
  );
}
