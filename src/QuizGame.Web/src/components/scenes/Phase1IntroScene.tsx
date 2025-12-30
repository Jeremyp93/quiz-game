import { motion } from 'framer-motion';
import styles from './Phase1IntroScene.module.css';

export function Phase1IntroScene() {
  const rules = [
    { fr: "Les équipes se précipitent pour répondre en premier", nl: "Teams racen om als eerste te antwoorden" },
    { fr: "Les mauvaises réponses bloquent la question suivante", nl: "Foute antwoorden blokkeren de volgende vraag" },
    { fr: "Réflexion rapide requise", nl: "Snel denken vereist" }
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
          PHASE 1
        </motion.div>

        <motion.div
          className={styles['phase-icon']}
          initial={{ rotate: -180, scale: 0 }}
          animate={{ rotate: 0, scale: 1 }}
          transition={{ delay: 0.5, duration: 0.8 }}
        >
          ⚡
        </motion.div>

        <motion.div
          className={styles['phase-name']}
          initial={{ y: 50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ delay: 0.7 }}
        >
          <div className={styles['name-nl']}>Fast Buzzer</div>
          <div className={styles['name-fr']}>Buzzer Rapide</div>
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
                <div className={styles['rule-nl']}>{rule.nl}</div>
                <div className={styles['rule-fr']}>{rule.fr}</div>
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
          <div className={styles['scoring-nl']}>Punten voor goede antwoorden, straf voor blokkades</div>
          <div className={styles['scoring-fr']}>Points pour bonnes réponses, pénalités pour blocages</div>
        </motion.div>
      </motion.div>
    </div>
  );
}
