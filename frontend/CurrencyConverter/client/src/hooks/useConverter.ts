import { useState } from "react";
import { priceChanges } from "../mocks";


export const useConverter = () => {
    const [from, setFrom] = useState<string>('PLN');
    const [to, setTo] = useState<string>('JPY');
    const [amount, setAmount] = useState<string>('1');
    const [result, setResult] = useState<string>('0.99');
    const [isMoreAboutOpen, setIsMoreAboutOpen] = useState<boolean>(false);

    const recalculateResult = (newFrom: string, newTo: string, newAmount: string) => {
        const currentRate = priceChanges[newFrom]?.[newTo]?.price;

        if (currentRate && newAmount) {
            setResult((Number(newAmount) * currentRate).toString())
        } else {
            setResult('0');
        }
    };

    const handleFromChange = (newCode: string) => {
        setFrom(newCode);
        recalculateResult(newCode, to, amount);
    };

    const handleToChange = (newCode: string) => {
        setTo(newCode);
        recalculateResult(from, newCode, amount);
    };

    const handleAmountChange = (newAmount: string) => {
        setAmount(newAmount);
        recalculateResult(from, to, newAmount);
    };

    const handleToggleMoreAbout = () => {
        setIsMoreAboutOpen(prev => !prev);
    };

    return {
        from,
        to,
        amount,
        result,
        isMoreAboutOpen,
        handleFromChange,
        handleToChange,
        handleAmountChange,
        handleToggleMoreAbout
    };
}