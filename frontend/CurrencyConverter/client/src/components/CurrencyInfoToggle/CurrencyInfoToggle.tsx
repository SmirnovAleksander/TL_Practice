import arrow from '../../assets/arrow.svg';
import styles from './CurrencyInfoToggle.module.scss';

const label = 'PLN/JPY: about';

const CurrencyInfoToggle = () => {
    return (
        <div className={styles.wrapper}>
            <hr className={styles.line} />
            <button type="button" className={styles.button}>
                {label} <img src={arrow} alt="" className={styles.arrow}/>
            </button>
        </div>
    );
};

export default CurrencyInfoToggle;