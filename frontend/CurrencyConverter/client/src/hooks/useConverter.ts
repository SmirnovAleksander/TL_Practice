import { useEffect, useState } from "react";
import { useDataReducer } from "./useDataReducer";
import type { Currency, PriceChange } from "../models";
import { fetchCurrencies, fetchPriceChanges } from "../api";
import { mapCurrencyDtoToCurrency, mapPriceChangeDtoToPriceChange } from "../mappers";

const ONE_MINUTE_MS = 60000;

export const useConverter = () => {
    const { state: currenciesState, dispatch: currenciesDispatch } = useDataReducer<Currency[]>();
    const { state: pricesState, dispatch: pricesDispatch } = useDataReducer<PriceChange[]>();

    const [from, setFrom] = useState<Currency | undefined>(undefined);
    const [to, setTo] = useState<Currency | undefined>(undefined);
    const [amount, setAmount] = useState('1');
    const [result, setResult] = useState('');

    useEffect(() => {
        const abortController = new AbortController();

        const load = async () => {
            currenciesDispatch({ type: 'LOADING' });

            try {
                const dtos = await fetchCurrencies(abortController.signal);
                const currencies = dtos.map(mapCurrencyDtoToCurrency);

                currenciesDispatch({
                    type: "SUCCESS",
                    payload: currencies,
                });

                if (currencies.length >= 2) {
                    setFrom(currencies[0]);
                    setTo(currencies[1]);
                }
            } catch (e) {
                if (abortController.signal.aborted) return;

                currenciesDispatch({
                    type: 'ERROR',
                    payload: (e as Error).message || 'Unknown error'
                });
            }
        };

        load();

        return () => {
            abortController.abort();
        };
    }, [currenciesDispatch])

    const currenciesCodes = currenciesState.data?.map(c => c.code) ?? [];

    useEffect(() => {
        if (!from || !to) return;

        const abortController = new AbortController();

        const load = async () => {
            pricesDispatch({ type: 'LOADING' });

            const fromDateTime = new Date(Date.now() - ONE_MINUTE_MS).toISOString();

            try {
                const dtos = await fetchPriceChanges(from.code, to.code, fromDateTime, abortController.signal);
                pricesDispatch({
                    type: "SUCCESS",
                    payload: dtos.map(mapPriceChangeDtoToPriceChange),
                });
            } catch (e) {
                if (abortController.signal.aborted) return;

                pricesDispatch({
                    type: 'ERROR',
                    payload: (e as Error).message || 'Unknown error'
                });
            }
        };

        load();

        return () => {
            abortController.abort();
        };
    }, [ from, to, pricesDispatch ])

    const latestPriceChange = pricesState.data?.[pricesState.data.length - 1];
    const exchangeRate = latestPriceChange?.price ?? 0;
    const rateDate = latestPriceChange?.dateTime ?? '';

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

    const findAlternativeCurrency = (newCode: string) =>
        currenciesState.data?.find((c) => c.code !== newCode);

    const handleFromChange = (newCode: string) => {
        const newCurrency = currenciesState.data?.find((c) => c.code === newCode);
        if (!newCurrency) return;

        setFrom(newCurrency);

        if (newCurrency.code === to?.code) {
            const alt = findAlternativeCurrency(newCurrency.code);
            if (alt) setTo(alt);
        }
    };

    const handleToChange = (newCode: string) => {
        const newCurrency = currenciesState.data?.find((c) => c.code === newCode);
        if (!newCurrency) return;

        setTo(newCurrency);

        if (newCurrency.code === from?.code) {
            const alt = findAlternativeCurrency(newCurrency.code);
            if (alt) setFrom(alt);
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
        from: from?.code ?? '',
        to: to?.code ?? '',
        amount,
        result,
        exchangeRate,
        rateDate,
        fromCurrency: from,
        toCurrency: to,
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