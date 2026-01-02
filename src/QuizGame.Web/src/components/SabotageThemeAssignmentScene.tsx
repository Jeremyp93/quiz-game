import { motion, AnimatePresence } from 'framer-motion';
import { GameState } from '../types';
import styles from './SabotageThemeAssignmentScene.module.css';
import { isDuplicateText } from '../utils/bilingualHelpers';

interface Props {
  gameState: GameState;
}

export default function SabotageThemeAssignmentScene({ gameState }: Props) {
  const { teams, sabotage } = gameState;

  // Helper to determine if a theme is assigned and where
  const getThemeAssignmentInfo = (themeId: string) => {
    for (const assignment of sabotage.teamThemeAssignments) {
      if (assignment.selfSelectedTheme?.id === themeId) {
        return { isAssigned: true, type: 'self-selected', teamIndex: assignment.teamIndex };
      }
      if (assignment.sabotageTheme?.id === themeId) {
        return { isAssigned: true, type: 'sabotage', teamIndex: assignment.teamIndex };
      }
    }
    return { isAssigned: false, type: null, teamIndex: null };
  };

  return (
    <div className={styles['sabotage-theme-assignment-scene']}>
      <motion.h1
        initial={{ opacity: 0, y: -50 }}
        animate={{ opacity: 1, y: 0 }}
        className={styles['phase-title']}
      >
        Phase 3: Sabotage
      </motion.h1>

      <motion.h2
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3 }}
        className={styles['subphase-title']}
      >
        Theme Assignment
      </motion.h2>

      <div className={styles['assignment-container']}>
        <div className={styles['teams-section']}>
          {teams.map((team, idx) => {
            const assignment = sabotage.teamThemeAssignments.find(ta => ta.teamIndex === idx);
            const isCurrentPicker = sabotage.currentPickingTeamIndex === idx;

            return (
              <motion.div
                key={idx}
                className={`${styles['team-assignment']} ${isCurrentPicker ? styles['active-picker'] : ''}`}
                initial={{ opacity: 0, x: -50 }}
                animate={{ opacity: 1, x: 0 }}
                transition={{ delay: 0.5 + idx * 0.1 }}
              >
                <div className={styles['team-header']}>
                  <h3>{team.name}</h3>
                </div>

                <div className={styles['assigned-themes']}>
                  <AnimatePresence mode="wait">
                    {assignment?.selfSelectedTheme ? (
                      <motion.div
                        key={`theme-${assignment.selfSelectedTheme.id}-self`}
                        layoutId={`theme-${assignment.selfSelectedTheme.id}`}
                        className={`${styles['theme-badge']} ${styles['self-selected']}`}
                        transition={{ type: "spring", stiffness: 200, damping: 25 }}
                      >
                        <div className={styles['theme-type-icon']}>✨</div>
                        <div className={styles['theme-icon']}>{assignment.selfSelectedTheme.icon}</div>
                        <div className={styles['theme-content']}>
                          <div className={styles['theme-name-fr']}>{assignment.selfSelectedTheme.nameNl}</div>
                          {!isDuplicateText(assignment.selfSelectedTheme.nameFr, assignment.selfSelectedTheme.nameNl) && (
                            <div className={styles["theme-name-fr"]}>{assignment.selfSelectedTheme.nameFr}</div>
                          )}
                        </div>
                      </motion.div>
                    ) : (
                      <div key="self-empty" className={styles['theme-slot-empty']}>
                        <span className={styles['slot-icon']}>✨</span> ?
                      </div>
                    )}
                  </AnimatePresence>

                  <AnimatePresence mode="wait">
                    {assignment?.sabotageTheme ? (
                      <motion.div
                        key={`theme-${assignment.sabotageTheme.id}-sabotage`}
                        layoutId={`theme-${assignment.sabotageTheme.id}`}
                        className={`${styles['theme-badge']} ${styles['sabotage']}`}
                        transition={{ type: "spring", stiffness: 200, damping: 25 }}
                      >
                        <div className={styles['theme-type-icon']}>💣</div>
                        <div className={styles['theme-icon']}>{assignment.sabotageTheme.icon}</div>
                        <div className={styles['theme-content']}>
                          <div className={styles['theme-name-fr']}>{assignment.sabotageTheme.nameNl}</div>
                          {!isDuplicateText(assignment.sabotageTheme.nameFr, assignment.sabotageTheme.nameNl) && (
                            <div className={styles["theme-name-fr"]}>{assignment.sabotageTheme.nameFr}</div>
                          )}
                        </div>
                      </motion.div>
                    ) : (
                      <div key="sabotage-empty" className={styles['theme-slot-empty']}>
                        <span className={styles['slot-icon']}>💣</span> ?
                      </div>
                    )}
                  </AnimatePresence>
                </div>
              </motion.div>
            );
          })}
        </div>

        <div className={styles['available-themes']}>
          {!sabotage.isThemeAssignmentComplete && (
            <div className={styles['theme-action']}>
              <motion.div
                className={styles['picker-indicator']}
                animate={{ scale: [1, 1.2, 1] }}
                transition={{ repeat: Infinity, duration: 1.5 }}
              >
                {sabotage.currentPickNumber === 1 ? '✨ Pick 1: Choose your theme' : '💣 Pick 2: Sabotage another team'}
              </motion.div>
            </div>
          )}
          <h3>Available Themes</h3>
          <div className={styles['themes-grid']}>
            <AnimatePresence>
              {sabotage.selectedThemes.map((theme, idx) => {
                const assignmentInfo = getThemeAssignmentInfo(theme.id);

                // If theme is assigned, don't render it in the available list
                if (assignmentInfo.isAssigned) {
                  return null;
                }

                return (
                  <motion.div
                    key={theme.id}
                    layoutId={`theme-${theme.id}`}
                    className={styles['available-theme']}
                    initial={{ opacity: 0, scale: 0.8 }}
                    animate={{ opacity: 1, scale: 1 }}
                    exit={{ opacity: 0, scale: 0.8 }}
                    transition={{ delay: 0.6 + idx * 0.05 }}
                  >
                    <span className={styles['available-theme-icon']}>{theme.icon}</span>
                    <div className={styles["available-theme-text"]}>
                      <div className={styles["theme-name-fr"]}>{theme.nameNl}</div>

                      {!isDuplicateText(theme.nameFr, theme.nameNl) && (
                        <div className={styles["theme-name-fr"]}>{theme.nameFr}</div>
                      )}
                    </div>
                  </motion.div>
                );
              })}
            </AnimatePresence>
          </div>
        </div>
      </div>
      {sabotage.isThemeAssignmentComplete && (
        <motion.div
          className={styles['completion-message']}
          initial={{ opacity: 0, scale: 0.8 }}
          animate={{ opacity: 1, scale: 1 }}
        >
          ✓ Theme Assignment Complete!
        </motion.div>
      )}
    </div>
  );
}
