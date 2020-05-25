using System;
using System.Collections.Generic;

public class ObservableProperty<T> : IObservable<T>
{
    private T m_Value;
    private readonly Subject<T> m_Subject = new Subject<T>();
    public T Value
    {
        get => m_Value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(m_Value, value))
            {
                m_Value = value;
                m_Subject.OnNext(m_Value);
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        IDisposable subscription = null;
        try
        {
            observer.OnNext(m_Value);
        } finally
        {
            subscription = m_Subject.Subscribe(observer);
        }

        return subscription;
    }
}