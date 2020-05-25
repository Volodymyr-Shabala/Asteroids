using UnityEngine;

public class UFOStateMachine : StateManager
{
    [SerializeField] private Vector2 m_MinMaxDistance = new Vector2(10, 25);
    private Transform m_Target;
    private IEngineInputHandler m_EngineInputHandler;
    
    private void Start()
    {
        PlayerShipHealth player = FindObjectOfType<PlayerShipHealth>();
        m_Target = player == null ? FindObjectOfType<ObjectPool>().transform : player.transform;

        m_EngineInputHandler = GetComponent<IEngineInputHandler>();
        ChangeState(new FollowTargetAtDistance(this, m_Target, transform, m_EngineInputHandler, m_MinMaxDistance.x,
                                               m_MinMaxDistance.y));

    }

    private void Update()
    {
        m_CurrentState.Update();
    }
}