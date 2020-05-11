using System;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseEngineHandler : MonoBehaviour, IEngineHandler
{
    protected IEngine m_Engine;
    protected Vector2 m_MovementInput;
    protected float m_RotationInput;
    protected const float k_InputTolerance = 0.001f;

    protected Func<Vector2> m_MovementInputFunc;
    protected Func<float> m_RottationInputFunc;

    protected virtual void Start()
    {
        m_Engine = GetComponent<IEngine>();
        Assert.IsNotNull(m_Engine, $"No Engine has been found on this GameObject. {name} requires it.");
    }
    
    protected virtual void FixedUpdate()
    {
        ApplyInput();
        ResetInputValues();
    }

    public virtual void ReceiveInput(Func<Vector2> movementInput, Func<float> rotationInput)
    {
        m_MovementInputFunc = movementInput;
        m_RottationInputFunc = rotationInput;
    }
    
    public virtual void ReceiveInput(Vector2 movementInput, float rotationInput)
    {
        if (Mathf.Abs(movementInput.sqrMagnitude) > k_InputTolerance)
        {
            m_MovementInput = movementInput;
        }

        if (Mathf.Abs(rotationInput) > k_InputTolerance)
        {
            m_RotationInput = rotationInput;
        }
    }

    protected virtual void ApplyInput()
    {
        m_Engine.Move(m_MovementInput);
        m_Engine.Rotate(m_RotationInput);
    }

    protected virtual void ResetInputValues()
    {
        m_MovementInput = new Vector2(0, 0);
        m_RotationInput = 0;
    }
}