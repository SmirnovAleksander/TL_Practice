
import { CartesianGrid, Line, LineChart, Tooltip, XAxis, YAxis } from 'recharts';
import styles from './PriceGraph.module.scss';
import type { PriceChange } from '../../models';

type PriceGraphProps = {
    data: PriceChange[] | null;
    isLoading: boolean;
    error: string | null;
}

export const PriceGraph = ({ data, isLoading, error }: PriceGraphProps) => {
    const formatDateTime = (iso: string) => {
        const dt = new Date(iso);
        return dt.toLocaleTimeString('ru-RU', { minute: '2-digit', second: '2-digit' });
    };

    if (isLoading && !data) {
        return <div className={styles.chart}><div className={styles.loading}>Loading chart...</div></div>;
    }

    if (error && !data) {
        return <div className={styles.chart}><div className={styles.error}>{error}</div></div>;
    }

    if (data && data.length === 0) {
        return <div className={styles.chart}><div className={styles.empty}>No data for selected period.</div></div>;
    }

    if (!data) return null;

    return (
        <div className={styles.chart}>
            <LineChart
                style={{ width: '100%', height: 200 }}
                data={data}
            >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="dateTime" tickFormatter={formatDateTime} />
                <YAxis domain={['auto', 'auto']} />
                <Tooltip
                    labelFormatter={formatDateTime}
                    formatter={(value: number) => [value.toFixed(4), 'Price']}
                />
                <Line type="monotone" dataKey="price" stroke="#2080e1" dot={false} strokeWidth={2} />
            </LineChart>
        </div>
    );
};
