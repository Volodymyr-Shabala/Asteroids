using System;

public static class ExtensionMethodsIDisposable
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
    {
        return observable.Subscribe(new ActionToObserver<T>(onNext));
    }

    public static IDisposable Subscribe<T>(this IObservable<T> observable, IObserver<T> observer)
    {
        return observable.Subscribe(observer);
    }
}