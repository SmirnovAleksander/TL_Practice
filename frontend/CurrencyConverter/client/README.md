# Currency Converter

## Запуск

```bash
npm i
npm run dev      # dev-сервер
npm run test     # тесты
npm run lint     # линтер
```

etchPriceChanges` → `SUCCESS` → `pricesState.data` → `PriceGraph` перерисовывается.

## Reset по key

В `ConverterCard` у `MoreAboutGroup` стоит `key={${from}-${to}}`.  
Так как у нас меняется key при смене пары валют, то react удаляет старыт и создает новый, то есть пересоздание компонента и в
новом компоненте стоит по умолчанию isOpen false поэтому more about информация будет закрыта при смене валют

## Интервал

10 секунд. `setInterval(fetchData, 10000)`.

## useEffect

- **Currencies** — загрузка валют при монтировании.
- **Prices** — загрузка цен при смене from/to/period.
- **Result** — `useMemo` пересчёт при изменении amount/exchangeRate.

## Тесты

#### CurrencyInput
- [x] отображает селект с валютами из мок-данных

#### MoreAboutGroup
- [x] отображает кнопку с правильным названием
- [x] отображает описания валют после клика по кнопке

#### ConverterCard
- [x] показывает ошибку если сервер недоступен
- [x] показывает loading при загрузке валют
- [x] показывает конвертер после успешной загрузки
- [x] пересчитывает результат при изменении суммы
- [x] пересчитывает результат при смене пары валют
- [x] запрещает выбор одинаковой валюты в обеих селектах

#### PeriodSwitcher
- [x] рендерит 5 кнопок периодов 1-5 min
- [x] вызывает handlePeriodChange с новым периодом при клике

#### PriceGraph
- [x] показывает loader при isLoading=true и отсутствии данных
- [x] показывает ошибку при error и отсутствии данных
- [x] показывает нет данных при пустом массиве
- [x] рендерит SVG с данными

#### Mappers
- [x] маппит все поля CurrencyDto в Currency
- [x] маппит все поля PriceChangeDto в PriceChange

#### useDataReducer
- [x] проверка initialState
- [x] переходит в состояние загрузки при dispatch LOADING
- [x] записывает данные при dispatch SUCCESS
- [x] записывает ошибку при dispatch ERROR, сохраняя data
- [x] сохраняет data при повторном LOADING (автообновление)