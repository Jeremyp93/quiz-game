import { motion } from 'framer-motion';
import { GameState } from '../types';
import './SabotageThemeAssignmentScene.css';

interface Props {
  gameState: GameState;
}

export default function SabotageThemeAssignmentScene({ gameState }: Props) {
  const { teams, sabotage } = gameState;

  return (
    <div className="sabotage-theme-assignment-scene">
      <motion.h1
        initial={{ opacity: 0, y: -50 }}
        animate={{ opacity: 1, y: 0 }}
        className="phase-title"
      >
        Phase 3: Sabotage
      </motion.h1>

      <motion.h2
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3 }}
        className="subphase-title"
      >
        Theme Assignment
      </motion.h2>

      <div className="assignment-container">
        <div className="teams-section">
          {teams.map((team, idx) => {
            const assignment = sabotage.teamThemeAssignments.find(ta => ta.teamIndex === idx);
            const isCurrentPicker = sabotage.currentPickingTeamIndex === idx;

            return (
              <motion.div
                key={idx}
                className={`team-assignment ${isCurrentPicker ? 'active-picker' : ''}`}
                initial={{ opacity: 0, x: -50 }}
                animate={{ opacity: 1, x: 0 }}
                transition={{ delay: 0.5 + idx * 0.1 }}
              >
                <div className="team-header">
                  <h3>{team.name}</h3>
                  {isCurrentPicker && !sabotage.isThemeAssignmentComplete && (
                    <motion.div
                      className="picker-indicator"
                      animate={{ scale: [1, 1.2, 1] }}
                      transition={{ repeat: Infinity, duration: 1.5 }}
                    >
                      {sabotage.currentPickNumber === 1 ? '✨ Pick 1: Choose your theme' : '💣 Pick 2: Sabotage another team'}
                    </motion.div>
                  )}
                </div>

                <div className="assigned-themes">
                  {assignment?.selfSelectedTheme && (
                    <motion.div
                      className="theme-badge self-selected"
                      initial={{ scale: 0 }}
                      animate={{ scale: 1 }}
                      transition={{ delay: 0.8 }}
                    >
                      <div className="theme-type-icon">✨</div>
                      <div className="theme-icon">{assignment.selfSelectedTheme.icon}</div>
                      <div className="theme-content">
                        <div className="theme-name-fr">{assignment.selfSelectedTheme.nameFr}</div>
                        <div className="theme-name-nl">{assignment.selfSelectedTheme.nameNl}</div>
                      </div>
                    </motion.div>
                  )}
                  {!assignment?.selfSelectedTheme && (
                    <div className="theme-slot-empty">
                      <span className="slot-icon">✨</span> ?
                    </div>
                  )}

                  {assignment?.sabotageTheme && (
                    <motion.div
                      className="theme-badge sabotage"
                      initial={{ scale: 0 }}
                      animate={{ scale: 1 }}
                      transition={{ delay: 1.0 }}
                    >
                      <div className="theme-type-icon">💣</div>
                      <div className="theme-icon">{assignment.sabotageTheme.icon}</div>
                      <div className="theme-content">
                        <div className="theme-name-fr">{assignment.sabotageTheme.nameFr}</div>
                        <div className="theme-name-nl">{assignment.sabotageTheme.nameNl}</div>
                      </div>
                    </motion.div>
                  )}
                  {!assignment?.sabotageTheme && (
                    <div className="theme-slot-empty">
                      <span className="slot-icon">💣</span> ?
                    </div>
                  )}
                </div>
              </motion.div>
            );
          })}
        </div>

        <div className="available-themes">
          <h3>Available Themes</h3>
          <div className="themes-grid">
            {sabotage.selectedThemes.map((theme, idx) => {
              const isAssigned = sabotage.teamThemeAssignments.some(
                ta => (ta.selfSelectedTheme?.id === theme.id) || (ta.sabotageTheme?.id === theme.id)
              );

              return (
                <motion.div
                  key={theme.id}
                  className={`available-theme ${isAssigned ? 'assigned' : ''}`}
                  initial={{ opacity: 0, scale: 0.8 }}
                  animate={{ opacity: isAssigned ? 0.3 : 1, scale: 1 }}
                  transition={{ delay: 0.6 + idx * 0.05 }}
                >
                  <span className="available-theme-icon">{theme.icon}</span>
                  <div className="available-theme-text">
                    <div className="theme-name-fr">{theme.nameFr}</div>
                    <div className="theme-name-nl">{theme.nameNl}</div>
                  </div>
                </motion.div>
              );
            })}
          </div>
        </div>
      </div>

      {sabotage.isThemeAssignmentComplete && (
        <motion.div
          className="completion-message"
          initial={{ opacity: 0, scale: 0.8 }}
          animate={{ opacity: 1, scale: 1 }}
        >
          ✓ Theme Assignment Complete!
        </motion.div>
      )}
    </div>
  );
}
