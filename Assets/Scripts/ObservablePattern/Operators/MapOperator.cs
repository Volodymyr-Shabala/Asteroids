using System;

public class MapOperator<T, TK> : IObservable<TK>
{
    private readonly IObservable<T> m_OriginalObservable;
    private readonly Func<T, TK> m_Mapper;

    public MapOperator(IObservable<T> originalObservable, Func<T, TK> mapper)
    {
        m_OriginalObservable = originalObservable;
        m_Mapper = mapper;
    }

    public IDisposable Subscribe(IObserver<TK> observer)
    {
        MapOperatorObserver operatorObserver = new MapOperatorObserver(this, observer);
        return m_OriginalObservable.Subscribe(operatorObserver);
    }

    private class MapOperatorObserver : IObserver<T>
    {
        private readonly MapOperator<T, TK> m_Parent;
        private readonly IObserver<TK> m_OriginalObserver;

        public MapOperatorObserver(MapOperator<T, TK> parent, IObserver<TK> originalObserver)
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
            TK result = m_Parent.m_Mapper(value);
            m_OriginalObserver.OnNext(result);
        }
    }
}