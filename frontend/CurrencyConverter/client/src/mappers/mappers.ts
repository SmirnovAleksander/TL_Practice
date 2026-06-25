import type { Currency, PriceChange } from '../models';
import type { CurrencyDto, PriceChangeDto } from '../models/dtos';

export const mapCurrencyDtoToCurrency = (dto: CurrencyDto): Currency => ({
  code: dto.code,
  name: dto.name,
  description: dto.description,
  symbol: dto.symbol
});

export const mapPriceChangeDtoToPriceChange = (dto: PriceChangeDto): PriceChange => ({
  purchasedCurrencyCode: dto.purchasedCurrencyCode,
  paymentCurrencyCode: dto.paymentCurrencyCode,
  price: dto.price,
  dateTime: dto.dateTime
});
