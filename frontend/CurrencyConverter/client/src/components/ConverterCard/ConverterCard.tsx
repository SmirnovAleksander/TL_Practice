import { ConverterHeader } from "../ConverterHeader";
import { MoreAboutGroup } from "../MoreAboutGroup";
import { CurrencyInputGroup } from "../CurrencyInputGroup";
import styles from './ConverterCard.module.scss';
import { useConverter } from "../../hooks/useConverter";

export const ConverterCard = () => {
    const {
        from,
        to,
        amount,
        result,
        isMoreAboutOpen,
        exchangeRate,
        rateDate,
        fromCurrency,
        toCurrency,
        currenciesCodes,
        handleFromChange,
        handleToChange,
        handleAmountChange,
        handleToggleMoreAbout
    } = useConverter();
    return (
        <div className={styles.card}>
            <ConverterHeader
                exchangeRate={exchangeRate}
                fromCurrency={fromCurrency}
                toCurrency={toCurrency}
                rateDate={rateDate}
            />
            <CurrencyInputGroup

            />
            <MoreAboutGroup
                fromCurrency={fromCurrency}
                toCurrency={toCurrency}
                isOpen={isMoreAboutOpen}
                handleToggleMoreAbout={handleToggleMoreAbout}
            />
        </div>
    );
};
