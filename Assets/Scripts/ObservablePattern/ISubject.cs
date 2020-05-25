using System;

public interface ISubject<T> : IObservable<T>, IObserver<T>
{

}