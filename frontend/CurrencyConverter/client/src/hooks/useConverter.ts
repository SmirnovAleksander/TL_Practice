import { useEffect, useMemo, useState } from "react";
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
    const [period, setPeriod] = useState(3);

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

        pricesDispatch({ type: 'LOADING' });

        const fetchData = async () => {
            try {
                const fromDateTime = new Date(Date.now() - period * 60 * 1000).toISOString()

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

        fetchData();
        const interval = setInterval(fetchData, 10000);

        return () => {
            clearInterval(interval);
        };

    }, [ from, to, period, pricesDispatch ])

    const latestPriceChange = pricesState.data?.[pricesState.data.length - 1];
    const exchangeRate = latestPriceChange?.price ?? 0;
    const rateDate = latestPriceChange?.dateTime ?? '';

    const fromCurrency = currenciesState.data?.find(c => c.code === from);
    const toCurrency = currenciesState.data?.find(c => c.code === to);

    const result = useMemo(() => {
        if (exchangeRate && amount && !isNaN(Number(amount))) {
            return (Number(amount) * exchangeRate).toFixed(2);
        }
        return '0';
    }, [amount, exchangeRate]);

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
    }

    const handlePeriodChange = (newPeriod: number) => {
        setPeriod(newPeriod);
    };

    return {
        from,
        to,
        amount,
        result,
        exchangeRate,
        rateDate,
        period,
        fromCurrency,
        toCurrency,
        currenciesCodes,
        currenciesError: currenciesState.error,
        currenciesLoading: currenciesState.isLoading,
        pricesState,
        handleFromChange,
        handleToChange,
        handleAmountChange,
        handleSwap,
        handlePeriodChange
    };
}