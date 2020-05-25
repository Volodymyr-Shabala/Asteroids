using UnityEngine;
using UnityEngine.Assertions;

public class AsteroidSpawnManager : MonoBehaviour
{
    [SerializeField] private AsteroidHealth m_AsteroidHealthPrefab = null;
    [SerializeField] private float m_TimeBetweenSpawns = 0.2f;
    [SerializeField] private float m_PushForce = 75;
    [SerializeField] private float m_RotationForceMax = 10;
    private float m_TimeHolder;
    private const float k_SpawnOffset = 2;

    private Vector2 m_NegativeBorder;

    private void Start()
    {
        m_TimeHolder = Time.time + m_TimeBetweenSpawns;
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float cameraSize = mainCamera.orthographicSize;
            Vector3 cameraPosition = mainCamera.transform.position;
            m_NegativeBorder = new Vector2(cameraPosition.x - cameraSize * 2 - k_SpawnOffset,
                                           cameraPosition.y - cameraSize - k_SpawnOffset);
        }
    }

    private void Update()
    {
        if (Time.time > m_TimeHolder)
        {
            SpawnAsteroidRandom();
            m_TimeHolder = Time.time + m_TimeBetweenSpawns;
        }
    }

    private void SpawnAsteroidRandom()
    {
        Vector3 position = GetRandomSpawnPosition();
        SpaceObject asteroid = SpawnAsteroidAtPosition(position);
        Vector2 pushForce = GetMoveDirectionFromPosition(position) * m_PushForce;
        asteroid.GetComponent<IEngine>().Push(pushForce, Random.Range(0, m_RotationForceMax));
    }

    public SpaceObject SpawnAsteroidAtPosition(Vector3 position)
    {
        SpaceObject asteroid = ObjectPool.Get(m_AsteroidHealthPrefab);
        asteroid.transform.position = position;
        return asteroid;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float random = Random.value;
        float x = m_NegativeBorder.x;
        float y = m_NegativeBorder.y;

        if (random < 0.5f)
        {
            x = Lerp(m_NegativeBorder.x, Mathf.Abs(m_NegativeBorder.x), Random.value);
            random = Random.value;
            if (random < 0.5f)
            {
                y = Mathf.Abs(m_NegativeBorder.y);
            }
        } else
        {
            y = Lerp(m_NegativeBorder.y, Mathf.Abs(m_NegativeBorder.y), Random.value);
            random = Random.value;
            if (random < 0.5f)
            {
                x = Mathf.Abs(m_NegativeBorder.x);
            }
        }

        return new Vector3(x, y);
    }

    private Vector2 GetMoveDirectionFromPosition(Vector3 position)
    {
        Vector2 random = Random.insideUnitCircle * 20;
        Vector2 returnValue = (random - (Vector2) position).normalized;
        return returnValue;
    }

    private float Lerp(float v0, float v1, float t)
    {
        return (1 - t) * v0 + t * v1;
    }
}