import styles from './StatusMessage.module.scss';

type StatusMessageProps = {
  message: string;
  variant: 'loading' | 'error';
};

export const StatusMessage = ({ message, variant }: StatusMessageProps) => {
  return (
    <div className={`${styles.card} ${styles[variant]}`}>
      <p>{message}</p>
    </div>
  );
};
