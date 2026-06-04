import styles from './CurrencyInput.module.scss';

type CurrencyInputProps = {
    amount: string;
    currencyCode: string;
    currenciesCodes: string[];
    handleAmountChange?: (newAmount: string) => void;
    handelCurrencyChange: (newCode: string) => void;
}

export const CurrencyInput = ({
    amount,
    currencyCode,
    currenciesCodes,
    handleAmountChange,
    handelCurrencyChange
}: CurrencyInputProps) => {
  return (
    <div className={styles.row}>
        <input
            data-testid="currency-amount-input"
            className={styles.amount}
            type="text"
            value={amount}
            onChange={(e) => handleAmountChange(e.target.value)}
        />
        <hr className={styles.divider} />
        <select
            data-testid="currency-select"
            className={styles.select}
            value={currencyCode}
            onChange={(e) => handelCurrencyChange(e.target.value)}
        >
            {currenciesCodes.map((code, id) => (
                <option key={id} value={code}>
                    {code}
                </option>
            ))}
        </select>
    </div>
  );
};
