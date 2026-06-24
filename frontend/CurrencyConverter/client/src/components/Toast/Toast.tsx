import { useEffect, useState } from 'react';
import styles from './Toast.module.scss';

type ToastProps = {
    message: string | null;
    duration?: number;
};

const ToastInner = ({ message, duration }: { message: string; duration: number }) => {
    const [visible, setVisible] = useState(true);

    useEffect(() => {
        const timer = setTimeout(() => setVisible(false), duration);

        return () => clearTimeout(timer);
    }, [duration]);

    if (!visible) return null;

    return (
        <div className={styles.toast}>
            {message}
        </div>
    );
};

export const Toast = ({ message, duration = 3000 }: ToastProps) => {
    if (!message) return null;

    return <ToastInner key={message} message={message} duration={duration} />;
};
