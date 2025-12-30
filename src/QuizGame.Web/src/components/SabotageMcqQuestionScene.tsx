import { motion } from 'framer-motion';
import { GameState } from '../types';
import styles from './SabotageMcqQuestionScene.module.css';
import sharedStyles from '../styles/shared.module.css';
import '../styles/animations.module.css';

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
    <div className={styles['sabotage-mcq-question-scene']}>
      <motion.div
        key="mcq-header"
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
        key={`question-${question.id}`}
        className={sharedStyles['question-container']}
        initial={{ opacity: 0, scale: 0.9 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{
          delay: 0.5,
          duration: 0.4,
          ease: "easeOut"
        }}
      >
        <motion.div
          className={sharedStyles['question-text-light']}
          initial={{ opacity: 0, y: 30 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.7, duration: 0.4 }}
        >
          <div className={sharedStyles['question-nl-light']}>{question.textNl}</div>
          <div className={sharedStyles['question-fr-light']}>{question.textFr}</div>
        </motion.div>

        <div className={styles.choices}>
          {[0, 1, 2].map((index) => {
            const isSelected = sabotage.selectedAnswer === index;
            const letter = String.fromCharCode(65 + index);
            const choiceData = [
              { fr: question.choiceAFr, nl: question.choiceANl },
              { fr: question.choiceBFr, nl: question.choiceBNl },
              { fr: question.choiceCFr, nl: question.choiceCNl }
            ][index];

            return (
              <motion.div
                key={index}
                className={`${styles.choice} ${isSelected ? styles.selected : ''}`}
                initial={{ opacity: 0, x: -100 }}
                animate={{ opacity: 1, x: 0 }}
                transition={{
                  delay: 0.8 + (index * 0.2),
                  duration: 0.5,
                  type: "spring",
                  stiffness: 80
                }}
              >
                <div className={styles['choice-letter']}>{letter}</div>
                <div className={styles['choice-text']}>
                  <div>{choiceData.nl}</div>
                  <div className={sharedStyles['choice-fr']}>{choiceData.fr}</div>
                </div>
              </motion.div>
            );
          })}
        </div>
      </motion.div>
    </div>
  );
}
