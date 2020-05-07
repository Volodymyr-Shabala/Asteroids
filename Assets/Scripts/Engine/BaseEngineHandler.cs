using UnityEngine;
using UnityEngine.Assertions;

public class BaseEngineHandler : MonoBehaviour, IEngineHandler
{
    protected IEngine m_Engine;
    protected float m_HorizontalInput;
    protected float m_VerticalInput;
    protected const float k_InputTolerance = 0.001f;

    private bool m_Initialized;

    public void Init(IEngine engine)
    {
        Assert.IsFalse(m_Initialized, $"Trying to initialize already initialized class in {name}. ");
        m_Engine = engine;
        m_Initialized = true;
    }

    protected virtual void FixedUpdate()
    {
        if (!m_Initialized)
        {
            return;
        }

        ApplyInput();
        ResetInputValues();
    }

    public virtual void ReceiveInput(float horizontal, float vertical)
    {
        if (Mathf.Abs(horizontal) > k_InputTolerance)
        {
            m_HorizontalInput = horizontal;
        }

        if (Mathf.Abs(vertical) > k_InputTolerance)
        {
            m_VerticalInput = vertical;
        }
    }

    protected virtual void ApplyInput()
    {
        m_Engine.Move(transform.up * m_VerticalInput);
        m_Engine.Rotate(m_HorizontalInput);
    }

    protected virtual void ResetInputValues()
    {
        m_HorizontalInput = 0;
        m_VerticalInput = 0;
    }
}