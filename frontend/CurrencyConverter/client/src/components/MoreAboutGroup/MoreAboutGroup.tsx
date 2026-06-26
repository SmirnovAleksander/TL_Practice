import { MoreAboutCurrency } from '../MoreAboutCurrency';
import { MoreAboutButton } from '../MoreAboutButton';
import styles from './MoreAboutGroup.module.scss';
import type { Currency } from '../../models';
import { useState } from 'react';

type MoreAboutGroupProps = {
  fromCurrency: Currency | undefined;
  toCurrency: Currency | undefined;
};

export const MoreAboutGroup = ({ fromCurrency, toCurrency }: MoreAboutGroupProps) => {
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const label = fromCurrency && toCurrency ? `${fromCurrency.code}/${toCurrency.code}: about` : '';

  return (
    <div className={styles.info}>
      <MoreAboutButton label={label} isOpen={isOpen} onClick={() => setIsOpen((prev) => !prev)} />
      {isOpen && (
        <div className={styles.blocks}>
          {fromCurrency && (
            <div data-testid="more-about-currency-from">
              <MoreAboutCurrency
                title={`${fromCurrency.name} - ${fromCurrency.code} - ${fromCurrency.symbol}`}
                description={fromCurrency.description || 'No description'}
              />
            </div>
          )}
          {toCurrency && (
            <div data-testid="more-about-currency-to">
              <MoreAboutCurrency
                title={`${toCurrency.name} - ${toCurrency.code} - ${toCurrency.symbol}`}
                description={toCurrency.description || 'No description'}
              />
            </div>
          )}
        </div>
      )}
    </div>
  );
};
