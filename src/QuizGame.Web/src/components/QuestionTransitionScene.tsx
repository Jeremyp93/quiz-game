import { motion } from 'framer-motion';
import './QuestionTransitionScene.css';

export default function QuestionTransitionScene() {
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
    <div className="question-transition-scene">
      <motion.div
        initial={{ opacity: 0, scale: 0.8 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
        className="transition-container"
      >
        <div className="transition-text">
          <div className="transition-lang">
            <h1 className="transition-title">
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

          <div className="transition-divider"></div>

          <div className="transition-lang">
            <h1 className="transition-title">
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
