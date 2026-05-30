import styles from './MoreAboutCurrency.module.scss';

type MoreAboutCurrencyProps = {
  title: string;
  description: string;
  dataTestId?: string;
};

export const MoreAboutCurrency = ({title, description, dataTestId}: MoreAboutCurrencyProps) => {
    return (
        <div data-testid={dataTestId}>
            <h3 className={styles.title}>{title}</h3>
            <p className={styles.description}>{description}</p>
        </div>
    );
};
