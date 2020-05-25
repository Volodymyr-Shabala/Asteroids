using UnityEngine;
using UnityEngine.Assertions;

public class PlayerEngineInput : MonoBehaviour
{
    private IEngineInputHandler m_EngineInputHandler;

    private void Start()
    {
        m_EngineInputHandler = GetComponentInChildren<IEngineInputHandler>();
        Assert.IsNotNull(m_EngineInputHandler, $"Couldn't locate IEngineInputHandler. {name} requires it.");
        m_EngineInputHandler.ListenToInput(() => transform.up * Input.GetAxis("Vertical"),
                                     () => -Input.GetAxis("Horizontal"));
    }
}