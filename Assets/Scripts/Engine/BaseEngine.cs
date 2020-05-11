using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseEngine : MonoBehaviour, IEngine
{
    private Rigidbody2D m_Rigidbody;
    [SerializeField] private float m_MoveForce = 100000;
    [SerializeField] private float m_RotationForce = 100;
    
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(m_Rigidbody, $"Rigidbody2D couldn't be found. {name} requires it.");
    }

    public virtual void Move(Vector2 dir)
    {
        m_Rigidbody.AddForce(m_MoveForce * Time.deltaTime * dir, ForceMode2D.Force);
    }

    public virtual void Rotate(float amount)
    {
        m_Rigidbody.AddTorque(amount * Time.deltaTime * m_RotationForce, ForceMode2D.Force);
    }
}