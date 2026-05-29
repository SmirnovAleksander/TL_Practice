import { ConverterHeader } from "../ConverterHeader/ConverterHeader";
import { MoreAboutGroup } from "../MoreAboutGroup/MoreAboutGroup";
import { CurrencyInputGroup } from "../CurrencyInputGroup/CurrencyInputGroup";
import styles from './ConverterCard.module.scss';

export const ConverterCard = () => {
    return (
        <div className={styles.card}>
            <ConverterHeader/>
            <CurrencyInputGroup/>
            <MoreAboutGroup/>
        </div>
    );
};
