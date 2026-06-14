import styles from './PeriodSwitcher.module.scss';

type PeriodSwitcherProps = {
    period: number;
    handlePeriodChange: (period: number) => void;
};

const periods = [1, 2, 3, 4, 5];

export const PeriodSwitcher = ({ period, handlePeriodChange }: PeriodSwitcherProps) => {
    return (
        <div className={styles.wrapper}>
            {periods.map((p) => (
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