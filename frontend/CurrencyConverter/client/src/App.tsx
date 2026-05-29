import { ConverterCard } from "./components/ConverterCard/ConverterCard";
import styles from './App.module.scss';

export const App = () => {
    return (
        <div className={styles.page}>
            <ConverterCard />
        </div>
    );
};
