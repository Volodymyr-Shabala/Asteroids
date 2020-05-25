using System;
using System.Collections.Generic;

public class Subject<T> : ISubject<T>
{
    private readonly List<IObserver<T>> m_Observers = new List<IObserver<T>>();
    private int m_Index;

    public void OnCompleted()
    {
        for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
        {
            m_Observers[m_Index].OnCompleted();
        }
    }

    public void OnError(Exception error)
    {
        for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
        {
            m_Observers[m_Index].OnError(error);
        }
    }

    public void OnNext(T value)
    {
        for (m_Index = 0; m_Index < m_Observers.Count; m_Index++)
        {
            m_Observers[m_Index].OnNext(value);
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        m_Observers.Add(observer);
        return new Unsubscriber(this, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly Subject<T> m_Subject;
        private readonly IObserver<T> m_Observer;

        public Unsubscriber(Subject<T> subject, IObserver<T> observer)
        {
            m_Subject = subject;
            m_Observer = observer;
        }

        public void Dispose()
        {
            int index = m_Subject.m_Observers.IndexOf(m_Observer);
            if (index < 0)
            {
                m_Subject.m_Observers.RemoveAt(index);

                if (index <= m_Subject.m_Index)
                {
                    m_Subject.m_Index--;
                }
            }
        }
    }
}