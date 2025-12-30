import { motion, useSpring, useTransform } from 'framer-motion';
import { useEffect, useState } from 'react';
import { Team } from '../types';
import styles from './ScoreboardScene.module.css';

interface Props {
  teams: Team[];
}

function AnimatedScore({ value }: { value: number }) {
  const [prevValue, setPrevValue] = useState(value);
  const spring = useSpring(prevValue, { damping: 20, stiffness: 100 });
  const display = useTransform(spring, (current) => Math.round(current));

  useEffect(() => {
    setPrevValue(value);
    spring.set(value);
  }, [value, spring]);

  return <motion.span>{display}</motion.span>;
}

export default function ScoreboardScene({ teams }: Props) {
  const sortedTeams = [...teams].sort((a, b) => b.score - a.score);

  const container = {
    hidden: { opacity: 0 },
    show: {
      opacity: 1,
      transition: {
        staggerChildren: 0.15,
      },
    },
  };

  const item = {
    hidden: { opacity: 0, x: -100 },
    show: {
      opacity: 1,
      x: 0,
      transition: {
        type: 'spring',
        damping: 15,
        stiffness: 100,
      },
    },
  };

  return (
    <div className={styles['scoreboard-scene']}>
      <motion.h1
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.6, type: 'spring' }}
        className={styles['scoreboard-title']}
      >
        Scoreboard
      </motion.h1>

      <motion.div
        variants={container}
        initial="hidden"
        animate="show"
        className={styles['scoreboard-list']}
      >
        {sortedTeams.map((team, index) => (
          <motion.div
            key={team.name}
            variants={item}
            className={`${styles['scoreboard-item']} ${styles[`rank-${index + 1}`]}`}
          >
            <div className={styles['rank-badge']}>{index + 1}</div>

            <div className={styles['team-info']}>
              <h2>{team.name}</h2>
              <div className={styles['team-players-list']}>
                {team.players.join(', ')}
              </div>
            </div>

            <motion.div
              className={styles['score-display']}
              initial={{ scale: 1 }}
              animate={{ scale: [1, 1.1, 1] }}
              transition={{ duration: 0.3 }}
              key={team.score}
            >
              <AnimatedScore value={team.score} />
            </motion.div>
          </motion.div>
        ))}
      </motion.div>
    </div>
  );
}
