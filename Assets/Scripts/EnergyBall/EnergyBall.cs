using UnityEngine;

public class EnergyBall : SpaceObject, IDamageable
{
    public ObservableProperty<float> Health { get; } = new ObservableProperty<float>();
    private readonly CompositeDisposable m_CompositeDisposable = new CompositeDisposable();
    [SerializeField] private float m_HealthToGive = 10;
    [SerializeField] private string m_ObjectToCollideTag = "Player";
    
    private void OnEnable()
    {
        Health.Value = 1;
        RenewSubscriptions();
    }

    private void Start()
    {
        FindObjectOfType<ScreenWrappingManager>()?.AddToWrap(transform);
    }

    private void RenewSubscriptions()
    {
        Health.Where(health => health <= 0).Once().Subscribe(f => ReturnToPool());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(m_ObjectToCollideTag))
        {
            other.gameObject.GetComponent<IDamageable>().Health.Value += m_HealthToGive;
            ReturnToPool();
        }
    }

    private void OnDisable()
    {
        m_CompositeDisposable.Dispose();
    }
}