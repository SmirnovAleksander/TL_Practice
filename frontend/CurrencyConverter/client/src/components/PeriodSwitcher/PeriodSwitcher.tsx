import styles from './PeriodSwitcher.module.scss';
import { PERIODS } from './periods';

type PeriodSwitcherProps = {
  period: number;
  handlePeriodChange: (period: number) => void;
};

export const PeriodSwitcher = ({ period, handlePeriodChange }: PeriodSwitcherProps) => {
  return (
    <div className={styles.wrapper}>
      {PERIODS.map((p) => (
        <button
          key={p}
          type="button"
          className={`${styles.button} ${p === period ? styles.active : ''}`}
          onClick={() => handlePeriodChange(p)}
        >
          {p} min.
        </button>
      ))}
    </div>
  );
};
