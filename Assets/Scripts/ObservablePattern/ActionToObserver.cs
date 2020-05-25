using System;

public class ActionToObserver<T> : IObserver<T>
{
    private readonly Action<T> m_Action = null;

    public ActionToObserver(Action<T> action)
    {
        m_Action = action;
    }
    
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(T value)
    {
        m_Action.Invoke(value);
    }
}