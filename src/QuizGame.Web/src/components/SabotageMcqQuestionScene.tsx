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
        key="mcq-header"
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
        key={`question-${question.id}`}
        className="question-container"
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.4 }}
      >
        <div className="question-text">
          <div className="question-fr">{question.textFr}</div>
          <div className="question-nl">{question.textNl}</div>
        </div>

        <div className="choices">
          <div className={`choice ${sabotage.selectedAnswer === 0 ? 'selected' : ''}`}>
            <div className="choice-letter">A</div>
            <div className="choice-text">
              <div>{question.choiceAFr}</div>
              <div className="choice-nl">{question.choiceANl}</div>
            </div>
          </div>

          <div className={`choice ${sabotage.selectedAnswer === 1 ? 'selected' : ''}`}>
            <div className="choice-letter">B</div>
            <div className="choice-text">
              <div>{question.choiceBFr}</div>
              <div className="choice-nl">{question.choiceBNl}</div>
            </div>
          </div>

          <div className={`choice ${sabotage.selectedAnswer === 2 ? 'selected' : ''}`}>
            <div className="choice-letter">C</div>
            <div className="choice-text">
              <div>{question.choiceCFr}</div>
              <div className="choice-nl">{question.choiceCNl}</div>
            </div>
          </div>
        </div>
      </motion.div>
    </div>
  );
}
