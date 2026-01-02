import { motion } from 'framer-motion';
import { Team } from '../types';
import styles from './TeamsScene.module.css';

interface Props {
  teams: Team[];
}

// Team colors - vibrant TV game show palette (same as ScoreboardScene)
const TEAM_COLORS = [
  { gradient: 'linear-gradient(135deg, #ffd700 0%, #ffed4e 100%)', glow: 'rgba(255, 215, 0, 0.5)', name: 'yellow' },
  { gradient: 'linear-gradient(135deg, #ff0000 0%, #ff4444 100%)', glow: 'rgba(255, 0, 0, 0.5)', name: 'red' },
  { gradient: 'linear-gradient(135deg, #00c853 0%, #00e676 100%)', glow: 'rgba(0, 200, 83, 0.5)', name: 'green' },
  { gradient: 'linear-gradient(135deg, #2979ff 0%, #448aff 100%)', glow: 'rgba(41, 121, 255, 0.5)', name: 'blue' },
  { gradient: 'linear-gradient(135deg, #fa709a 0%, #fee140 100%)', glow: 'rgba(250, 112, 154, 0.5)', name: 'sunset' },
  { gradient: 'linear-gradient(135deg, #30cfd0 0%, #330867 100%)', glow: 'rgba(48, 207, 208, 0.5)', name: 'teal' },
];

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
        {teams.map((team, index) => {
          const teamColor = TEAM_COLORS[index % TEAM_COLORS.length];
          return (
            <motion.div
              key={index}
              variants={item}
              className={styles['team-display-card']}
              style={{
                ['--team-gradient' as any]: teamColor.gradient,
                ['--team-glow' as any]: teamColor.glow,
              }}
            >
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
          );
        })}
      </motion.div>
    </div>
  );
}
