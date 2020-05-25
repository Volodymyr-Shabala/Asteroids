using UnityEngine;

public class RepulseWeapon : BaseWeapon
{
    [SerializeField] private LayerMask m_EnemyLayer = 1;
    [SerializeField] private float m_PushForce = 10;
    private float m_Radius;

    public override void Init(Transform[] shootPositions)
    {
        base.Init(shootPositions);
        m_Radius = (transform.position - shootPositions[0].position).magnitude;
    }

    public override void Fire()
    {
        if (!CanFire() || !m_Initialized)
        {
            return;
        }

        Vector3 myPosition = transform.position;
        Collider2D[] colliders = new Collider2D[1];
        Physics2D.OverlapCircleNonAlloc(myPosition, m_Radius, colliders, m_EnemyLayer);

        foreach (Collider2D col in colliders)
        {
            if (!col)
            {
                continue;
            }

            Vector2 dir = (col.transform.position - myPosition).normalized;
            col.GetComponent<IEngine>()?.Push(dir * m_PushForce, 0);
        }
    }
}