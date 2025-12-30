import { motion } from 'framer-motion';
import styles from './TeamCreationLoadingScene.module.css';

export default function TeamCreationLoadingScene() {
  const spinnerVariants = {
    animate: {
      rotate: 360,
      transition: {
        duration: 1.5,
        repeat: Infinity,
        ease: 'linear',
      },
    },
  };

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

  return (
    <div className={styles['team-creation-loading-scene']}>
      <motion.div
        initial={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
        className={styles['loading-container']}
      >
        <motion.div
          variants={spinnerVariants}
          animate="animate"
          className={styles['spinner']}
        >
          <div className={styles['spinner-circle']}></div>
        </motion.div>

        <div className={styles['loading-text']}>
          <div className={styles['loading-lang']}>
            <h1 className={styles['loading-title']}>
              Création des équipes
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
            <p className={styles['loading-subtitle']}>Distribution des joueurs en équipes...</p>
          </div>

          <div className={styles['loading-divider']}></div>

          <div className={styles['loading-lang']}>
            <h1 className={styles['loading-title']}>
              Teams worden gemaakt
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
            <p className={styles['loading-subtitle']}>Spelers worden in teams verdeeld...</p>
          </div>
        </div>
      </motion.div>
    </div>
  );
}
