using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private SpaceObject m_UFOPrefab = null;
    [SerializeField] private float m_SpawnChance = 2;

    private void Update()
    {
        float randomChance = Random.value * 100;
        if (randomChance <= m_SpawnChance)
        {
            SpawnUFO();
        }
    }

    private void SpawnUFO()
    {
        SpaceObject ufo = ObjectPool.Get(m_UFOPrefab);
        ufo.transform.position = transform.position;
    }
}