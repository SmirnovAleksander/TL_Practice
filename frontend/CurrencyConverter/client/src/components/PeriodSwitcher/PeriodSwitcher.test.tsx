import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { PeriodSwitcher } from '.';

describe('PeriodSwitcher', () => {
    it('рендерит 5 кнопок периодов 1-5 min', () => {
        render(<PeriodSwitcher period={3} handlePeriodChange={() => {}} />);

        const buttons = screen.getAllByRole('button');
        expect(buttons).toHaveLength(5);

        for (let i = 1; i <= 5; i++) {
            expect(screen.getByText(`${i} min.`)).toBeInTheDocument();
        }
    });

    it('вызывает handlePeriodChange с новым периодом при клике', async () => {
        const onChange = vi.fn();
        render(<PeriodSwitcher period={3} handlePeriodChange={onChange} />);

        await userEvent.click(screen.getByText('1 min.'));

        expect(onChange).toHaveBeenCalledWith(1);
    });
});
