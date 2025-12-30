import { motion } from 'framer-motion';
import styles from './Phase4IntroScene.module.css';

export function Phase4IntroScene() {
  const rules = [
    { fr: "Répondez correctement à 10 questions", nl: "Beantwoord 10 vragen correct" },
    { fr: "Le temps le plus rapide gagne", nl: "De snelste tijd wint" },
    { fr: "Battez le meilleur temps", nl: "Versla de beste tijd" }
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
          PHASE 4
        </motion.div>

        <motion.div
          className={styles['phase-icon']}
          initial={{ rotate: -180, scale: 0 }}
          animate={{ rotate: 0, scale: 1 }}
          transition={{ delay: 0.5, duration: 0.8 }}
        >
          ⏱️
        </motion.div>

        <motion.div
          className={styles['phase-name']}
          initial={{ y: 50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ delay: 0.7 }}
        >
          <div className={styles['name-nl']}>Chrono</div>
          <div className={styles['name-fr']}>Chrono</div>
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
          <div className={styles['scoring-nl']}>Ranking op basis van tijd (snelste = meeste punten)</div>
          <div className={styles['scoring-fr']}>Classement basé sur le temps (le plus rapide = le plus de points)</div>
        </motion.div>
      </motion.div>
    </div>
  );
}
