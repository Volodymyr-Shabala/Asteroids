using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseAmmunition : SpaceObject
{
    protected IEngine m_Engine;
    private float m_Damage = 1;

    private const float k_MaxTimeAlive = 5f;
    private float m_Timer;

    protected virtual void Start()
    {
        m_Engine = GetComponent<IEngine>();
        Assert.IsNotNull(m_Engine, $"Engine needs to be added to {name}");
    }

    protected virtual void Update()
    {
        if (Time.time >= m_Timer)
        {
            ReturnToPool();
        }
    }

    protected void Move(Vector2 dir)
    {
        m_Engine.Move(dir);
    }

    protected void Rotate(float amount)
    {
        m_Engine.Rotate(amount);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable target = other.collider.GetComponent<IDamageable>();

        if (target != null)
        {
            target.Health.Value -= m_Damage;
            ReturnToPool();
        }
    }

    private void UpdateTimer()
    {
        m_Timer = Time.time + k_MaxTimeAlive;
    }

    protected void OnEnable()
    {
        UpdateTimer();
    }
}