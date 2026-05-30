import styles from './CurrencyInput.module.scss';

type CurrencyInputProps = {
  amount: string;
  currencyCode: string;
  currencies: string[];
}

export const CurrencyInput = ({ amount, currencyCode, currencies }: CurrencyInputProps) => {
  return (
    <div className={styles.row}>
        <input className={styles.amount} type="text" value={amount} />
        <hr className={styles.divider} />
        <select className={styles.select} value={currencyCode}>
            {currencies.map((currency, id) => (
                <option key={id} value={currency}>
                    {currency}
                </option>
            ))}
        </select>
    </div>
  );
};
