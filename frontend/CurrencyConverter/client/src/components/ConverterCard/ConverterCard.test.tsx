import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ConverterCard } from '.';

describe('ConverterCard', () => {
  it('пересчитывает результат при изменении суммы', async () => {
    render(<ConverterCard />);

    const inputs = screen.getAllByTestId('currency-amount-input');
    await userEvent.clear(inputs[0]);
    await userEvent.type(inputs[0], '10');

    expect(Number((inputs[1] as HTMLInputElement).value)).toBeGreaterThan(0);
  });

  it('пересчитывает результат при смене пары валют', async () => {
    render(<ConverterCard />);

    const resultInput = screen.getAllByTestId('currency-amount-input')[1] as HTMLInputElement;
    const before = resultInput.value;

    const toSelect = screen.getAllByTestId('currency-select')[1];
    await userEvent.selectOptions(toSelect, 'JPY');

    expect(resultInput.value).not.toBe(before);
  });

  it('запрещает выбор одинаковой валюты в обеих селектах', async () => {
    render(<ConverterCard />);

    const selects = screen.getAllByTestId('currency-select');
    const fromSelect = selects[0];
    const toSelect = selects[1];
    const toValue = (toSelect as HTMLSelectElement).value;

    await userEvent.selectOptions(fromSelect, toValue);

    expect(fromSelect).toHaveValue(toValue);
    expect(toSelect).not.toHaveValue(toValue);
  });

  it('сбрасывает открытое состояние MoreAboutGroup при смене пары и key', async () => {
    render(<ConverterCard />);

    await userEvent.click(screen.getByTestId('more-about-button'));
    expect(screen.getByTestId('more-about-currency-from')).toBeInTheDocument();

    const fromSelect = screen.getAllByTestId('currency-select')[0];
    await userEvent.selectOptions(fromSelect, 'AUD');

    expect(screen.queryByTestId('more-about-currency-from')).not.toBeInTheDocument();
  });

});
