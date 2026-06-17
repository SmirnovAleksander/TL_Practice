import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MoreAboutGroup } from '.';
import { currencies } from '../../mocks';

const mockPLN = currencies.find(c => c.code === 'PLN') ?? null;
const mockJPY = currencies.find(c => c.code === 'JPY') ?? null;

describe('MoreAboutGroup', () => {
  it('отображает кнопку с правильным названием', () => {
    render(<MoreAboutGroup fromCurrency={mockPLN} toCurrency={mockJPY} />);

    const button = screen.getByTestId('more-about-button');

    expect(button).toBeInTheDocument();
    expect(button).toHaveTextContent('PLN/JPY: about');
  });

  it('отображает описания валют после клика по кнопке', async () => {
    render(<MoreAboutGroup fromCurrency={mockPLN} toCurrency={mockJPY} />);
    const button = screen.getByTestId('more-about-button');
    await userEvent.click(button);

    const fromBlock = screen.getByTestId('more-about-currency-from');
    const toBlock = screen.getByTestId('more-about-currency-to');

    expect(fromBlock).toHaveTextContent('Polish zloty - PLN - zł');
    expect(fromBlock).toHaveTextContent(/This is the official currency and legal tender of Poland./);
    expect(toBlock).toHaveTextContent('Japanese yen - JPY - ¥');
    expect(toBlock).toHaveTextContent(/The yen is the official currency of Japan/);
  });
});
