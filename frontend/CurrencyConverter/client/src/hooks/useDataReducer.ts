import { useReducer } from 'react';

export type DataState<T> = {
    isLoading: boolean;
    data: T | null;
    error: string | null;
};

export type DataAction<T> =
    | { type: 'LOADING' }
    | { type: 'SUCCESS'; payload: T }
    | { type: 'ERROR'; payload: string };

export const createDataReducer = <T>() => (
    state: DataState<T>,
    action: DataAction<T>,
): DataState<T> => {
    switch (action.type) {
        case 'LOADING':
            return { ...state, isLoading: true, error: null };
        case 'SUCCESS':
            return { isLoading: false, data: action.payload, error: null };
        case 'ERROR':
            return { ...state, isLoading: false, error: action.payload };
        default:
            return state;
    }
};

const initialData = {
    isLoading: false,
    data: null,
    error: null
};

export const useDataReducer = <T>() => {
    const [state, dispatch] = useReducer(
        createDataReducer<T>(),
        initialData
    );

    return { state, dispatch };
};