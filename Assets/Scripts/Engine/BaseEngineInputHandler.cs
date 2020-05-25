using System;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseEngineInputHandler : MonoBehaviour, IEngineInputHandler
{
    protected IEngine m_Engine;

    protected Func<Vector2> m_MovementInputFunc;
    protected Func<float> m_RotationInputFunc;
    protected Vector2 m_MovementInput;
    protected float m_RotationInput;
    protected const float k_InputTolerance = 0.001f;

    protected bool m_InputAssigned;

    protected virtual void Awake()
    {
        m_Engine = GetComponent<IEngine>();
        Assert.IsNotNull(m_Engine, $"No IEngine has been found on this GameObject. {name} requires it.");
    }

    protected void Update()
    {
        if (!m_InputAssigned)
        {
            return;
        }

        Vector2 movementInput = m_MovementInputFunc();
        if (movementInput.sqrMagnitude > k_InputTolerance)
        {
            m_MovementInput = movementInput;
        }

        float rotationInput = m_RotationInputFunc();
        if (Mathf.Abs(rotationInput) > k_InputTolerance)
        {
            m_RotationInput = rotationInput;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!m_InputAssigned)
        {
            return;
        }

        ApplyInput();
        ResetInputValues();
    }

    public virtual void ListenToInput(Func<Vector2> movementInput, Func<float> rotationInput)
    {
        m_InputAssigned = true;
        m_MovementInputFunc = movementInput;
        m_RotationInputFunc = rotationInput;
    }

    public virtual void ListenToInput(Vector2 movementInput, float rotationInput)
    {
        m_InputAssigned = true;
        m_MovementInputFunc = () => movementInput;
        m_RotationInputFunc = () => rotationInput;
    }

    protected virtual void ApplyInput()
    {
        if (m_MovementInput.sqrMagnitude > k_InputTolerance)
        {
            m_Engine.Move(m_MovementInput);
        }

        if (Mathf.Abs(m_RotationInput) > k_InputTolerance)
        {
            m_Engine.Rotate(m_RotationInput);
        }
    }

    protected virtual void ResetInputValues()
    {
        m_MovementInput = new Vector2(0, 0);
        m_RotationInput = 0;
    }
}