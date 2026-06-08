import type { Currency, PriceChange } from '../models';
import currenciesData from './currencies.json';
import priceChangesData from './priceChanges.json';

export const currencies: Currency[] = currenciesData;
export const priceChanges: Record<string, Record<string, PriceChange>> = priceChangesData;
