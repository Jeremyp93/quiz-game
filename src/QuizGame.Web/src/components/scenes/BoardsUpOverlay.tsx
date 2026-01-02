import { motion } from 'framer-motion';
import styles from './BoardsUpOverlay.module.css';

export function BoardsUpOverlay() {
  return (
    <motion.div
      className={styles['boards-up-overlay']}
      initial={{ opacity: 0, scale: 0.5 }}
      animate={{ opacity: 1, scale: 1 }}
      exit={{ opacity: 0, scale: 0.5 }}
      transition={{ duration: 0.5, type: 'spring', stiffness: 200 }}
    >
      <motion.div
        className={styles['boards-up-content']}
        initial={{ y: -100 }}
        animate={{ y: 0 }}
        transition={{ delay: 0.2, duration: 0.6, type: 'spring', stiffness: 150 }}
      >
        <motion.h1
          className={styles['boards-up-title']}
          animate={{
            scale: [1, 1.1, 1],
          }}
          transition={{
            duration: 1,
            repeat: Infinity,
            repeatType: 'reverse',
          }}
        >
          <p>STOPPEN MET SCHRIJVEN!</p>
          <p>ARRETER D'ECRIRE!</p>
        </motion.h1>
        <motion.div
          className={styles['boards-up-subtitle']}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.4, duration: 0.5 }}
        >
          <p>Handen omhoog!</p>
          <p>Levez les mains !</p>
        </motion.div>
      </motion.div>
    </motion.div>
  );
}
