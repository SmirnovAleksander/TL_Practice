import { CurrencyInput } from "../CurrencyInput";
import styles from './CurrencyInputGroup.module.scss';
import swapIcon from '../../assets/swap-arrow.svg';

type CurrencyInputGroupProps = {
    from: string;
    to: string;
    amount: string;
    result: string;
    currenciesCodes: string[];
    handleFromChange: (newCode: string) => void;
    handleToChange: (newCode: string) => void;
    handleAmountChange: (newAmount: string) => void;
    handleSwap: () => void;
}

export const CurrencyInputGroup = ({
    from,
    to,
    amount,
    result,
    currenciesCodes,
    handleFromChange,
    handleToChange,
    handleAmountChange,
    handleSwap
}: CurrencyInputGroupProps) => {
    return (
        <div className={styles.row}>
            <button type="button" className={styles.swapButton} onClick={handleSwap}>
                <img src={swapIcon} alt="" />
            </button>
            <div className={styles.group}>
                <CurrencyInput
                    amount={amount}
                    currencyCode={from}
                    currenciesCodes={currenciesCodes}
                    handelCurrencyChange={handleFromChange}
                    handleAmountChange={handleAmountChange}
                />
                <CurrencyInput
                    amount={result}
                    currencyCode={to}
                    currenciesCodes={currenciesCodes}
                    handelCurrencyChange={handleToChange}
                    handleAmountChange={handleAmountChange}
                />
            </div>
        </div>
    );
};
