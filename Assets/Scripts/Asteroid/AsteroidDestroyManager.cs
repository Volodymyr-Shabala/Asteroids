using UnityEngine;

public class AsteroidDestroyManager : MonoBehaviour, IAsteroidDestroyManager
{
    private int m_SpawnAmount;

    public void Init(int spawnAmount)
    {
        m_SpawnAmount = spawnAmount;
    }

    public void Handle()
    {
        float angle = 360 / (float) m_SpawnAmount;
        for (int i = 0; i < m_SpawnAmount; i++)
        {
            AsteroidSetup asteroid =
                Instantiate(gameObject, transform.position, Quaternion.identity).
                    GetComponent<AsteroidSetup>();

            asteroid.Initialize();
            IEngine engine = asteroid.GetComponent<IEngine>();
            engine.Move(new Vector2(Mathf.Sin(angle * i * Mathf.Deg2Rad),
                                    Mathf.Cos(angle * i * Mathf.Deg2Rad)));
            engine.Rotate(Random.Range(0, 255));
        }
    }
}