import { mapCurrencyDtoToCurrency, mapPriceChangeDtoToPriceChange } from './mappers';
import { describe, it, expect } from 'vitest';

describe('mapCurrencyDtoToCurrency', () => {
  it('маппит все поля CurrencyDto в Currency', () => {
    const dto = {
      code: 'USD',
      name: 'Dollar',
      description: 'description',
      symbol: '$'
    };

    const result = mapCurrencyDtoToCurrency(dto);

    expect(result).toEqual({
      code: 'USD',
      name: 'Dollar',
      description: 'description',
      symbol: '$'
    });
  });
});

describe('mapPriceChangeDtoToPriceChange', () => {
  it('маппит все поля PriceChangeDto в PriceChange', () => {
    const dto = {
      purchasedCurrencyCode: 'JPY',
      paymentCurrencyCode: 'CAD',
      price: 1.0,
      dateTime: '2026-05-21'
    };

    const result = mapPriceChangeDtoToPriceChange(dto);

    expect(result).toEqual({
      purchasedCurrencyCode: 'JPY',
      paymentCurrencyCode: 'CAD',
      price: 1.0,
      dateTime: '2026-05-21'
    });
  });
});
