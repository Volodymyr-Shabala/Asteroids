using UnityEngine;
using UnityEngine.Assertions;

public class PlayerEngineInput : MonoBehaviour
{
    private IEngineHandler m_EngineHandler;
    private void Start()
    {
        m_EngineHandler = GetComponentInChildren<IEngineHandler>();
        Assert.IsNotNull(m_EngineHandler, $"Couldn't locate BaseEngineHandler. {name} requires it.");
    }

    private void Update()
    {
        m_EngineHandler.ReceiveInput(transform.up * Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
    }
}