import { motion } from 'framer-motion';
import styles from './WelcomeScene.module.css';

export function WelcomeScene() {
  const phases = [
    { number: 1, icon: '⚡', nameFr: 'Buzzer', nameNl: 'Buzzer', color: '#f97316' },
    { number: 2, icon: '📝', nameFr: 'Liste', nameNl: 'Lijst', color: '#a855f7' },
    { number: 3, icon: '💣', nameFr: 'Sabotage', nameNl: 'Sabotage', color: '#4c1d95' },
    { number: 4, icon: '⏱️', nameFr: 'Chrono', nameNl: 'Chrono', color: '#3b82f6' }
  ];

  return (
    <div className={styles['welcome-scene']}>
      {/* Animated title */}
      <motion.div
        className={styles['title-container']}
        initial={{ opacity: 0, y: -30 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.8 }}
      >
        <h1 className={styles['game-title-fr']}>Le Quiz Ultime</h1>
        <h2 className={styles['game-title-nl']}>De Ultieme Quiz</h2>
      </motion.div>

      {/* Phase logos grid */}
      <div className={styles['phases-grid']}>
        {phases.map((phase, index) => (
          <motion.div
            key={phase.number}
            className={styles['phase-card']}
            initial={{ opacity: 0, scale: 0.8 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ delay: 0.2 * index, duration: 0.5 }}
            style={{ borderTopColor: phase.color }}
          >
            <div className={styles['phase-icon']}>{phase.icon}</div>
            <div className={styles['phase-label']}>Phase {phase.number}</div>
            <div className={styles['phase-names']}>
              <div className={styles['phase-name-fr']}>{phase.nameFr}</div>
              <div className={styles['phase-name-nl']}>{phase.nameNl}</div>
            </div>
          </motion.div>
        ))}
      </div>

      {/* Waiting message with pulsing animation */}
      <motion.div
        className={styles['waiting-message']}
        animate={{ opacity: [0.7, 1, 0.7] }}
        transition={{ duration: 2, repeat: Infinity, ease: 'easeInOut' }}
      >
        <p className={styles['waiting-text-fr']}>En attente du Game Master...</p>
        <p className={styles['waiting-text-nl']}>Wachten op de Game Master...</p>
      </motion.div>
    </div>
  );
}
