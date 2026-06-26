import { useEffect, useMemo, useState } from 'react';
import { useDataReducer } from './useDataReducer';
import type { Currency, PriceChange } from '../models';
import { fetchCurrencies, fetchPriceChanges } from '../api';
import { mapCurrencyDtoToCurrency, mapPriceChangeDtoToPriceChange } from '../mappers';

const DEFAULT_PERIOD = 3;
const MS_IN_MINUTE = 60000;
const POLL_INTERVAL_MS = 10000;

export const useConverter = () => {
  const { state: currenciesState, dispatch: currenciesDispatch } = useDataReducer<Currency[]>();
  const { state: pricesState, dispatch: pricesDispatch } = useDataReducer<PriceChange[]>();

  const [from, setFrom] = useState<Currency | undefined>(undefined);
  const [to, setTo] = useState<Currency | undefined>(undefined);
  const [amount, setAmount] = useState('1');
  const [period, setPeriod] = useState(DEFAULT_PERIOD);

  useEffect(() => {
    const abortController = new AbortController();

    const load = async () => {
      currenciesDispatch({ type: 'LOADING' });

      try {
        const dtos = await fetchCurrencies(abortController.signal);
        const currencies = dtos.map(mapCurrencyDtoToCurrency);

        currenciesDispatch({
          type: 'SUCCESS',
          payload: currencies
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
  }, [currenciesDispatch]);

  const currenciesCodes = currenciesState.data?.map((c) => c.code) ?? [];

  useEffect(() => {
    if (!from || !to) return;

    pricesDispatch({ type: 'LOADING' });

    const abortController = new AbortController();

    const fetchData = async () => {
      try {
        const fromDateTime = new Date(Date.now() - period * MS_IN_MINUTE).toISOString();

        const dtos = await fetchPriceChanges(from.code, to.code, fromDateTime, abortController.signal);

        if (abortController.signal.aborted) return;

        pricesDispatch({
          type: 'SUCCESS',
          payload: dtos.map(mapPriceChangeDtoToPriceChange)
        });
      } catch (e) {
        if (abortController.signal.aborted) return;
        pricesDispatch({
          type: 'ERROR',
          payload: (e as Error).message || 'Unknown error'
        });
      }
    };

    fetchData();
    const interval = setInterval(fetchData, POLL_INTERVAL_MS);

    return () => {
      clearInterval(interval);
      abortController.abort();
    };
  }, [from, to, period, pricesDispatch]);

  const latestPriceChange = pricesState.data?.[pricesState.data.length - 1];
  const exchangeRate = latestPriceChange?.price ?? 0;
  const rateDate = latestPriceChange?.dateTime ?? '';

  const result = useMemo(() => {
    if (exchangeRate && amount && !isNaN(Number(amount))) {
      return (Number(amount) * exchangeRate).toFixed(2);
    }
    return '0';
  }, [amount, exchangeRate]);

  const findAlternativeCurrency = (newCode: string) => currenciesState.data?.find((c) => c.code !== newCode);

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
  };

  const handlePeriodChange = (newPeriod: number) => {
    setPeriod(newPeriod);
  };

  return {
    from: from?.code ?? '',
    to: to?.code ?? '',
    amount,
    result,
    exchangeRate,
    rateDate,
    period,
    fromCurrency: from,
    toCurrency: to,
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
};
