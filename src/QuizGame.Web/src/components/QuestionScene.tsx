import { motion } from 'framer-motion';
import { CurrentQuestion, Team } from '../types';
import styles from './QuestionScene.module.css';

interface Props {
  question: CurrentQuestion;
  teams: Team[];
  blockedTeamIndices: number[];
}

export default function QuestionScene({ question, teams, blockedTeamIndices }: Props) {
  const container = {
    hidden: { opacity: 0 },
    show: {
      opacity: 1,
      transition: {
        staggerChildren: 0.15,
      },
    },
  };

  const questionCard = {
    hidden: { opacity: 0, scale: 0.8, y: 100 },
    show: {
      opacity: 1,
      scale: 1,
      y: 0,
      transition: {
        type: 'spring',
        damping: 15,
        stiffness: 100,
      },
    },
  };

  const teamBadge = {
    hidden: { opacity: 0, x: -50 },
    show: {
      opacity: 1,
      x: 0,
      transition: {
        type: 'spring',
        damping: 12,
        stiffness: 120,
      },
    },
  };

  return (
    <div className={styles['question-scene']}>
      <motion.div
        variants={container}
        initial="hidden"
        animate="show"
        className={styles['question-container']}
      >
        <motion.div variants={questionCard} className={styles['question-card']}>
          <div className={styles['question-header']}>
            <div className={styles['difficulty-badge']}>
              Difficulty: {question.difficulty}
            </div>
          </div>

          <div className={styles['question-bilingual']}>
            <div className={styles['question-lang']}>
              <div className={styles['lang-label']}>NL</div>
              <div className={styles['question-text']}>{question.textNl}</div>
            </div>

            <div className={styles['question-divider']}></div>

            <div className={styles['question-lang']}>
              <div className={styles['lang-label']}>FR</div>
              <div className={styles['question-text']}>{question.textFr}</div>
            </div>
          </div>
        </motion.div>

        {blockedTeamIndices.length > 0 && (
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.5 }}
            className={styles['blocked-teams-section']}
          >
            <div className={styles['blocked-title']}>Blocked Teams</div>
            <div className={styles['blocked-teams']}>
              {blockedTeamIndices.map((index) => (
                <motion.div
                  key={index}
                  variants={teamBadge}
                  className={styles['blocked-team-badge']}
                >
                  {teams[index]?.name || `Team ${index + 1}`}
                </motion.div>
              ))}
            </div>
          </motion.div>
        )}
      </motion.div>
    </div>
  );
}
