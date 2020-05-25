using System;

public static class ExtensionMethodsCompositeDisposable
{
    public static void AddTo(this IDisposable disposable, CompositeDisposable compositeDisposable)
    {
        compositeDisposable.Add(disposable);
    }
}