import { render, screen } from '@testing-library/react';
import { PriceGraph } from '.';

const mockData = [
  { purchasedCurrencyCode: 'PLN', paymentCurrencyCode: 'CAD', price: 0.34, dateTime: '2026-04-27T09:20:00.000Z' },
  { purchasedCurrencyCode: 'PLN', paymentCurrencyCode: 'CAD', price: 0.35, dateTime: '2026-04-27T09:21:00.000Z' }
];

describe('PriceGraph', () => {
  it('показывает loader при isLoading=true и отсутствии данных', () => {
    render(<PriceGraph data={[]} isLoading={true} error={null} />);
    expect(screen.getByText('Loading chart...')).toBeInTheDocument();
  });

  it('показывает ошибку при error и отсутствии данных', () => {
    render(<PriceGraph data={[]} isLoading={false} error="Server error" />);
    expect(screen.getByText('Server error')).toBeInTheDocument();
  });

  it('показывает нет данных при пустом массиве', () => {
    render(<PriceGraph data={[]} isLoading={false} error={null} />);
    expect(screen.getByText('No data for selected period.')).toBeInTheDocument();
  });

  it('рендерит SVG с данными', () => {
    render(<PriceGraph data={mockData} isLoading={false} error={null} />);
    expect(document.querySelector('svg')).toBeInTheDocument();
  });
});
