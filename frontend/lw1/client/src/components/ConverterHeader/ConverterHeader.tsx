import styles from './ConverterHeader.module.scss';

const prefix = '1 Polish zloty is';
const rate = '0.99 Japanese yen';
const updatedAt = 'Fri, 05 Apr 2026 10:35 UTC';

const ConverterHeader = () => {
    return (
        <div className={styles.summary}>
            <p className={styles.prefix}>{prefix}</p>
            <p className={styles.rate}>{rate}</p>
            <p className={styles.updatedAt}>{updatedAt}</p> 
        </div>
    );
};

export default ConverterHeader;