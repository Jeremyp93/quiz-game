import { motion } from 'framer-motion';
import { CurrentQuestion, Team } from '../types';
import './AnswerRevealScene.css';

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
    <div className="answer-reveal-scene">
      <motion.div
        variants={container}
        initial="hidden"
        animate="show"
        className="answer-container"
      >
        <motion.div className="answer-card">
          <div className="answer-header">
            <div className="difficulty-badge">
              Difficulty: {question.difficulty}
            </div>
          </div>

          {/* Question Section */}
          <div className="qa-section">
            <div className="section-title">Question</div>
            <div className="qa-bilingual">
              <div className="qa-lang">
                <div className="lang-label">FR</div>
                <div className="qa-text">{question.textFr}</div>
              </div>

              <div className="qa-divider"></div>

              <div className="qa-lang">
                <div className="lang-label">NL</div>
                <div className="qa-text">{question.textNl}</div>
              </div>
            </div>
          </div>

          {/* Answer Section - Animated Reveal */}
          <motion.div variants={answerReveal} className="qa-section answer-section">
            <div className="section-title answer-title">Answer</div>
            <div className="qa-bilingual">
              <div className="qa-lang">
                <div className="lang-label">FR</div>
                <div className="qa-text answer-text">{question.answerFr}</div>
              </div>

              <div className="qa-divider"></div>

              <div className="qa-lang">
                <div className="lang-label">NL</div>
                <div className="qa-text answer-text">{question.answerNl}</div>
              </div>
            </div>
          </motion.div>
        </motion.div>

        {blockedTeamIndices.length > 0 && (
          <motion.div
            initial={{ opacity: 1 }}
            animate={{ opacity: 1 }}
            className="blocked-teams-section"
          >
            <div className="blocked-title">Blocked Teams</div>
            <div className="blocked-teams">
              {blockedTeamIndices.map((index) => (
                <div key={index} className="blocked-team-badge">
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
