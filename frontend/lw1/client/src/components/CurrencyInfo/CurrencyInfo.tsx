import CurrencyDescription from "../CurrencyDescription/CurrencyDescription";
import CurrencyInfoToggle from "../CurrencyInfoToggle/CurrencyInfoToggle";
import styles from './CurrencyInfo.module.scss';

const pln = {
  title: 'Polish zloty - PLN - zł',
  description:
    'This is the official currency and legal tender of Poland. It is subdivided into 100 grosz-y (gr). It is the most traded currency in Central and Eastern Europe and ranks 21st most-traded in the foreign exchange market. '
};

const jpy = {
  title: 'Japanese yen - JPY - ¥',
  description:
    'The yen is the official currency of Japan. It is the third-most traded currency in the foreign exchange market, after the United States dollar and the euro. It is also widely used as a third reserve currency after the US dollar and the euro.'
};

const CurrencyInfo = () => {
    return (
        <div className={styles.info}>
            <CurrencyInfoToggle/>
            <div className={styles.blocks}>
                <CurrencyDescription title={pln.title} description={pln.description} />
                <CurrencyDescription title={jpy.title} description={jpy.description} />
            </div>
        </div>
    );
};

export default CurrencyInfo;