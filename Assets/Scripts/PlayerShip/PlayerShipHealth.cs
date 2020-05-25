using UnityEngine;

public class PlayerShipHealth : SpaceObject, IDamageable
{
    [SerializeField] protected float m_Health = 10;
    public ObservableProperty<float> Health { get; } = new ObservableProperty<float>();
    private readonly CompositeDisposable m_Disposable = new CompositeDisposable();

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<ScreenWrappingManager>()?.AddToWrap(transform);
        Health.Value = m_Health;
    }

    private void OnEnable()
    {
        RenewSubscriptions();
    }

    private void RenewSubscriptions()
    {
        Health.Where(health => health <= 0).
               Once().
               Subscribe((f) => gameObject.SetActive(false)).
               AddTo(m_Disposable);
    }

    private void OnDisable()
    {
        m_Disposable.Dispose();
    }
}