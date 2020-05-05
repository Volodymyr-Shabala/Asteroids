using UnityEngine;

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

    protected virtual void ApplyDamage(IHealth target)
    {
        target.Health -= m_Damage;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        IHealth target = other.collider.GetComponent<IHealth>();

        if (target != null)
        {
            ApplyDamage(target);
            DestroySelf();
        }
    }
}