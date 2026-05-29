import { render, screen } from '@testing-library/react';
import { MoreAboutGroup } from './MoreAboutGroup';

describe('MoreAboutGroup', () => {
  it('render button with correct label', () => {
    render(<MoreAboutGroup />);
    expect(screen.getByText('PLN/JPY: about')).toBeInTheDocument();
  });

  it('renders descriptions for PLN and JPY', () => {
    render(<MoreAboutGroup />);
    expect(screen.getByText('Polish zloty - PLN - zł')).toBeInTheDocument();
    expect(screen.getByText('Japanese yen - JPY - ¥')).toBeInTheDocument();
  });

  it('renders PLN description text', () => {
    render(<MoreAboutGroup />);
    expect(
      screen.getByText(/This is the official currency and legal tender of Poland./),
    ).toBeInTheDocument();
  });

  it('renders JPY description text', () => {
    render(<MoreAboutGroup />);
    expect(
      screen.getByText(/The yen is the official currency of Japan/),
    ).toBeInTheDocument();
  });
});
