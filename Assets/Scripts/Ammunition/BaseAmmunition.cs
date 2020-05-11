using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseAmmunition : MonoBehaviour, IMovable
{
    private Rigidbody2D m_Rigidbody;
    private float m_MoveForce = 100;
    
    private float m_Damage = 1;
    
    private const float k_MaxTimeAlive = 5f;
    private float m_Timer;

    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(m_Rigidbody, $"Rigidbody2D needs to be assigned in {name}");
        m_Timer = Time.time + k_MaxTimeAlive;
    }

    protected virtual void Update()
    {
        if (Time.time >= m_Timer)
        {
            DestroySelf();
        }
    }

    public virtual void Move(Vector2 dir)
    {
        m_Rigidbody.AddForce(m_MoveForce * Time.deltaTime * dir, ForceMode2D.Impulse);
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable target = other.collider.GetComponent<IDamageable>();

        if (target != null)
        {
            target.TakeDamage(m_Damage);
            DestroySelf();
        }
    }
}