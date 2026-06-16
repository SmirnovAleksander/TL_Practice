# Currency Converter

## Запуск
```bash
npm i
npm run dev      # dev-сервер
npm run test     # тесты
npm run lint     # линтер
```

## Мок-данные

Мок данные находятся в папке /mocks, и они используются в хуке `useConverter` и в тестах

## Reset по key

В `ConverterCard` у `MoreAboutGroup` стоит `key={${from}-${to}}`.  
Так как у нас меняется key при смене пары валют, то react удаляет старыт и создает новый, то есть пересоздание компонента и в
новом компоненте стоит по умолчанию isOpen false поэтому more about информация будет закрыта при смене валют
