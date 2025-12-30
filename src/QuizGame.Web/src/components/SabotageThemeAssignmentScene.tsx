import { motion } from 'framer-motion';
import { GameState } from '../types';
import styles from './SabotageThemeAssignmentScene.module.css';

interface Props {
  gameState: GameState;
}

export default function SabotageThemeAssignmentScene({ gameState }: Props) {
  const { teams, sabotage } = gameState;

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
                  {assignment?.selfSelectedTheme && (
                    <motion.div
                      className={`${styles['theme-badge']} ${styles['self-selected']}`}
                      initial={{ scale: 0 }}
                      animate={{ scale: 1 }}
                      transition={{ delay: 0.8 }}
                    >
                      <div className={styles['theme-type-icon']}>✨</div>
                      <div className={styles['theme-icon']}>{assignment.selfSelectedTheme.icon}</div>
                      <div className={styles['theme-content']}>
                        <div className={styles['theme-name-fr']}>{assignment.selfSelectedTheme.nameFr}</div>
                        <div className={styles['theme-name-nl']}>{assignment.selfSelectedTheme.nameNl}</div>
                      </div>
                    </motion.div>
                  )}
                  {!assignment?.selfSelectedTheme && (
                    <div className={styles['theme-slot-empty']}>
                      <span className={styles['slot-icon']}>✨</span> ?
                    </div>
                  )}

                  {assignment?.sabotageTheme && (
                    <motion.div
                      className={`${styles['theme-badge']} ${styles['sabotage']}`}
                      initial={{ scale: 0 }}
                      animate={{ scale: 1 }}
                      transition={{ delay: 1.0 }}
                    >
                      <div className={styles['theme-type-icon']}>💣</div>
                      <div className={styles['theme-icon']}>{assignment.sabotageTheme.icon}</div>
                      <div className={styles['theme-content']}>
                        <div className={styles['theme-name-fr']}>{assignment.sabotageTheme.nameFr}</div>
                        <div className={styles['theme-name-nl']}>{assignment.sabotageTheme.nameNl}</div>
                      </div>
                    </motion.div>
                  )}
                  {!assignment?.sabotageTheme && (
                    <div className={styles['theme-slot-empty']}>
                      <span className={styles['slot-icon']}>💣</span> ?
                    </div>
                  )}
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
                    </motion.div></div>
                  )}
          <h3>Available Themes</h3>
          <div className={styles['themes-grid']}>
            {sabotage.selectedThemes.map((theme, idx) => {
              const isAssigned = sabotage.teamThemeAssignments.some(
                ta => (ta.selfSelectedTheme?.id === theme.id) || (ta.sabotageTheme?.id === theme.id)
              );

              return (
                <motion.div
                  key={theme.id}
                  className={`${styles['available-theme']} ${isAssigned ? styles['assigned'] : ''}`}
                  initial={{ opacity: 0, scale: 0.8 }}
                  animate={{ opacity: isAssigned ? 0.3 : 1, scale: 1 }}
                  transition={{ delay: 0.6 + idx * 0.05 }}
                >
                  <span className={styles['available-theme-icon']}>{theme.icon}</span>
                  <div className={styles['available-theme-text']}>
                    <div className={styles['theme-name-fr']}>{theme.nameFr}</div>
                    <div className={styles['theme-name-nl']}>{theme.nameNl}</div>
                  </div>
                </motion.div>
              );
            })}
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
