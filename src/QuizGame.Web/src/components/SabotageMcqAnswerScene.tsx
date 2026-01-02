import { motion } from 'framer-motion';
import { GameState, McqChoice } from '../types';
import styles from './SabotageMcqAnswerScene.module.css';
import sharedStyles from '../styles/shared.module.css';
import '../styles/animations.module.css';
import { isDuplicateText } from '../utils/bilingualHelpers';

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
    <div className={styles['sabotage-mcq-answer-scene']}>
      <motion.div
        className={sharedStyles['mcq-header']}
        initial={{ opacity: 0, y: -80 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{
          duration: 0.6,
          type: "spring",
          stiffness: 100,
          damping: 15
        }}
      >
        <h1 className={sharedStyles['phase-title']}>Phase 3: Sabotage - MCQ</h1>

        <div className={sharedStyles['team-info']}>
          <h2>{currentTeam.name}</h2>
          <div className={sharedStyles['theme-info']}>
            {themeIcon} {question.theme.icon} {question.theme.nameFr} / {question.theme.nameNl}
            <span className={sharedStyles['theme-type']}>({themeName})</span>
          </div>
          <div className={sharedStyles['progress-info']}>
            Theme {(sabotage.currentThemeIndex || 0) + 1}/2 · Question {sabotage.currentQuestionInTheme + 1}/4 · {difficultyStars}
          </div>
        </div>
      </motion.div>

      <motion.div
        className={styles['question-container']}
        initial={{ opacity: 0, scale: 0.95 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.4 }}
      >
        <motion.div
          className={sharedStyles['question-text-dark']}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.3 }}
        >
          <div className={sharedStyles['question-fr-dark']}>{question.textNl}</div>
          <hr></hr>
          <div className={sharedStyles['question-fr-dark']}>{question.textFr}</div>
        </motion.div>

        <div className={styles.choices}>
          {[McqChoice.A, McqChoice.B, McqChoice.C].map((choice, idx) => {
            const choiceText = getChoiceText(choice);
            const choiceClass = getChoiceClass(choice);
            const isCorrect = choice === question.correctChoice;
            const isSelected = sabotage.selectedAnswer === choice;

            // Staggered reveal animation
            const baseDelay = 0.5;
            const revealDelay = baseDelay + (idx * 0.15);

            return (
              <motion.div
                key={choice}
                className={`${styles.choice} ${choiceClass ? styles[choiceClass] : ''}`}
                initial={{ opacity: 0.3, scale: 0.95 }}
                animate={{ opacity: 1, scale: 1 }}
                transition={{
                  delay: revealDelay,
                  duration: 0.3
                }}
              >
                <motion.div
                  className={styles['choice-letter']}
                  initial={{ scale: 1 }}
                  animate={isCorrect ? { scale: [1, 1.2, 1] } : {}}
                  transition={{
                    delay: revealDelay + 0.2,
                    duration: 0.5
                  }}
                >
                  {String.fromCharCode(65 + idx)}
                </motion.div>
                <div className={styles['choice-text']}>
                  {isDuplicateText(choiceText.fr, choiceText.nl) ? (
                    <div>{choiceText.nl}</div>
                  ) : (
                    <>
                      <div>{choiceText.nl}</div>
                      <div className={sharedStyles['choice-fr']}>{choiceText.fr}</div>
                    </>
                  )}
                </div>
                <motion.div
                  className={styles['choice-indicator']}
                  initial={{ opacity: 0, x: 50 }}
                  animate={{ opacity: 1, x: 0 }}
                  transition={{
                    delay: revealDelay + 0.4,
                    duration: 0.4,
                    type: "spring",
                    stiffness: 200
                  }}
                >
                  {isCorrect && <span className={styles['indicator-icon']}>✓</span>}
                  {isSelected && !isCorrect && <span className={styles['indicator-icon']}>✗</span>}
                </motion.div>
              </motion.div>
            );
          })}
        </div>
      </motion.div>
    </div>
  );
}
