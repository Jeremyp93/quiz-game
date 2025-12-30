import { motion } from 'framer-motion';
import { Phase } from '../types';
import styles from './QuestionTransitionScene.module.css';

interface QuestionTransitionSceneProps {
  currentPhase: Phase;
}

export default function QuestionTransitionScene({ currentPhase }: QuestionTransitionSceneProps) {
  const dotVariants = {
    animate: {
      opacity: [0, 1, 0],
      transition: {
        duration: 1.5,
        repeat: Infinity,
        ease: 'easeInOut',
      },
    },
  };

  // Determine phase-specific class
  const phaseClass = currentPhase === Phase.FastBuzzer
    ? styles['phase-1']
    : currentPhase === Phase.Sabotage
    ? styles['phase-3']
    : '';

  return (
    <div className={`${styles['question-transition-scene']} ${phaseClass}`}>
      <motion.div
        initial={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
        className={styles['transition-container']}
      >
        <div className={styles['transition-text']}>
          <div className={styles['transition-lang']}>
            <h1 className={styles['transition-title']}>
              Prochaine question
              <motion.span
                variants={dotVariants}
                animate="animate"
                transition={{ delay: 0 }}
              >
                .
              </motion.span>
              <motion.span
                variants={dotVariants}
                animate="animate"
                transition={{ delay: 0.3 }}
              >
                .
              </motion.span>
              <motion.span
                variants={dotVariants}
                animate="animate"
                transition={{ delay: 0.6 }}
              >
                .
              </motion.span>
            </h1>
          </div>

          <div className={styles['transition-divider']}></div>

          <div className={styles['transition-lang']}>
            <h1 className={styles['transition-title']}>
              Volgende vraag
              <motion.span
                variants={dotVariants}
                animate="animate"
                transition={{ delay: 0 }}
              >
                .
              </motion.span>
              <motion.span
                variants={dotVariants}
                animate="animate"
                transition={{ delay: 0.3 }}
              >
                .
              </motion.span>
              <motion.span
                variants={dotVariants}
                animate="animate"
                transition={{ delay: 0.6 }}
              >
                .
              </motion.span>
            </h1>
          </div>
        </div>
      </motion.div>
    </div>
  );
}
