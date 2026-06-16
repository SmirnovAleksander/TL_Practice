import { render, screen } from '@testing-library/react';
import { CurrencyInput } from '.';

const mockCodes = ['CAD', 'PLN', 'AUD', 'JPY', 'ZAR'];

describe('CurrencyInput', () => {
  it('отображает селект с валютами из мок-данных', () => {
    render(
      <CurrencyInput
        amount="100"
        currencyCode="PLN"
        currenciesCodes={mockCodes}
        handelCurrencyChange={() => {}}
        isEditable={true}
      />
    );

    const select = screen.getByTestId('currency-select');

    expect(select).toBeInTheDocument();
    expect(select.children).toHaveLength(mockCodes.length);
    mockCodes.forEach(code => {
      expect(screen.getByText(code)).toBeInTheDocument();
    });
  });
});
