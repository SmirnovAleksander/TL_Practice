import ConverterCard from "./components/ConverterCard/ConverterCard";
import styles from './App.module.scss';

const App = () => {
    return (
        <div className={styles.page}>
            <ConverterCard />
        </div>
    );
};

export default App;