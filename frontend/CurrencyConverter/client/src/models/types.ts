export type Currency = {
  code: string;
  description: string;
  name: string;
  symbol: string;
};

export type PriceChange = {
  purchasedCurrencyCode: string;
  paymentCurrencyCode: string;
  price: number;
  dateTime: string;
};
