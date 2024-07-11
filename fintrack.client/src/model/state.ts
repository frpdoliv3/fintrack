export type Failure<T> = {
    failure: T
    success?: never
}

export type Success<T> = {
    failure?: never
    success: T
}

export type State<S, F> = NonNullable<Success<S> | Failure<F>>

export const makeFailure = <T>(value: T): Failure<T> => ({ failure: value });
export const makeSuccess = <T>(value: T): Success<T> => ({ success: value });
