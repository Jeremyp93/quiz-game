import { motion } from 'framer-motion';
import styles from './Phase3IntroScene.module.css';

export function Phase3IntroScene() {
  const rules = [
    { fr: "Choisissez un thème pour votre équipe", nl: "Kies een thema voor je team" },
    { fr: "Sabotez vos adversaires avec des questions difficiles", nl: "Saboteer tegenstanders met moeilijke vragen" },
    { fr: "Répondez correctement aux QCM pour gagner", nl: "Beantwoord MCQ's correct om te winnen" }
  ];

  return (
    <div className={styles['intro-scene']}>
      <motion.div
        className={styles['intro-container']}
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
      >
        <motion.div
          className={styles['phase-number']}
          initial={{ scale: 0 }}
          animate={{ scale: 1 }}
          transition={{ delay: 0.2, type: 'spring' }}
        >
          PHASE 3
        </motion.div>

        <motion.div
          className={styles['phase-icon']}
          initial={{ rotate: -180, scale: 0 }}
          animate={{ rotate: 0, scale: 1 }}
          transition={{ delay: 0.5, duration: 0.8 }}
        >
          💣
        </motion.div>

        <motion.div
          className={styles['phase-name']}
          initial={{ y: 50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ delay: 0.7 }}
        >
          <div className={styles['name-fr']}>Sabotage</div>
          <div className={styles['name-nl']}>Sabotage</div>
        </motion.div>

        <motion.div
          className={styles['rules-container']}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 1.0 }}
        >
          {rules.map((rule, i) => (
            <motion.div
              key={i}
              className={styles['rule-item']}
              initial={{ x: -50, opacity: 0 }}
              animate={{ x: 0, opacity: 1 }}
              transition={{ delay: 1.2 + i * 0.2 }}
            >
              <div className={styles['rule-bullet']}>•</div>
              <div className={styles['rule-text']}>
                <div className={styles['rule-fr']}>{rule.fr}</div>
                <div className={styles['rule-nl']}>{rule.nl}</div>
              </div>
            </motion.div>
          ))}
        </motion.div>

        <motion.div
          className={styles['scoring']}
          initial={{ y: 30, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ delay: 2.0 }}
        >
          <div className={styles['scoring-fr']}>Points pour réponses QCM correctes</div>
          <div className={styles['scoring-nl']}>Punten voor correcte MCQ-antwoorden</div>
        </motion.div>
      </motion.div>
    </div>
  );
}
