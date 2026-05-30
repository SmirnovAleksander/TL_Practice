import arrow from '../../assets/arrow.svg';
import styles from './MoreAboutButton.module.scss';

const label = 'PLN/JPY: about';

export const MoreAboutButton = () => {
    return (
        <div className={styles.wrapper}>
            <hr className={styles.line} />
            <button type="button" data-testid="more-about-button" className={styles.button}>
                {label} <img src={arrow} alt="" className={styles.arrow} />
            </button>
        </div>
    );
};
