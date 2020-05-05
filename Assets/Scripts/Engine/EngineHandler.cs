using UnityEngine;

public class EngineHandler : MonoBehaviour, IEngineHandler
{
    private IEngine m_Rb2DEngine;

    private float m_HorizontalInput;
    private float m_VerticalInput;
    private const float k_InputTolerance = 0.001f;

    public void Init(IEngine engine)
    {
        m_Rb2DEngine = engine;
    }

    private void FixedUpdate()
    {
        m_Rb2DEngine.Move(transform.up * m_VerticalInput);
        m_Rb2DEngine.Rotate(m_HorizontalInput);
    }

    public void ReceiveInput(float horizontal, float vertical)
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
}