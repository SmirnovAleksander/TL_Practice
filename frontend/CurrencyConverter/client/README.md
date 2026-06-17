# Currency Converter

## Запуск

```bash
npm i
npm run dev      # dev-сервер
npm run test     # тесты
npm run lint     # линтер
```

etchPriceChanges` → `SUCCESS` → `pricesState.data` → `PriceGraph` перерисовывается.

## Интервал

10 секунд. `setInterval(fetchData, 10000)`.

## useEffect

- **Currencies** — загрузка валют при монтировании.
- **Prices** — загрузка цен при смене from/to/period.
- **Result** — `useMemo` пересчёт при изменении amount/exchangeRate.
