import { useState } from "react";
import { currencies, priceChanges } from "../mocks";


export const useConverter = () => {
    const [from, setFrom] = useState<string>('PLN');
    const [to, setTo] = useState<string>('JPY');
    const [amount, setAmount] = useState<string>('1');
    const [result, setResult] = useState<string>('0.99');
    const [isMoreAboutOpen, setIsMoreAboutOpen] = useState<boolean>(true);

    const exchangeRate = priceChanges[from]?.[to]?.price;
    const rateDate = priceChanges[from]?.[to]?.dateTime;
    const fromCurrency = currencies.find(c => c.code === from);
    const toCurrency = currencies.find(c => c.code === to);
    const currenciesCodes = currencies.map(c => c.code);

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
            const next = findAlternativeCode(newCode);
            if (next) setTo(next);
            recalculateResult(newCode, next, amount);
        } else {
            recalculateResult(newCode, to, amount);
        }
    };

    const handleToChange = (newCode: string) => {
        setTo(newCode);

        if (newCode === from) {
            const next = findAlternativeCode(newCode);
            if (next) setFrom(next);
            recalculateResult(next, newCode, amount);
        } else {
            recalculateResult(from, newCode, amount);
        }
    };

    const handleAmountChange = (newAmount: string) => {
        setAmount(newAmount);
        recalculateResult(from, to, newAmount);
    };

    const handleToggleMoreAbout = () => {
        setIsMoreAboutOpen(prev => !prev);
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
        isMoreAboutOpen,
        exchangeRate,
        rateDate,
        fromCurrency,
        toCurrency,
        currenciesCodes,
        handleFromChange,
        handleToChange,
        handleAmountChange,
        handleToggleMoreAbout,
        handleSwap
    };
}