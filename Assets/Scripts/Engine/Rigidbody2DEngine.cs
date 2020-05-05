using UnityEngine;

public class Rigidbody2DEngine : IEngine
{
    private readonly Rigidbody2D m_Rigidbody;
    private readonly float m_MoveForce;
    private readonly float m_RotationForce;
    
    public Rigidbody2DEngine(Rigidbody2D rigidbody, float moveForce, float rotationForce)
    {
        m_Rigidbody = rigidbody;
        m_MoveForce = moveForce;
        m_RotationForce = rotationForce;
    }

    public void Move(Vector2 dir)
    {
        m_Rigidbody.AddForce(m_MoveForce * Time.deltaTime * dir, ForceMode2D.Force);
    }

    public void Rotate(float amount)
    {
        m_Rigidbody.AddTorque(amount * Time.deltaTime * m_RotationForce, ForceMode2D.Force);
    }
}