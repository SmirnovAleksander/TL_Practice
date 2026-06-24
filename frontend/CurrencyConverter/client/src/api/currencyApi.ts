import axios from "axios";
import type { CurrencyDto, PriceChangeDto } from "../models";

const api = axios.create({
    baseURL: 'http://localhost:5081'
})

export const fetchCurrencies = async (signal?: AbortSignal): Promise<CurrencyDto[]> => {
    const { data } = await api.get('/Currency', { signal });
    return data;
}

export const fetchPriceChanges = async (
    paymentCurrency: string,
    purchasedCurrency: string,
    fromDateTime: string,
    signal?: AbortSignal
): Promise<PriceChangeDto[]> => {
    const { data } = await api.get('/prices', {
        params: {
            paymentCurrency,
            purchasedCurrency,
            fromDateTime
        },
        signal,
    })

    return data;
}