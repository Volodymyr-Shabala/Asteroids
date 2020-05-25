using UnityEngine;
using UnityEngine.Assertions;

public class EnergyBallDestroyManager : MonoBehaviour
{
    [SerializeField] private float m_Damage = 5;
    [SerializeField] private float m_PushForce = 10;
    [SerializeField] private float m_ExplodeRadius = 25;
    [SerializeField] private LayerMask m_LayerToDamage = 0;
    private IDamageable m_HealthContainer;
    private readonly CompositeDisposable m_CompositeDisposable = new CompositeDisposable();

    private void OnEnable()
    {
        if (m_HealthContainer == null)
        {
            m_HealthContainer = GetComponent<IDamageable>();
            Assert.IsNotNull(m_HealthContainer, $"IDamageable couldn't been found on {gameObject.name}.");
        }

        RenewSubscriptions();
    }

    private void Handle()
    {
        Vector3 myPosition = transform.position;
        Collider2D[] colliders = new Collider2D[25];
        Physics2D.OverlapCircleNonAlloc(myPosition, m_ExplodeRadius, colliders, m_LayerToDamage);

        foreach (Collider2D col in colliders)
        {
            if (!col)
            {
                continue;
            }

            Vector2 dir = (col.transform.position - myPosition).normalized;
            col.GetComponent<IEngine>()?.Push(dir * m_PushForce, 0);
            IDamageable healthContainer = col.GetComponent<IDamageable>();
            if (healthContainer != null)
            {
                healthContainer.Health.Value -= m_Damage;
            }

        }
    }

    private void RenewSubscriptions()
    {
        m_HealthContainer.Health.
                          Where(health => health <= 0).
                          Once().
                          Subscribe(f => Handle());
    }

    private void OnDisable()
    {
        m_CompositeDisposable.Dispose();
    }
}