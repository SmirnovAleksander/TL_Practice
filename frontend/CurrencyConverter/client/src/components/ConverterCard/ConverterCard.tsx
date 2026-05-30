import { ConverterHeader } from "../ConverterHeader";
import { MoreAboutGroup } from "../MoreAboutGroup";
import { CurrencyInputGroup } from "../CurrencyInputGroup";
import styles from './ConverterCard.module.scss';

export const ConverterCard = () => {
    return (
        <div className={styles.card}>
            <ConverterHeader />
            <CurrencyInputGroup />
            <MoreAboutGroup />
        </div>
    );
};
