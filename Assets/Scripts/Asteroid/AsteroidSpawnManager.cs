using UnityEngine;

public class AsteroidSpawnManager : MonoBehaviour
{
    [SerializeField] private Asteroid m_AsteroidPrefab = null;
    [SerializeField] private float m_TimeBetweenSpawns = 0.2f;
    private float m_TimeHolder;

    private Vector2 m_NegativeBorder;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float cameraSize = mainCamera.orthographicSize;
            Vector3 cameraPosition = mainCamera.transform.position;
            m_NegativeBorder = new Vector2(cameraPosition.x - cameraSize * 2 - 5,
                                           cameraPosition.y - cameraSize - 5);
        }
    }

    private void Update()
    {
        if (Time.time > m_TimeHolder)
        {
            SpawnAsteroid();
            m_TimeHolder = Time.time + m_TimeBetweenSpawns;
        }
    }

    // Where to spawn?
    // Where to push?
    private void SpawnAsteroid()
    {
        Vector3 position = GetRandomSpawnPosition();
        IEngine engine = Instantiate(m_AsteroidPrefab, position, Quaternion.identity).GetComponent<IEngine>();
        engine.Move(GetMoveDirectionFromPosition(position));
        engine.Rotate(Random.Range(0, 255));
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

    // TODO: calculate direction
    private Vector2 GetMoveDirectionFromPosition(Vector3 position)
    {
        float x = 0;
        float y = 0;
        if (position.x < 0)
        {
            x = 1;
            if (position.y < 0)
            {
                
            }
            float random = Random.value;
            // y = lerp
        }
        // float x = position.x < 0 ? 1 : -1;
        // float y = position.y < 0 ? 1 : -1;

        return new Vector2(x, y);
    }

    private float Lerp(float v0, float v1, float t)
    {
        return (1 - t) * v0 + t * v1;
    }
}