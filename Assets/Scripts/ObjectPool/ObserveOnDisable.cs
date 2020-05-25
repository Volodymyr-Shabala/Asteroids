using System;
using UnityEngine;

public class ObserveOnDisable : MonoBehaviour
{
    public event Action<GameObject> OnDisableObservable;

    private void OnDisable()
    {
        OnDisableObservable?.Invoke(gameObject);
    }
}