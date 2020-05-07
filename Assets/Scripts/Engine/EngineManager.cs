using UnityEngine;
using UnityEngine.Assertions;

public class EngineManager : MonoBehaviour
{
    [SerializeField] private BaseEngine m_Engine = null;
    [SerializeField] private BaseEngineHandler m_EngineHandler = null;

    private void Awake()
    {
        Assert.IsNotNull(m_Engine, $"Engine needs to be assigned in {name}");
        Assert.IsNotNull(m_EngineHandler, $"EngineHandler needs to be assigned in {name}");
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        Assert.IsNotNull(rb, $"Rigidbody2D is missing from {name}");

        Transform myTransform = transform;
        Vector3 position = myTransform.position;
        
        BaseEngineHandler baseEngineHandler = Instantiate(m_EngineHandler, position, Quaternion.identity, myTransform);
        BaseEngine engine = Instantiate(m_Engine, position, Quaternion.identity, baseEngineHandler.transform);
       
        baseEngineHandler.Init(engine);
    }
}