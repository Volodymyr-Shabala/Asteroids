using System;

public static class ObservableOperators
{
    public static IObservable<T> Skip<T>(this IObservable<T> observable, int skipAmount)
    {
        return new SkipOperator<T>(observable, skipAmount);
    }

    public static IObservable<T> Where<T>(this IObservable<T> observable, Predicate<T> filter)
    {
        return new WhereOperator<T>(observable, filter);
    }

    public static IObservable<TK> Map<T, TK>(this IObservable<T> observable, Func<T, TK> mapper)
    {
        return new MapOperator<T,TK>(observable, mapper);
    }

    public static IObservable<T> Once<T>(this IObservable<T> observable)
    {
        return new OnceOperator<T>(observable);
    }
}