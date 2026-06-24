import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { ConverterCard } from '.';
import { currencies, priceChanges } from '../../mocks';

const mockFetchCurrencies = vi.hoisted(() => vi.fn());
const mockFetchPriceChanges = vi.hoisted(() => vi.fn());

vi.mock('../../api', () => ({
    fetchCurrencies: mockFetchCurrencies,
    fetchPriceChanges: mockFetchPriceChanges,
}));

describe('ConverterCard', () => {
    beforeEach(() => {
        mockFetchCurrencies.mockResolvedValue(currencies);
        mockFetchPriceChanges.mockImplementation(
            (paymentCurrency: string, purchasedCurrency: string) => {
                const change = priceChanges[purchasedCurrency]?.[paymentCurrency];
                return Promise.resolve(change ? [change] : []);
            }
        );
    });

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

        expect((inputs[1] as HTMLInputElement).value).toBe('3.40');
    });

    it('пересчитывает результат при смене пары валют', async () => {
        render(<ConverterCard />);

        const selects = await screen.findAllByRole('combobox');
        const inputs = await screen.findAllByRole('textbox');

        await userEvent.selectOptions(selects[1], 'JPY');

        expect((inputs[1] as HTMLInputElement).value).toBe('0.01');
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
