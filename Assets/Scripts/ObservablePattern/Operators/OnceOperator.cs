using System;

public class OnceOperator<T> : IObservable<T>
{
    private readonly IObservable<T> m_OriginalObservable;

    public OnceOperator(IObservable<T> originalObservable)
    {
        m_OriginalObservable = originalObservable;
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        OnceOperatorObserver onceOperatorObserver = new OnceOperatorObserver(observer);
        return m_OriginalObservable.Subscribe(onceOperatorObserver);
    }
    
    private class OnceOperatorObserver : IObserver<T>
    {
        private readonly IObserver<T> m_OriginalObserver;
        private bool m_Once;

        public OnceOperatorObserver(IObserver<T> originalObserver)
        {
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
            if (m_Once)
            {
                return;
            }
            m_Once = true;
            m_OriginalObserver.OnNext(value);
            // m_OriginalObserver.OnCompleted();
        }
    }
}