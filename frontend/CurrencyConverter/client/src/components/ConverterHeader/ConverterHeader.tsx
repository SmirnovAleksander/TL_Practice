import type { Currency } from '../../models';
import styles from './ConverterHeader.module.scss';

type ConverterHeaderProps = {
  fromCurrency: Currency | undefined;
  toCurrency: Currency | undefined;
  exchangeRate: number;
  rateDate: string;
};

export const ConverterHeader = ({ fromCurrency, toCurrency, exchangeRate, rateDate }: ConverterHeaderProps) => {
  const formatDate = (newDate: string) => {
    const date = new Date(newDate);
    return date.toUTCString().replace(/:\d{2} GMT$/, ' UTC');
  };

  const prefix = `1 ${fromCurrency?.name ?? ''} is`;
  const rate = `${exchangeRate ?? ''} ${toCurrency?.code ?? ''}`;
  const updatedAt = rateDate ? formatDate(rateDate) : '';

  return (
    <div className={styles.summary}>
      <p className={styles.prefix}>{prefix}</p>
      <p className={styles.rate}>{rate}</p>
      <p className={styles.updatedAt}>{updatedAt}</p>
    </div>
  );
};
