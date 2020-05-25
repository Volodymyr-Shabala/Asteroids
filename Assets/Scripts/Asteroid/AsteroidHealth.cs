using UnityEngine;

public class AsteroidHealth : SpaceObject, IDamageable
{
    /*
     * Random scale
     * Bigger scale => more damage => more hp => slower => bigger mass
     * Move with linear and angular speed
     * If big enough on destroy spawn small asteroids. Small asteroids move in opposite directions
     */

    public ObservableProperty<float> Health { get; } = new ObservableProperty<float>();
    private readonly CompositeDisposable m_Disposable = new CompositeDisposable();
    private bool m_Subscribed;
    
    public void Init(float health)
    {
        Health.Value = health;
        RenewSubscriptions();
    }

    private void OnEnable()
    {
        if (m_Subscribed)
        {
            return;
        }

        RenewSubscriptions();
    }

    private void RenewSubscriptions()
    {
        Health.Where(health => health <= 0).Once().Subscribe((f) => ReturnToPool()).AddTo(m_Disposable);
        m_Subscribed = true;
    }

    private void OnDisable()
    {
        m_Disposable.Dispose();
        m_Subscribed = false;
    }
}