import { CurrencyInput } from "../CurrencyInput/CurrencyInput";
import styles from './CurrencyInputGroup.module.scss';

const currencies = ['CAD', 'PLN', 'AUD', 'JPY', 'ZAR'];
const from = { amount: '1', code: 'PLN' };
const to = { amount: '0,99', code: 'JPY' };

export const CurrencyInputGroup = () => {
    return (
        <div className={styles.group}>
            <CurrencyInput amount={from.amount} currencyCode={from.code} currencies={currencies}/>
            <CurrencyInput amount={to.amount} currencyCode={to.code} currencies={currencies}/>
        </div>
    );
};
