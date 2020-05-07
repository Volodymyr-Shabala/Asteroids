using UnityEngine;
using UnityEngine.Assertions;

public class BaseEngine : MonoBehaviour, IEngine
{
    private Rigidbody2D m_Rigidbody;
    [SerializeField] private float m_MoveForce = 100000;
    [SerializeField] private float m_RotationForce = 100;

    private bool m_Initialized;
    
    private void Start()
    {
        m_Rigidbody = GetComponentInParent<Rigidbody2D>();
        Assert.IsNotNull(m_Rigidbody, $"Rigidbody2D couldn't be found in parent. {name} depends on it.");
        m_Initialized = true;
    }

    public virtual void Move(Vector2 dir)
    {
        if (!m_Initialized)
        {
            return;
        }
        
        m_Rigidbody.AddForce(m_MoveForce * Time.deltaTime * dir, ForceMode2D.Force);
    }

    public virtual void Rotate(float amount)
    {
        if (!m_Initialized)
        {
            return;
        }

        m_Rigidbody.AddTorque(amount * Time.deltaTime * m_RotationForce, ForceMode2D.Force);
    }
}