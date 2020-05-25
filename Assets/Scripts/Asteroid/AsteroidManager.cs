using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private Vector2 m_HealthMinMax = new Vector2(1, 10);
    [SerializeField] private Vector2 m_MassMinMax = new Vector2(10, 100);
    [SerializeField] private Vector2 m_ScaleMinMax = new Vector2(0.25f, 5);
    // [SerializeField] private float m_ScaleToSpawnSmallAsteroids = 3f;
    [SerializeField] private int m_AsteroidsSpawnAmount = 5;
    [SerializeField] private float m_PushForceSpawnedAsteroids = 50;
    [SerializeField] private Sprite[] m_Sprites = new Sprite[0];

    private Rigidbody2D m_Rigidbody;
    private SpriteRenderer m_SpriteRenderer;
    private AsteroidHealth m_AsteroidHealth;
    private IDestroyManager m_DestroyManager;
    private bool m_Initialized;
    private bool m_IsSmall;

    private void Awake()
    {
        FindObjectOfType<ScreenWrappingManager>()?.AddToWrap(transform);
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_AsteroidHealth = GetComponent<AsteroidHealth>();
        m_DestroyManager = GetComponent<IDestroyManager>();

        m_DestroyManager.Init(m_AsteroidHealth, m_AsteroidsSpawnAmount, m_PushForceSpawnedAsteroids);
    }

    private void OnEnable()
    {
        if (m_Initialized)
        {
            return;
        }

        InitializeInternal();
    }

    private void InitializeInternal()
    {
        float scale = m_IsSmall ? 1f : Random.Range(m_ScaleMinMax.x, m_ScaleMinMax.y);
        float scalePercent = scale / m_ScaleMinMax.y;
        float heath = scalePercent * m_HealthMinMax.y;
        transform.localScale = Vector3.one * scale;

        m_Rigidbody.mass = scalePercent * m_MassMinMax.y;
        m_SpriteRenderer.sprite = m_Sprites[Random.Range(0, m_Sprites.Length)];

        m_AsteroidHealth.Init(heath);

        m_Initialized = true;
    }

    public void SetSmall()
    {
        m_IsSmall = true;
        InitializeInternal();
    }

    private void OnDisable()
    {
        m_Initialized = false;
        m_IsSmall = false;
    }
}