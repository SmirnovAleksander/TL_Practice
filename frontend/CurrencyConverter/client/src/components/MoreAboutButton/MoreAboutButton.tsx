import arrow from '../../assets/arrow.svg';
import styles from './MoreAboutButton.module.scss';

type MoreAboutButtonProps = {
    label: string;
    isOpen: boolean;
    handleToggleMoreAbout: () => void;
}

export const MoreAboutButton = ({label, isOpen, handleToggleMoreAbout}: MoreAboutButtonProps) => {
    return (
        <div className={styles.wrapper}>
            <hr className={styles.line} />
            <button onClick={handleToggleMoreAbout} type="button" data-testid="more-about-button" className={styles.button}>
                {label} <img src={arrow} alt="" className={isOpen ? styles.arrowOpen : styles.arrow} />
            </button>
        </div>
    );
};
