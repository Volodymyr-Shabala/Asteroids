using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class AsteroidDestroyManager : MonoBehaviour, IDestroyManager
{
    private AsteroidSpawnManager m_SpawnManager;
    private IDamageable m_HealthContainer;
    private int m_SpawnAmount;
    private float m_PushForce;
    private readonly CompositeDisposable m_CompositeDisposable = new CompositeDisposable();

    private void OnEnable()
    {
        if (m_HealthContainer != null)
        {
            RenewSubscriptions();
        }
    }

    public void Init(IDamageable healthContainer, int spawnAmount, float pushForce)
    {
        m_HealthContainer = healthContainer;
        m_SpawnAmount = spawnAmount;
        m_PushForce = pushForce;

        m_SpawnManager = FindObjectOfType<AsteroidSpawnManager>();
        Assert.IsNotNull(m_SpawnManager,
                         $"Couldn't find AsteroidSpawnManager in the scene. {name} requires it.");
        Assert.IsNotNull(m_HealthContainer, $"IDamageable couldn't been found on {gameObject.name}.");
        RenewSubscriptions();
    }

    public void Handle()
    {
        float angle = 360 / (float) m_SpawnAmount;
        for (int i = 0; i < m_SpawnAmount; i++)
        {
            Vector3 pushDirection = GetPushDirection(i, angle);
            SpaceObject asteroid = SpawnAsteroidAtPosition(transform.localPosition + pushDirection);
            asteroid.GetComponent<AsteroidManager>().SetSmall();
            asteroid.GetComponent<IEngine>().
                     Push(pushDirection * m_PushForce, Random.Range(0, 25));
        }
    }

    private SpaceObject SpawnAsteroidAtPosition(Vector3 position)
    {
        return m_SpawnManager.SpawnAsteroidAtPosition(position);
    }

    private Vector3 GetPushDirection(int i, float angle)
    {
        float angleRad = angle * i * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(angleRad), Mathf.Cos(angleRad));
    }

    private void RenewSubscriptions()
    {
        m_HealthContainer.Health.
                          Where(health => health <= 0).
                          Where(f => transform.localScale.x >= 3).
                          Where(f => gameObject.activeInHierarchy).
                          Once().
                          Subscribe(f => Handle()).
                          AddTo(m_CompositeDisposable);
    }

    private void OnDisable()
    {
        m_CompositeDisposable.Dispose();
    }
}