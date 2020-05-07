using UnityEngine;

public class PlayerShip : SpaceObject, IHealth
{
    // Is this the best way to do it?
    public float Health { get => m_Health; set => m_Health = value; }
    [SerializeField] private float m_Health = 10;

    public override void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}