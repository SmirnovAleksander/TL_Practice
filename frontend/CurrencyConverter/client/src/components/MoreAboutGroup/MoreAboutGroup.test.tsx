import { render, screen } from '@testing-library/react';
import { MoreAboutGroup } from '.';

describe('MoreAboutGroup', () => {
  it('render button with correct label', () => {
    render(<MoreAboutGroup />);
    const button = screen.getByTestId('more-about-button');
    expect(button).toBeInTheDocument();
    expect(button).toHaveTextContent('PLN/JPY: about');
  });

  it('renders descriptions for PLN and JPY', () => {
    render(<MoreAboutGroup />);
    const description = screen.getByTestId('more-about-currency-pln');
    expect(description).toBeInTheDocument();
    expect(description).toHaveTextContent('Polish zloty - PLN - zł');
    expect(description).toHaveTextContent(/This is the official currency and legal tender of Poland./);
  });

  it('renders JPY description text', () => {
    render(<MoreAboutGroup />);
    const description = screen.getByTestId('more-about-currency-jpy');
    expect(description).toBeInTheDocument();
    expect(description).toHaveTextContent('Japanese yen - JPY - ¥');
    expect(description).toHaveTextContent(/The yen is the official currency of Japan/);
  });
});
