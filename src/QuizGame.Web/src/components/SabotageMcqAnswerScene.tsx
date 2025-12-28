import { motion } from 'framer-motion';
import { GameState, McqChoice } from '../types';
import './SabotageMcqAnswerScene.css';

interface Props {
  gameState: GameState;
}

export default function SabotageMcqAnswerScene({ gameState }: Props) {
  const { teams, sabotage } = gameState;

  if (!sabotage.currentMcqQuestion || sabotage.currentPlayingTeamIndex === undefined) {
    return <div>Loading...</div>;
  }

  const currentTeam = teams[sabotage.currentPlayingTeamIndex];
  const question = sabotage.currentMcqQuestion;
  const themeName = sabotage.currentThemeIndex === 0 ? 'Self-Selected' : 'Sabotage';
  const themeIcon = sabotage.currentThemeIndex === 0 ? '✨' : '💣';

  const difficultyStars = '⭐'.repeat(question.difficulty);

  const getChoiceClass = (choice: McqChoice) => {
    const isCorrect = choice === question.correctChoice;
    const isSelected = sabotage.selectedAnswer === choice;

    if (isCorrect) return 'correct';
    if (isSelected && !isCorrect) return 'incorrect';
    return '';
  };

  const getChoiceText = (choice: McqChoice): { fr: string; nl: string } => {
    switch (choice) {
      case McqChoice.A:
        return { fr: question.choiceAFr, nl: question.choiceANl };
      case McqChoice.B:
        return { fr: question.choiceBFr, nl: question.choiceBNl };
      case McqChoice.C:
        return { fr: question.choiceCFr, nl: question.choiceCNl };
    }
  };

  const isCorrectAnswer = sabotage.selectedAnswer === question.correctChoice;

  return (
    <div className="sabotage-mcq-answer-scene">
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
          {[McqChoice.A, McqChoice.B, McqChoice.C].map((choice, idx) => {
            const choiceText = getChoiceText(choice);
            const choiceClass = getChoiceClass(choice);
            const isCorrect = choice === question.correctChoice;
            const isSelected = sabotage.selectedAnswer === choice;

            return (
              <motion.div
                key={choice}
                className={`choice ${choiceClass}`}
                initial={{ opacity: 0, x: -50 }}
                animate={{ opacity: 1, x: 0 }}
                transition={{ delay: 0.5 + idx * 0.2 }}
              >
                <div className="choice-letter">{String.fromCharCode(65 + idx)}</div>
                <div className="choice-text">
                  <div>{choiceText.fr}</div>
                  <div className="choice-nl">{choiceText.nl}</div>
                </div>
                <div className="choice-indicator">
                  {isCorrect && <span className="indicator-icon">✓</span>}
                  {isSelected && !isCorrect && <span className="indicator-icon">✗</span>}
                </div>
              </motion.div>
            );
          })}
        </div>

        <motion.div
          className={`result-banner ${isCorrectAnswer ? 'correct-result' : 'incorrect-result'}`}
          initial={{ opacity: 0, scale: 0.8 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ delay: 1.3 }}
        >
          {isCorrectAnswer ? (
            <>
              <span className="result-icon">🎉</span>
              <span className="result-text">Correct Answer!</span>
              <span className="result-icon">🎉</span>
            </>
          ) : (
            <>
              <span className="result-icon">😞</span>
              <span className="result-text">Wrong Answer</span>
              <span className="result-icon">😞</span>
            </>
          )}
        </motion.div>
      </motion.div>
    </div>
  );
}
