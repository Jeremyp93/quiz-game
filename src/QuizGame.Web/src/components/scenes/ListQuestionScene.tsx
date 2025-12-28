import { motion } from 'framer-motion';
import { CurrentListQuestion, ListTimer, TimerState } from '../../types';
import './ListQuestionScene.css';
import { useEffect, useState } from 'react';

interface ListQuestionSceneProps {
  question: CurrentListQuestion;
  timer: ListTimer;
}

export function ListQuestionScene({ question, timer }: ListQuestionSceneProps) {

  const [currentTime, setCurrentTime] = useState(Date.now());

  // Update current time every 100ms when timer is running
  useEffect(() => {
    if (timer.state === TimerState.Running) {
      const interval = setInterval(() => {
        setCurrentTime(Date.now());
      }, 100);
      return () => clearInterval(interval);
    }
  }, [timer.state]);


  // Calculate remaining seconds from timer
  const getRemainingSeconds = (): number => {
    if (timer.state === TimerState.Idle) {
      return timer.durationSeconds;
    }

    if (timer.state === TimerState.Finished) {
      return 0;
    }

    if (timer.state === TimerState.Paused && timer.pausedAtUtc && timer.startedAtUtc) {
      const startTime = new Date(timer.startedAtUtc).getTime();
      const pausedTime = new Date(timer.pausedAtUtc).getTime();
      const elapsed = (pausedTime - startTime) - timer.accumulatedPausedMs;
      const remaining = (timer.durationSeconds * 1000) - elapsed;
      return Math.max(0, Math.ceil(remaining / 1000));
    }

    if (timer.state === TimerState.Running && timer.startedAtUtc) {
      const startTime = new Date(timer.startedAtUtc).getTime();
      const elapsed = (currentTime - startTime) - timer.accumulatedPausedMs;
      const remaining = (timer.durationSeconds * 1000) - elapsed;
      return Math.max(0, Math.ceil(remaining / 1000));
    }

    return timer.durationSeconds;
  };

  const remainingSeconds = getRemainingSeconds();
  const minutes = Math.floor(remainingSeconds / 60);
  const seconds = remainingSeconds % 60;

  const getTimerStatus = (): string => {
    switch (timer.state) {
      case TimerState.Idle:
        return 'Ready';
      case TimerState.Running:
        return 'Running';
      case TimerState.Paused:
        return 'Paused';
      case TimerState.Finished:
        return 'Finished';
      default:
        return 'Ready';
    }
  };

  const isLowTime = remainingSeconds <= 10 && timer.state === TimerState.Running;

  return (
    <motion.div
      className="list-question-scene"
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      exit={{ opacity: 0 }}
      transition={{ duration: 0.5 }}
    >
      <motion.div
        className="question-content"
        initial={{ y: -50, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ delay: 0.2, duration: 0.6 }}
      >
        <div className="question-text-container">
          <div className="question-lang">
            <span className="lang-label">FR:</span>
            <p className="question-text">{question.textFr}</p>
          </div>
          <div className="question-divider"></div>
          <div className="question-lang">
            <span className="lang-label">NL:</span>
            <p className="question-text">{question.textNl}</p>
          </div>
        </div>
      </motion.div>

      <motion.div
        className={`timer-display ${isLowTime ? 'low-time' : ''} timer-${timer.state}`}
        initial={{ scale: 0.8, opacity: 0 }}
        animate={{ scale: 1, opacity: 1 }}
        transition={{ delay: 0.4, duration: 0.6, type: 'spring' }}
      >
        <div className="timer-status">{getTimerStatus()}</div>
        <div className="timer-value">
          {String(minutes).padStart(2, '0')}:{String(seconds).padStart(2, '0')}
        </div>
      </motion.div>
    </motion.div>
  );
}
