using UnityEngine;
using UnityEngine.Assertions;

// TODO: UFO Should leave after 30 sec
// TODO: State machine
public class UFOEngineInput : MonoBehaviour
{
    // TODO: Better to receive this on Instantiate
    private Transform m_Target = null;
    [SerializeField] private Vector2 m_MinMaxDistance = new Vector2(5, 15);
    private IEngineInputHandler m_EngineInputHandler;

    private void Start()
    {
        PlayerShipHealth player = FindObjectOfType<PlayerShipHealth>();
        m_Target = player == null ? FindObjectOfType<ScreenWrappingManager>().transform : player.transform;

        m_EngineInputHandler = GetComponentInChildren<IEngineInputHandler>();
        Assert.IsNotNull(m_EngineInputHandler, $"Couldn't locate IEngineInputHandler. {name} requires it.");
        m_EngineInputHandler.ListenToInput(GetMoveDirection, () => 0);
    }

    public void SetTarget(Transform newTarget)
    {
        m_Target = newTarget;
    }

    private Vector2 GetMoveDirection()
    {
        Vector2 returnValue;
        Vector2 dirToTarget = m_Target.position - transform.position;
        // TODO: Need to change dirToTargetNormalized depending on how far the player is
        Vector2 dirToTargetNormalized = dirToTarget.normalized;
        float distance = dirToTarget.magnitude;

        if (distance < m_MinMaxDistance.x)
        {
            returnValue = dirToTargetNormalized * -1;
        } else if (distance > m_MinMaxDistance.y)
        {
            returnValue = dirToTargetNormalized;
        } else
        {
            returnValue = Random.insideUnitCircle;
        }

        return returnValue;
    }
}