import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ConverterCard } from '.';

const mockFetchCurrencies = vi.hoisted(() => vi.fn().mockResolvedValue([
    { code: 'CAD', name: 'Canadian dollar', description: 'desc', symbol: '$' },
    { code: 'PLN', name: 'Polish zloty', description: 'desc', symbol: 'zł' },
    { code: 'JPY', name: 'Japanese yen', description: 'desc', symbol: '¥' },
    { code: 'AUD', name: 'Australian dollar', description: 'desc', symbol: '$' },
    { code: 'ZAR', name: 'South African rand', description: 'desc', symbol: 'R' },
]));

const mockFetchPriceChanges = vi.hoisted(() => vi.fn().mockResolvedValue([
    { purchasedCurrencyCode: 'PLN', paymentCurrencyCode: 'CAD', price: 0.34, dateTime: '2026-04-27T09:20:00.000Z' },
    { purchasedCurrencyCode: 'JPY', paymentCurrencyCode: 'CAD', price: 0.0094, dateTime: '2026-04-27T10:00:00.000Z' },
    { purchasedCurrencyCode: 'AUD', paymentCurrencyCode: 'CAD', price: 0.9, dateTime: '2026-04-27T09:40:00.000Z' },
]));

vi.mock('../../api', () => ({
    fetchCurrencies: mockFetchCurrencies,
    fetchPriceChanges: mockFetchPriceChanges,
}));

describe('ConverterCard', () => {
    it('показывает ошибку если сервер недоступен', async () => {
        mockFetchCurrencies.mockRejectedValueOnce(new Error('Error'));

        render(<ConverterCard />);

        expect(await screen.findByText('Server is not available.')).toBeInTheDocument();
    });

    it('показывает loading при загрузке валют', async () => {
        mockFetchCurrencies.mockImplementationOnce(() => new Promise(() => {}));

        render(<ConverterCard />);

        expect(await screen.findByText('Loading ...')).toBeInTheDocument();
    });

    it('показывает конвертер после успешной загрузки', async () => {
        render(<ConverterCard />);

        const inputs = await screen.findAllByRole('textbox');
        expect(inputs).toHaveLength(2);
    });

    it('пересчитывает результат при изменении суммы', async () => {
        render(<ConverterCard />);

        const inputs = await screen.findAllByRole('textbox');
        await userEvent.clear(inputs[0]);
        await userEvent.type(inputs[0], '10');

        expect(Number((inputs[1] as HTMLInputElement).value)).toBeGreaterThan(0);
    });

    it('запрещает выбор одинаковой валюты в обеих селектах', async () => {
        render(<ConverterCard />);

        const selects = await screen.findAllByRole('combobox');

        const toValue = (selects[1] as HTMLSelectElement).value;

        await userEvent.selectOptions(selects[0], toValue);

        expect(selects[0]).toHaveValue(toValue);
        expect(selects[1]).not.toHaveValue(toValue);
    });
});
