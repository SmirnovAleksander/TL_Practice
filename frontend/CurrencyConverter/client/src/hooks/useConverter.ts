import { useEffect, useState } from "react";
import { useDataReducer } from "./useDataReducer";
import type { Currency, PriceChange } from "../models";
import { fetchCurrencies, fetchPriceChanges } from "../api";
import { mapCurrencyDtoToCurrency, mapPriceChangeDtoToPriceChange } from "../mappers";

export const useConverter = () => {
    const { state: currenciesState, dispatch: currenciesDispatch } = useDataReducer<Currency[]>();
    const { state: pricesState, dispatch: pricesDispatch } = useDataReducer<PriceChange[]>();

    const [from, setFrom] = useState('');
    const [to, setTo] = useState('');
    const [amount, setAmount] = useState('1');
    const [result, setResult] = useState('');

    useEffect(() => {
        const load = async () => {
            currenciesDispatch({ type: 'LOADING' });

            try {
                const dtos = await fetchCurrencies();
                const currencies = dtos.map(mapCurrencyDtoToCurrency);

                currenciesDispatch({
                    type: "SUCCESS",
                    payload: currencies,
                });

                if (currencies.length >= 2) {
                    setFrom(currencies[0].code);
                    setTo(currencies[1].code);
                }
            } catch (e) {
                currenciesDispatch({
                    type: 'ERROR',
                    payload: (e as Error).message
                });
            }
        };

        load();
    }, [currenciesDispatch])

    const currenciesCodes = currenciesState.data?.map(c => c.code) ?? [];

    useEffect(() => {
        if (!from || !to) return;

        const load = async () => {
            pricesDispatch({ type: 'LOADING' });

            const oneMinuteMs = 60000;
            const fromDateTime = new Date(Date.now() - oneMinuteMs).toISOString();

            try {
                const dtos = await fetchPriceChanges(from, to, fromDateTime);
                pricesDispatch({
                    type: "SUCCESS",
                    payload: dtos.map(mapPriceChangeDtoToPriceChange),
                });
            } catch (e) {
                pricesDispatch({
                    type: 'ERROR',
                    payload: (e as Error).message
                });
            }
        };

        load();
    }, [ from, to, pricesDispatch ])

    const latestPriceChange = pricesState.data?.[pricesState.data.length - 1];
    const exchangeRate = latestPriceChange?.price ?? 0;
    const rateDate = latestPriceChange?.dateTime ?? '';

    const fromCurrency = currenciesState.data?.find(c => c.code === from);
    const toCurrency = currenciesState.data?.find(c => c.code === to);

    useEffect(() => {
        const recalculateResult = (newAmount: string) => {
            if (exchangeRate && newAmount && !isNaN(Number(newAmount))) {
                setResult((Number(newAmount) * exchangeRate).toFixed(2))
            } else {
                setResult('0');
            }
        };

        recalculateResult( amount );
    }, [ amount, exchangeRate ])

    const findAlternativeCode = (newCode: string) =>
        currenciesState.data?.find((c) => c.code !== newCode)?.code ?? newCode;

    const handleFromChange = (newCode: string) => {
        setFrom(newCode);

        if (newCode === to) {
            const altCode = findAlternativeCode(newCode);
            if (altCode) setTo(altCode);
        }
    };

    const handleToChange = (newCode: string) => {
        setTo(newCode);

        if (newCode === from) {
            const altCode = findAlternativeCode(newCode);
            if (altCode) setFrom(altCode);
        }
    };

    const handleAmountChange = (newAmount: string) => {
        setAmount(newAmount);
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
        currenciesError: currenciesState.error,
        currenciesLoading: currenciesState.isLoading,
        pricesError: pricesState.error,
        pricesLoading: pricesState.isLoading,
        handleFromChange,
        handleToChange,
        handleAmountChange,
        handleSwap
    };
}