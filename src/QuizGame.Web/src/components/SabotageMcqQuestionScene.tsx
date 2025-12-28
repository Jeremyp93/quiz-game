import { motion } from 'framer-motion';
import { GameState } from '../types';
import './SabotageMcqQuestionScene.css';

interface Props {
  gameState: GameState;
}

export default function SabotageMcqQuestionScene({ gameState }: Props) {
  const { teams, sabotage } = gameState;

  if (!sabotage.currentMcqQuestion || sabotage.currentPlayingTeamIndex === undefined) {
    return <div>Loading...</div>;
  }

  const currentTeam = teams[sabotage.currentPlayingTeamIndex];
  const question = sabotage.currentMcqQuestion;
  const themeName = sabotage.currentThemeIndex === 0 ? 'Self-Selected' : 'Sabotage';
  const themeIcon = sabotage.currentThemeIndex === 0 ? '✨' : '💣';

  const difficultyStars = '⭐'.repeat(question.difficulty);

  return (
    <div className="sabotage-mcq-question-scene">
      <motion.div
        className="mcq-header"
        initial={{ opacity: 0, y: -50 }}
        animate={{ opacity: 1, y: 0 }}
      >
        <h1 className="phase-title">Phase 3: Sabotage - MCQ</h1>

        <div className="team-info">
          <h2>{currentTeam.name}</h2>
          <div className="theme-info">
            {themeIcon} {question.theme.icon} {question.theme.nameFr} / {question.theme.nameNl}
            <span className="theme-type">({themeName})</span>
          </div>
          <div className="progress-info">
            Theme {(sabotage.currentThemeIndex || 0) + 1}/2 · Question {sabotage.currentQuestionInTheme + 1}/4 · {difficultyStars}
          </div>
        </div>
      </motion.div>

      <motion.div
        className="question-container"
        initial={{ opacity: 0, scale: 0.9 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ delay: 0.3 }}
      >
        <div className="question-text">
          <div className="question-fr">{question.textFr}</div>
          <div className="question-nl">{question.textNl}</div>
        </div>

        <div className="choices">
          <motion.div
            className="choice"
            initial={{ opacity: 0, x: -50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.5 }}
          >
            <div className="choice-letter">A</div>
            <div className="choice-text">
              <div>{question.choiceAFr}</div>
              <div className="choice-nl">{question.choiceANl}</div>
            </div>
          </motion.div>

          <motion.div
            className="choice"
            initial={{ opacity: 0, x: -50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.7 }}
          >
            <div className="choice-letter">B</div>
            <div className="choice-text">
              <div>{question.choiceBFr}</div>
              <div className="choice-nl">{question.choiceBNl}</div>
            </div>
          </motion.div>

          <motion.div
            className="choice"
            initial={{ opacity: 0, x: -50 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.9 }}
          >
            <div className="choice-letter">C</div>
            <div className="choice-text">
              <div>{question.choiceCFr}</div>
              <div className="choice-nl">{question.choiceCNl}</div>
            </div>
          </motion.div>
        </div>
      </motion.div>
    </div>
  );
}
