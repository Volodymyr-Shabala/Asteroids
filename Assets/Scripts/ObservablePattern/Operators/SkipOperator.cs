using System;

public class SkipOperator<T> : IObservable<T>
{
    private readonly IObservable<T> m_OriginalObservable;
    private readonly int m_SkipAmount;

    public SkipOperator(IObservable<T> originalObservable, int skipAmount)
    {
        m_OriginalObservable = originalObservable;
        m_SkipAmount = skipAmount;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        SkipOperatorObserver skipOperatorObserver = new SkipOperatorObserver(observer, m_SkipAmount);
        return m_OriginalObservable.Subscribe(skipOperatorObserver);
    }
    
    private class SkipOperatorObserver : IObserver<T>
    {
        private readonly IObserver<T> m_OriginalObserver;
        private int m_SkipAmount;
        
        public SkipOperatorObserver(IObserver<T> originalObserver, int skipAmount)
        {
            m_OriginalObserver = originalObserver;
            m_SkipAmount = skipAmount;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            if (m_SkipAmount <= 0)
            {
                m_OriginalObserver.OnNext(value);
            } else
            {
                m_SkipAmount--;
            }
        }
    }
}