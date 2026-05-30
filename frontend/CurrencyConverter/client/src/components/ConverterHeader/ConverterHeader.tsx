import type { Currency } from '../../models';
import styles from './ConverterHeader.module.scss';

type ConverterHeaderProps = {
    fromCurrency: Currency;
    toCurrency: Currency;
    exchangeRate: Number;
    rateDate: string;
}

export const ConverterHeader = ({
    fromCurrency,
    toCurrency,
    exchangeRate,
    rateDate
}: ConverterHeaderProps) => {
    const prefix = `1 ${fromCurrency?.name ?? ''} is`;
    const rate = `${exchangeRate} ${toCurrency?.name ?? ''}`
    const updatedAt = rateDate;

    return (
        <div className={styles.summary}>
            <p className={styles.prefix}>{prefix}</p>
            <p className={styles.rate}>{rate}</p>
            <p className={styles.updatedAt}>{updatedAt}</p> 
        </div>
    );
};
