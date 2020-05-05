using UnityEngine;
using UnityEngine.Assertions;

public class PlayerShip : MonoBehaviour, IHealth
{
    private IEngineHandler m_Engine;
    private IWeaponsHandler m_WeaponsHandler;
    
    [SerializeField] private float m_MoveForce = 100000;
    [SerializeField] private float m_RotationForce = 100;

    public float Health { get => m_Health; set => m_Health = value; }
    [SerializeField] private float m_Health = 10;
    
    private void Start()
    {
        m_Engine = GetComponent<IEngineHandler>();
        m_WeaponsHandler = GetComponent<IWeaponsHandler>();
        Assert.IsNotNull(m_WeaponsHandler, "IWeaponsHandler hasn't been added.");
        Assert.IsNotNull(m_Engine, "IEngineHandler hasn't been added.");
        
        // TODO: Move somewhere
        IEngine engine = new Rigidbody2DEngine(GetComponent<Rigidbody2D>(), m_MoveForce, m_RotationForce);
        m_Engine.Init(engine);
    }

    private void Update()
    {
        m_Engine.ReceiveInput(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // TODO: I don't think this is the best way to handle this
        if (Input.GetKey(KeyCode.K))
        {
            m_WeaponsHandler.ReceiveInput(0);
        }
        if (Input.GetKey(KeyCode.L))
        {
            m_WeaponsHandler.ReceiveInput(1);
        }
    }
}