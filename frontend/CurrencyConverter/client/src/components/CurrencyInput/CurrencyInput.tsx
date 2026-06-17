import styles from './CurrencyInput.module.scss';

type CurrencyInputProps = {
    amount: string;
    currencyCode: string;
    currenciesCodes: string[];
    handleAmountChange?: (newAmount: string) => void;
    handelCurrencyChange: (newCode: string) => void;
    isEditable: boolean;
}

export const CurrencyInput = ({
    amount,
    currencyCode,
    currenciesCodes,
    handleAmountChange,
    handelCurrencyChange,
    isEditable
}: CurrencyInputProps) => {
    
    return (
        <div className={styles.row}>
            <input
                data-testid="currency-amount-input"
                className={styles.amount}
                type="text"
                value={amount}
                onChange={(e) => handleAmountChange?.(e.target.value)}
                disabled={!isEditable}
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
