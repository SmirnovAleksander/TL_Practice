import { ConverterCard } from '../../components/ConverterCard';
import styles from './ConverterPage.module.scss';

export const ConverterPage = () => {
    return (
        <div className={styles.page}>
            <ConverterCard />
        </div>
    );
};