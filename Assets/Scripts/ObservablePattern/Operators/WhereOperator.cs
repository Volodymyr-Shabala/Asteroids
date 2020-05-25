using System;

public class WhereOperator<T> : IObservable<T>
{
    private readonly IObservable<T> m_OriginalObservable;
    private readonly Predicate<T> m_Filter;

    public WhereOperator(IObservable<T> originalObservable, Predicate<T> filter)
    {
        m_OriginalObservable = originalObservable;
        m_Filter = filter;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        WhereOperatorObserver whereOperatorObserver = new WhereOperatorObserver(this, observer);
        return m_OriginalObservable.Subscribe(whereOperatorObserver);
    }
    
    private class WhereOperatorObserver : IObserver<T>
    {
        private readonly WhereOperator<T> m_Parent;
        private readonly IObserver<T> m_OriginalObserver;

        public WhereOperatorObserver(WhereOperator<T> parent, IObserver<T> originalObserver)
        {
            m_Parent = parent;
            m_OriginalObserver = originalObserver;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            if (m_Parent.m_Filter(value))
            {
                m_OriginalObserver.OnNext(value);
            }
        }
    }
}