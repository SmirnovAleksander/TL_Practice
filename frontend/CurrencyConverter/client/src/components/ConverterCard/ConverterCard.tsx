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
        exchangeRate,
        rateDate,
        fromCurrency,
        toCurrency,
        currenciesCodes,
        handleFromChange,
        handleToChange,
        handleAmountChange,
        handleSwap
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
                from={from}
                to={to}
                amount={amount}
                result={result}
                currenciesCodes={currenciesCodes}
                handleAmountChange={handleAmountChange}
                handleFromChange={handleFromChange}
                handleToChange={handleToChange}
                handleSwap={handleSwap}
            />
            {/* так как у нас меняется key при смене пары валют, то react удаляет старыт и создает новый, то есть пересоздание компонента и в 
            новом компоненте стоит по умолчанию isOpen false поэтому more about информация будет закрыта при смене валют */}
            <MoreAboutGroup
                key={`${from}-${to}`}
                fromCurrency={fromCurrency}
                toCurrency={toCurrency}
            />
        </div>
    );
};
