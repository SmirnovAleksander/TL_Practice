import ConverterHeader from "../ConverterHeader/ConverterHeader";
import MoreAboutGroup from "../MoreAboutGroup/MoreAboutGroup";
import CurrencyInputGroup from "../CurrencyInputGroup/CurrencyInputGroup";
import styles from './ConverterCard.module.scss';

const ConverterCard = () => {
    return (
        <div className={styles.card}>
            <ConverterHeader/>
            <CurrencyInputGroup/>
            <MoreAboutGroup/>
        </div>
    );
};

export default ConverterCard;