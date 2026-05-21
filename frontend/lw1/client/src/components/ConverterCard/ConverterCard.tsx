import ConverterHeader from "../ConverterHeader/ConverterHeader";
import CurrencyInfo from "../CurrencyInfo/CurrencyInfo";
import CurrencyInputGroup from "../CurrencyInputGroup/CurrencyInputGroup";
import styles from './ConverterCard.module.scss';

const ConverterCard = () => {
    return (
        <div className={styles.card}>
            <ConverterHeader/>
            <CurrencyInputGroup/>
            <CurrencyInfo/>
        </div>
    );
};

export default ConverterCard;