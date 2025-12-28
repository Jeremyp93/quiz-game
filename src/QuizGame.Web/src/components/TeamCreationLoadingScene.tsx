import { motion } from 'framer-motion';
import './TeamCreationLoadingScene.css';

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
    <div className="team-creation-loading-scene">
      <motion.div
        initial={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
        className="loading-container"
      >
        <motion.div
          variants={spinnerVariants}
          animate="animate"
          className="spinner"
        >
          <div className="spinner-circle"></div>
        </motion.div>

        <div className="loading-text">
          <div className="loading-lang">
            <h1 className="loading-title">
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
            <p className="loading-subtitle">Distribution des joueurs en équipes...</p>
          </div>

          <div className="loading-divider"></div>

          <div className="loading-lang">
            <h1 className="loading-title">
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
            <p className="loading-subtitle">Spelers worden in teams verdeeld...</p>
          </div>
        </div>
      </motion.div>
    </div>
  );
}
