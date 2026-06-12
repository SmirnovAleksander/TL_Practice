import { useEffect, useState } from 'react';
import styles from './Toast.module.scss';

type ToastProps = {
    message: string | null;
    duration?: number;
};

export const Toast = ({ message, duration = 3000 }: ToastProps) => {
    const [visible, setVisible] = useState(false);
    useEffect(() => {
        if (!message) {
            setVisible(false);
            return;
        }
        setVisible(true);

        const timer = setTimeout(() => setVisible(false), duration);
        return () => clearTimeout(timer);
    }, [ message ]);

    if (!visible)
        return null;
    
    return (
        <div className={styles.toast}>
            {message}
        </div>
    );
};