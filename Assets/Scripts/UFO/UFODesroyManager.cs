using UnityEngine;
using UnityEngine.Assertions;

public class UFODesroyManager : MonoBehaviour
{
    [SerializeField] private SpaceObject m_SpawnOnDestroy = null;
    [SerializeField] private int m_SpawnAmount = 1;
    [SerializeField] private float m_PushForce = 100;
    private IDamageable m_HealthContainer;
    private readonly CompositeDisposable m_CompositeDisposable = new CompositeDisposable();

    private void OnEnable()
    {
        if (m_HealthContainer == null)
        {
            m_HealthContainer = GetComponent<IDamageable>();
            Assert.IsNotNull(m_SpawnOnDestroy, $"There is no object to spawn on destroy assigned.");
            Assert.IsNotNull(m_HealthContainer, $"Couldn't find IDamageable on {gameObject.name}.");
        }

        RenewSubscriptions();
    }

    private void Handle()
    {
        for (int i = 0; i < m_SpawnAmount; i++)
        {
            SpaceObject energyBall = ObjectPool.Get(m_SpawnOnDestroy);
            energyBall.transform.position = transform.position;
            Vector2 randomDir = Random.onUnitSphere;
            energyBall.GetComponent<IEngine>()?.Push(randomDir * m_PushForce, 0);
        }
    }

    private void RenewSubscriptions()
    {
        m_HealthContainer.Health.Where(health => health <= 0).
                          Once().
                          Subscribe(f => Handle()).AddTo(m_CompositeDisposable);
    }

    private void OnDisable()
    {
        m_CompositeDisposable.Dispose();
    }
}