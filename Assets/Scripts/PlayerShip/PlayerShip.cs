using UnityEngine;

public class PlayerShip : SpaceObject 
{
    [SerializeField] private float m_Health = 10;

    public override void TakeDamage(float amount)
    {
        m_Health -= amount;
        if (m_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}