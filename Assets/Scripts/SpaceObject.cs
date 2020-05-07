using UnityEngine;

public abstract class SpaceObject : MonoBehaviour, IObject
{
    public Rigidbody2D Rigidbody2D { get; set; }

    public virtual void TakeDamage(float amount)
    {
    }
}