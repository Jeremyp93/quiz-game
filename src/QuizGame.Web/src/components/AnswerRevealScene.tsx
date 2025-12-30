import { motion } from 'framer-motion';
import { CurrentQuestion, Team } from '../types';
import styles from './AnswerRevealScene.module.css';
import { isDuplicateText } from '../utils/bilingualHelpers';

interface Props {
  question: CurrentQuestion;
  teams: Team[];
  blockedTeamIndices: number[];
}

export default function AnswerRevealScene({ question, teams, blockedTeamIndices }: Props) {
  const container = {
    hidden: { opacity: 1 },
    show: {
      opacity: 1,
      transition: {
        staggerChildren: 0.2,
      },
    },
  };

  const answerReveal = {
    hidden: { opacity: 0, y: 20, scale: 0.95 },
    show: {
      opacity: 1,
      y: 0,
      scale: 1,
      transition: {
        type: 'spring',
        damping: 20,
        stiffness: 100,
        duration: 0.8,
      },
    },
  };

  return (
    <div className={styles['answer-reveal-scene']}>
      <motion.div
        variants={container}
        initial="hidden"
        animate="show"
        className={styles['answer-container']}
      >
        <motion.div className={styles['answer-card']}>
          <div className={styles['answer-header']}>
            <div className={styles['difficulty-badge']}>
              Difficulty: {question.difficulty}
            </div>
          </div>

          {/* Question Section */}
          <div className={styles['qa-section']}>
            <div className={styles['section-title']}>Question</div>
            <div className={styles['qa-bilingual']}>
              <div className={styles['qa-lang']}>
                <div className={styles['lang-label']}>NL</div>
                <div className={styles['qa-text']}>{question.textNl}</div>
              </div>

              <div className={styles['qa-divider']}></div>

              <div className={styles['qa-lang']}>
                <div className={styles['lang-label']}>FR</div>
                <div className={styles['qa-text']}>{question.textFr}</div>
              </div>
            </div>
          </div>

          {/* Answer Section - Animated Reveal */}
          <motion.div variants={answerReveal} className={`${styles['qa-section']} ${styles['answer-section']}`}>
            <div className={`${styles['section-title']} ${styles['answer-title']}`}>Answer</div>
            {isDuplicateText(question.answerFr, question.answerNl) ? (
              <div className={styles['qa-bilingual']}>
                <div className={`${styles['qa-text']} ${styles['answer-text']}`}>
                  {question.answerNl}
                </div>
              </div>
            ) : (
              <div className={styles['qa-bilingual']}>
                <div className={styles['qa-lang']}>
                  <div className={styles['lang-label']}>NL</div>
                  <div className={`${styles['qa-text']} ${styles['answer-text']}`}>{question.answerNl}</div>
                </div>

                <div className={styles['qa-divider']}></div>

                <div className={styles['qa-lang']}>
                  <div className={styles['lang-label']}>FR</div>
                  <div className={`${styles['qa-text']} ${styles['answer-text']}`}>{question.answerFr}</div>
                </div>
              </div>
            )}
          </motion.div>
        </motion.div>

        {blockedTeamIndices.length > 0 && (
          <motion.div
            initial={{ opacity: 1 }}
            animate={{ opacity: 1 }}
            className={styles['blocked-teams-section']}
          >
            <div className={styles['blocked-title']}>Blocked Teams</div>
            <div className={styles['blocked-teams']}>
              {blockedTeamIndices.map((index) => (
                <div key={index} className={styles['blocked-team-badge']}>
                  {teams[index]?.name || `Team ${index + 1}`}
                </div>
              ))}
            </div>
          </motion.div>
        )}
      </motion.div>
    </div>
  );
}
