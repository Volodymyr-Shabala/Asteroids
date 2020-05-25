using UnityEngine;

public class RetreatFromTarget : State
{
    private readonly Transform m_Target;
    private readonly Transform m_MyTransform;
    private readonly IEngineInputHandler m_EngineInputHandler;

    public RetreatFromTarget(StateManager stateManager, Transform target, Transform myTransform,
                             IEngineInputHandler engineInputHandler) : base(stateManager)
    {
        m_Target = target;
        m_MyTransform = myTransform;
        m_EngineInputHandler = engineInputHandler;
    }

    public override void Enter()
    {
        base.Enter();

        m_EngineInputHandler.ListenToInput(GetMoveDirection, () => 0);
    }

    private Vector2 GetMoveDirection()
    {
        return (m_MyTransform.position - m_Target.position).normalized;
    }
}