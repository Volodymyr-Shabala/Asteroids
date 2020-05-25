using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SpaceObject : MonoBehaviour
{
    protected Rigidbody2D m_Rigidbody2D;
    protected ObjectPool m_ObjectPool;
    public Action<SpaceObject> OnReset;
    protected Vector3 m_InitialScale = new Vector3(0,0,0);

    protected virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_ObjectPool = FindObjectOfType<ObjectPool>();
        Assert.IsNotNull(m_ObjectPool, $"There is no ObjectPool in the scene");
        m_InitialScale = transform.localScale;
    }

    public virtual void ResetObject()
    {
        Transform myTransform = transform;
        myTransform.position = new Vector3(0, 0, 0);
        myTransform.rotation = Quaternion.identity;
        myTransform.localScale = m_InitialScale;
        
        m_Rigidbody2D.velocity = new Vector2(0, 0);
        m_Rigidbody2D.angularVelocity = 0;
        OnReset?.Invoke(this);
    }

    protected virtual void ReturnToPool()
    {
        ResetObject();
        m_ObjectPool.Return(this);
        gameObject.SetActive(false);
    }
}