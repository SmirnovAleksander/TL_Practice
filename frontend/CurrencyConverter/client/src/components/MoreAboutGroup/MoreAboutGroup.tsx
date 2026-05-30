import { MoreAboutCurrency } from "../MoreAboutCurrency";
import { MoreAboutButton } from "../MoreAboutButton";
import styles from './MoreAboutGroup.module.scss';
import type { Currency } from "../../models";

type MoreAboutGroupProps = {
    fromCurrency: Currency;
    toCurrency: Currency;
    isOpen: boolean;
    handleToggleMoreAbout: () => void;
}

export const MoreAboutGroup = ({
    fromCurrency,
    toCurrency,
    isOpen,
    handleToggleMoreAbout
}: MoreAboutGroupProps) => {
    const label = (fromCurrency && toCurrency) ? `${fromCurrency.code}/${toCurrency.code}` : ''
    
    return (
        <div className={styles.info}>
            <MoreAboutButton label={label} isOpen={isOpen} handleToggleMoreAbout={handleToggleMoreAbout}/>
            {isOpen && (
                <div className={styles.blocks}>
                    {fromCurrency && (
                        <div>
                            <MoreAboutCurrency 
                                title={`${fromCurrency.name} - ${fromCurrency.code} - ${fromCurrency.symbol}`} 
                                description={fromCurrency.description || ''} 
                            />
                        </div>
                    )}
                    {toCurrency && (
                        <div>
                            <MoreAboutCurrency 
                                title={`${toCurrency.name} - ${toCurrency.code} - ${toCurrency.symbol}`} 
                                description={toCurrency.description || ''} 
                            />
                        </div>
                    )}
                </div>
            )}
        </div>
    );
};
