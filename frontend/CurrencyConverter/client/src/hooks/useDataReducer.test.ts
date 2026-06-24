import { describe, it, expect } from 'vitest';
import type { DataState, DataAction } from './useDataReducer';
import { createDataReducer } from './useDataReducer';

describe('useDataReducer', () => {
    const reducer = createDataReducer<string[]>();

    const initialState: DataState<string[]> = {
        isLoading: false,
        data: null,
        error: null,
    };

    it('проверка initialState', () => {
        expect(initialState).toEqual({
            isLoading: false,
            data: null,
            error: null,
        });
    });

    it('переходит в состояние загрузки при dispatch LOADING', () => {
        const action: DataAction<string[]> = { type: 'LOADING' };
        const newState = reducer(initialState, action);

        expect(newState.isLoading).toBe(true);
        expect(newState.data).toBeNull();
        expect(newState.error).toBeNull();
    });

    it('записывает данные при dispatch SUCCESS', () => {
        const action: DataAction<string[]> = { type: 'SUCCESS', payload: ['CAD', 'USD'] };
        const newState = reducer(initialState, action);

        expect(newState.isLoading).toBe(false);
        expect(newState.data).toEqual(['CAD', 'USD']);
        expect(newState.error).toBeNull();
    });

    it('записывает ошибку при dispatch ERROR', () => {
        const action: DataAction<string[]> = { type: 'ERROR', payload: 'Network Error' };
        const newState = reducer(initialState, action);

        expect(newState.isLoading).toBe(false);
        expect(newState.data).toBeNull();
        expect(newState.error).toBe('Network Error');
    });

    it('очищает data и error при повторном LOADING', () => {
        const successState = reducer(initialState, { type: 'SUCCESS', payload: ['CAD'] });
        const loadingState = reducer(successState, { type: 'LOADING' });

        expect(loadingState.isLoading).toBe(true);
        expect(loadingState.data).toBeNull();
        expect(loadingState.error).toBeNull();
    });
});
