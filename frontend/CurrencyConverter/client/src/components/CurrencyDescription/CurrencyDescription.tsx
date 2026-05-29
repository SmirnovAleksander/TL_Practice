import styles from './CurrencyDescription.module.scss';

interface CurrencyDescriptionProps {
  title: string;
  description: string;
};

export const CurrencyDescription = ({title, description}: CurrencyDescriptionProps) => {
    return (
        <div>
            <h3 className={styles.title}>{title}</h3>
            <p className={styles.description}>{description}</p>
        </div>
    );
};
