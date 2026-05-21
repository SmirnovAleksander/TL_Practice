import styles from './App.module.scss';
import ConverterCard from './components/ConverterCard/ConverterCard'

function App() {
  return (
    <div className={styles.page}>
      <ConverterCard />
    </div>
  )
}

export default App