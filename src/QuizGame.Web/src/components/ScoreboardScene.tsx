import { motion, useSpring, useTransform } from 'framer-motion';
import { useEffect, useState } from 'react';
import { Team } from '../types';
import styles from './ScoreboardScene.module.css';

interface Props {
  teams: Team[];
}

// Team colors - vibrant TV game show palette
const TEAM_COLORS = [
  { gradient: 'linear-gradient(135deg, #ffd700 0%, #ffed4e 100%)', glow: 'rgba(255, 215, 0, 0.5)', name: 'yellow' },
  { gradient: 'linear-gradient(135deg, #ff0000 0%, #ff4444 100%)', glow: 'rgba(255, 0, 0, 0.5)', name: 'red' },
  { gradient: 'linear-gradient(135deg, #00c853 0%, #00e676 100%)', glow: 'rgba(0, 200, 83, 0.5)', name: 'green' },
  { gradient: 'linear-gradient(135deg, #2979ff 0%, #448aff 100%)', glow: 'rgba(41, 121, 255, 0.5)', name: 'blue' },
  { gradient: 'linear-gradient(135deg, #fa709a 0%, #fee140 100%)', glow: 'rgba(250, 112, 154, 0.5)', name: 'sunset' },
  { gradient: 'linear-gradient(135deg, #30cfd0 0%, #330867 100%)', glow: 'rgba(48, 207, 208, 0.5)', name: 'teal' },
];

function getTeamColor(teamName: string, teams: Team[]) {
  const teamIndex = teams.findIndex(t => t.name === teamName);
  return TEAM_COLORS[teamIndex % TEAM_COLORS.length];
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
  const originalTeams = teams; // Keep original order for color consistency

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
        <div>Scorebord</div>
        <div>Tableau des scores</div>
      </motion.h1>

      <motion.div
        variants={container}
        initial="hidden"
        animate="show"
        className={styles['scoreboard-list']}
      >
        {sortedTeams.map((team, index) => {
          const teamColor = getTeamColor(team.name, originalTeams);
          return (
            <motion.div
              key={team.name}
              layout
              layoutId={team.name}
              variants={item}
              className={`${styles['scoreboard-item']} ${index === 0 ? styles['rank-1'] : ''}`}
              style={{
                ['--team-gradient' as any]: teamColor.gradient,
                ['--team-glow' as any]: teamColor.glow,
              }}
              transition={{
                layout: {
                  type: 'spring',
                  stiffness: 100,
                  damping: 20,
                  duration: 0.8,
                }
              }}
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
          );
        })}
      </motion.div>
    </div>
  );
}
