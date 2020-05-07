using UnityEngine;
using UnityEngine.Assertions;

public class PlayerEngineInput : MonoBehaviour
{
    private BaseEngineHandler m_EngineHandler;
    private bool m_Initialized;
    private void Start()
    {
        m_EngineHandler = GetComponentInChildren<BaseEngineHandler>();
        Assert.IsNotNull(m_EngineHandler, $"Couldn't locate BaseEngineHandler in children of {name}.");
        m_Initialized = true;
    }

    private void Update()
    {
        if (!m_Initialized)
        {
            return;
        }
        
        m_EngineHandler.ReceiveInput(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}