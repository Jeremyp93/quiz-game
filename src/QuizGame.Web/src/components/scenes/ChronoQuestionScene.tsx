import { motion, AnimatePresence } from 'framer-motion';
import { GameState, TimerState, ChronoRunStatus } from '../../types';
import styles from './ChronoQuestionScene.module.css';
import { useEffect, useState } from 'react';

interface Props {
  gameState: GameState;
}

export function ChronoQuestionScene({ gameState }: Props) {
  const { chrono, teams } = gameState;
  const [currentTime, setCurrentTime] = useState(Date.now());

  // Update current time every 50ms for smooth timer
  useEffect(() => {
    if (chrono.timerState === TimerState.Running) {
      const interval = setInterval(() => {
        setCurrentTime(Date.now());
      }, 50);
      return () => clearInterval(interval);
    }
  }, [chrono.timerState]);

  if (chrono.activeTeamIndex === null || chrono.activeTeamIndex === undefined || !chrono.currentQuestion) {
    return <div>Loading...</div>;
  }

  const activeTeam = teams[chrono.activeTeamIndex];
  const isCountdownMode = chrono.bestTimeMs !== null && chrono.bestTimeMs !== undefined;

  // Calculate timer display
  const getTimerMs = (): number => {
    if (!chrono.timerStartedAtUtc) return chrono.bestTimeMs ?? 0;

    const startTime = new Date(chrono.timerStartedAtUtc).getTime();
    let elapsedMs: number;

    if (chrono.timerState === TimerState.Paused && chrono.timerPausedAtUtc) {
      const pausedTime = new Date(chrono.timerPausedAtUtc).getTime();
      elapsedMs = (pausedTime - startTime) - chrono.timerAccumulatedPausedMs;
    } else if (chrono.timerState === TimerState.Running) {
      elapsedMs = (currentTime - startTime) - chrono.timerAccumulatedPausedMs;
    } else if (chrono.timerState === TimerState.Finished && chrono.timerFinishedAtUtc) {
      const finishedTime = new Date(chrono.timerFinishedAtUtc).getTime();
      elapsedMs = (finishedTime - startTime) - chrono.timerAccumulatedPausedMs;
    } else {
      elapsedMs = 0;
    }

    if (isCountdownMode) {
      // Countdown: show remaining
      return Math.max(0, chrono.bestTimeMs! - elapsedMs);
    } else {
      // Count-up: show elapsed
      return elapsedMs;
    }
  };

  const timerMs = getTimerMs();
  const totalSeconds = Math.floor(timerMs / 1000);
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  const milliseconds = Math.floor((timerMs % 1000) / 10);

  const getTimerStatus = (): string => {
    switch (chrono.runStatus) {
      case ChronoRunStatus.Running:
        return 'Running';
      case ChronoRunStatus.Paused:
        return 'Paused';
      case ChronoRunStatus.Finished:
        return 'Finished';
      case ChronoRunStatus.NotFinished:
        return 'Time Up';
      default:
        return 'Ready';
    }
  };

  // Low time warning in countdown mode
  const isLowTime = isCountdownMode && timerMs <= 10000 && chrono.timerState === TimerState.Running;

  return (
    <div className={styles['chrono-scene']}>
      {/* Team Header */}
      <motion.div
        className={styles['team-header']}
        initial={{ opacity: 0, y: -50 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
      >
        <h1 className={styles['team-name']}>{activeTeam.name}</h1>
        {isCountdownMode && chrono.bestTimeMs && (
          <div className={styles['time-to-beat']}>
            <span className={styles['label-nl']}>Te kloppen tijd:</span>{' '}
            <span className={styles['label-fr']}>Temps à battre:</span>{' '}
            <span className={styles['time-value']}>{formatTime(chrono.bestTimeMs)}</span>
          </div>
        )}
      </motion.div>

      {/* Question Display */}
      <motion.div
        className={styles['question-container']}
        key={chrono.currentQuestion.id}
        initial={{ opacity: 0, scale: 0.95 }}
        animate={{ opacity: 1, scale: 1 }}
        exit={{ opacity: 0, scale: 1.05 }}
        transition={{ duration: 0.4 }}
      >
        <div className={styles['question-text']}>
          <div className={styles['question-lang']}>
            <span className={styles['lang-label']}>NL:</span>
            <p>{chrono.currentQuestion.textNl}</p>
          </div>
          <div className={styles['question-divider']}></div>
          <div className={styles['question-lang']}>
            <span className={styles['lang-label']}>FR:</span>
            <p>{chrono.currentQuestion.textFr}</p>
          </div>
        </div>
      </motion.div>

      {/* Milestone Progress */}
      <motion.div
        className={styles['milestone-container']}
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.3, duration: 0.5 }}
      >
        <div className={styles['milestone-grid']}>
          {Array.from({ length: 10 }, (_, i) => (
            <motion.div
              key={i}
              className={`${styles['milestone-circle']} ${
                i < chrono.correctCount ? styles['completed'] : ''
              }`}
              initial={{ scale: 0, opacity: 0 }}
              animate={{ scale: 1, opacity: 1 }}
              transition={{
                delay: 0.4 + (i * 0.05),
                duration: 0.3,
                type: 'spring',
                stiffness: 200
              }}
            >
              <AnimatePresence>
                {i < chrono.correctCount && (
                  <motion.div
                    className={styles['checkmark']}
                    initial={{ scale: 0, rotate: -180 }}
                    animate={{ scale: 1, rotate: 0 }}
                    exit={{ scale: 0 }}
                    transition={{
                      duration: 0.4,
                      type: 'spring',
                      stiffness: 300
                    }}
                  >
                    ✓
                  </motion.div>
                )}
              </AnimatePresence>
              <span className={styles['milestone-number']}>{i + 1}</span>
            </motion.div>
          ))}
        </div>
        <div className={styles['progress-label']}>
          {chrono.correctCount}/10 Correct
        </div>
      </motion.div>

      {/* Timer Display */}
      <motion.div
        className={`${styles['timer-display']} ${
          isLowTime ? styles['low-time'] : ''
        } ${styles[`timer-${chrono.timerState}`]}`}
        initial={{ scale: 0.8, opacity: 0 }}
        animate={{ scale: 1, opacity: 1 }}
        transition={{ delay: 0.5, duration: 0.6, type: 'spring' }}
      >
        <div className={styles['timer-status']}>{getTimerStatus()}</div>
        <div className={styles['timer-value']}>
          {String(minutes).padStart(2, '0')}:{String(seconds).padStart(2, '0')}.
          {String(milliseconds).padStart(2, '0')}
        </div>
        <div className={styles['timer-mode']}>
          {isCountdownMode ? 'Countdown' : 'Count Up'}
        </div>
      </motion.div>
    </div>
  );
}

function formatTime(ms: number): string {
  const totalSeconds = Math.floor(ms / 1000);
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  const milliseconds = Math.floor((ms % 1000) / 10);

  return `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}.${String(milliseconds).padStart(2, '0')}`;
}
