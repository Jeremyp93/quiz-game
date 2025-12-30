import { motion } from 'framer-motion';
import styles from './Phase2IntroScene.module.css';

export function Phase2IntroScene() {
  const rules = [
    { fr: "Temps limité pour lister les éléments", nl: "Beperkte tijd om items op te sommen" },
    { fr: "Plus d'éléments = plus de points", nl: "Meer items = meer punten" },
    { fr: "Collaboration d'équipe essentielle", nl: "Teamwerk essentieel" }
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
          PHASE 2
        </motion.div>

        <motion.div
          className={styles['phase-icon']}
          initial={{ rotate: -180, scale: 0 }}
          animate={{ rotate: 0, scale: 1 }}
          transition={{ delay: 0.5, duration: 0.8 }}
        >
          📝
        </motion.div>

        <motion.div
          className={styles['phase-name']}
          initial={{ y: 50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ delay: 0.7 }}
        >
          <div className={styles['name-fr']}>Liste</div>
          <div className={styles['name-nl']}>Lijst</div>
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
          <div className={styles['scoring-fr']}>Points par élément valide listé</div>
          <div className={styles['scoring-nl']}>Punten per geldig item opgelijst</div>
        </motion.div>
      </motion.div>
    </div>
  );
}
