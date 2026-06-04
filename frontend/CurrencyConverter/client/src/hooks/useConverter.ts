import { useState } from "react";
import { currencies, priceChanges } from "../mocks";

export const useConverter = () => {
    const currenciesCodes = currencies.map(c => c.code);
    const defaultFrom = currenciesCodes[0];
    const defaultTo = currenciesCodes[1];
    const defaultRate = priceChanges[defaultFrom]?.[defaultTo]?.price;
    const defaultResult = defaultRate ? (1 * defaultRate).toFixed(2) : '0';

    const [from, setFrom] = useState(defaultFrom);
    const [to, setTo] = useState(defaultTo);
    const [amount, setAmount] = useState('1');
    const [result, setResult] = useState(defaultResult);

    const exchangeRate = priceChanges[from]?.[to]?.price;
    const rateDate = priceChanges[from]?.[to]?.dateTime;
    const fromCurrency = currencies.find(c => c.code === from);
    const toCurrency = currencies.find(c => c.code === to);

    const recalculateResult = (newFrom: string, newTo: string, newAmount: string) => {
        const currentRate = priceChanges[newFrom]?.[newTo]?.price;

        if (currentRate && newAmount && !isNaN(Number(newAmount))) {
            setResult((Number(newAmount) * currentRate).toFixed(2))
        } else {
            setResult('0');
        }
    };

    const findAlternativeCode = (newCode: string) =>
        currencies.find((c) => c.code !== newCode)?.code ?? newCode;

    const handleFromChange = (newCode: string) => {
        setFrom(newCode);

        if (newCode === to) {
            const altCode = findAlternativeCode(newCode);
            if (altCode) setTo(altCode);
            recalculateResult(newCode, altCode, amount);
        } else {
            recalculateResult(newCode, to, amount);
        }
    };

    const handleToChange = (newCode: string) => {
        setTo(newCode);

        if (newCode === from) {
            const altCode = findAlternativeCode(newCode);
            if (altCode) setFrom(altCode);
            recalculateResult(altCode, newCode, amount);
        } else {
            recalculateResult(from, newCode, amount);
        }
    };

    const handleAmountChange = (newAmount: string) => {
        setAmount(newAmount);
        recalculateResult(from, to, newAmount);
    };

    const handleSwap = () => {
        setFrom(to);
        setTo(from);
        setAmount(result);
        setResult(amount);
    }

    return {
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
    };
}