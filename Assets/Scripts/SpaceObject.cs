using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SpaceObject : MonoBehaviour, IDamageable
{
    protected Rigidbody2D m_Rigidbody;

    protected virtual void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    public virtual void TakeDamage(float amount)
    {
    }
}