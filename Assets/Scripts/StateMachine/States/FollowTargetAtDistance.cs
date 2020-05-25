using UnityEngine;

public class FollowTargetAtDistance : State
{
    private readonly Transform m_Target;
    private readonly Transform m_MyTransform;
    private readonly IEngineInputHandler m_EngineInputHandler;
    private readonly float m_DistanceMin;
    private readonly float m_DistanceMax;

    private float m_SecondsToRetreatAfter = 30;
    private readonly float m_TimeHolder;

    public FollowTargetAtDistance(StateManager stateManager, Transform target, Transform myTransform,
                                  IEngineInputHandler engineInputHandler, float minDistance,
                                  float maxDistance) : base(stateManager)
    {
        m_Target = target;
        m_MyTransform = myTransform;
        m_EngineInputHandler = engineInputHandler;
        m_DistanceMin = minDistance;
        m_DistanceMax = maxDistance;

        m_TimeHolder = Time.time + m_SecondsToRetreatAfter;
    }

    public override void Enter()
    {
        base.Enter();

        m_EngineInputHandler.ListenToInput(GetMoveDirection, () => 0);
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= m_TimeHolder)
        {
            m_StateManager.ChangeState(new RetreatFromTarget(m_StateManager, m_Target, m_MyTransform,
                                                             m_EngineInputHandler));
        }
    }

    private Vector2 GetMoveDirection()
    {
        Vector2 returnValue;
        Vector2 dirToTarget = m_Target.position - m_MyTransform.position;
        // TODO: Need to change dirToTargetNormalized depending on how far the player is
        Vector2 dirToTargetNormalized = dirToTarget.normalized;
        float distance = dirToTarget.magnitude;
        // dirToTargetNormalized *= Lerp(0, 1, distance);

        if (distance < m_DistanceMin)
        {
            returnValue = dirToTargetNormalized * -1;
        } else if (distance > m_DistanceMax)
        {
            returnValue = dirToTargetNormalized;
        } else
        {
            returnValue = Random.insideUnitCircle;
        }

        return returnValue;
    }

    public override void Exit()
    {
        base.Exit();
        m_EngineInputHandler.ListenToInput(new Vector2(0, 0), 0);
    }
    
    private float Lerp(float v0, float v1, float t)
    {
        return (1 - t) * v0 + t * v1;
    }
}