import { MoreAboutCurrency } from "../MoreAboutCurrency";
import { MoreAboutButton } from "../MoreAboutButton";
import styles from './MoreAboutGroup.module.scss';

const pln = {
  title: 'Polish zloty - PLN - zł',
  description: 'This is the official currency and legal tender of Poland. It is subdivided into 100 grosz-y (gr). It is the most traded currency in Central and Eastern Europe and ranks 21st most-traded in the foreign exchange market. ',
  dataTestId: 'more-about-currency-pln'
};

const jpy = {
  title: 'Japanese yen - JPY - ¥',
  description: 'The yen is the official currency of Japan. It is the third-most traded currency in the foreign exchange market, after the United States dollar and the euro. It is also widely used as a third reserve currency after the US dollar and the euro.',
  dataTestId: 'more-about-currency-jpy'
};

export const MoreAboutGroup = () => {
    return (
        <div className={styles.info}>
            <MoreAboutButton />
            <div className={styles.blocks}>
                <MoreAboutCurrency dataTestId={pln.dataTestId} title={pln.title} description={pln.description} />
                <MoreAboutCurrency dataTestId={jpy.dataTestId} title={jpy.title} description={jpy.description} />
            </div>
        </div>
    );
};
