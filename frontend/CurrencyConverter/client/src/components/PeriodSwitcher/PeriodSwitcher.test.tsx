import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { PeriodSwitcher } from '.';
import { PERIODS } from './periods';

describe('PeriodSwitcher', () => {
  it('рендерит 5 кнопок периодов 1-5 min', () => {
    render(<PeriodSwitcher period={PERIODS[2]} handlePeriodChange={() => {}} />);

    const buttons = screen.getAllByRole('button');
    expect(buttons).toHaveLength(PERIODS.length);

    for (const p of PERIODS) {
      expect(screen.getByText(`${p} min.`)).toBeInTheDocument();
    }
  });

  it('вызывает handlePeriodChange с новым периодом при клике', async () => {
    const onChange = vi.fn();
    render(<PeriodSwitcher period={PERIODS[2]} handlePeriodChange={onChange} />);

    await userEvent.click(screen.getByText('1 min.'));

    expect(onChange).toHaveBeenCalledWith(1);
  });
});
