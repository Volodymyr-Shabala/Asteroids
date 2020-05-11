using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSetup : MonoBehaviour
{
    [SerializeField] private Vector2 m_HealthMinMax = new Vector2(1, 10);
    [SerializeField] private Vector2 m_MassMinMax = new Vector2(10, 100);
    [SerializeField] private Vector2 m_ScaleMinMax = new Vector2(0.25f, 5);
    [SerializeField] private float m_ScaleToSpawnSmallAsteroids = 3f;
    [SerializeField] private int m_SmallAsteroidsToSpawn = 5;
    [SerializeField] private Sprite[] m_Sprites = new Sprite[0];

    private bool m_Initialized;

    private void Start()
    {
        if (m_Initialized)
        {
            return;
        }

        Initialize(Random.Range(m_ScaleMinMax.x, m_ScaleMinMax.y));
    }

    public void Initialize(float scale = 1f)
    {
        if (m_Initialized)
        {
            return;
        }

        float scalePercent = scale / m_ScaleMinMax.y;

        transform.localScale = Vector3.one * scale;
        float heath = scalePercent * m_HealthMinMax.y;

        GetComponent<Rigidbody2D>().mass = scalePercent * m_MassMinMax.y;
        GetComponent<SpriteRenderer>().sprite = m_Sprites[Random.Range(0, m_Sprites.Length)];
        GetComponent<IAsteroid>().Init(heath, m_ScaleToSpawnSmallAsteroids);
        GetComponent<IAsteroidDestroyManager>()?.Init(m_SmallAsteroidsToSpawn);
        
        m_Initialized = true;
    }
}