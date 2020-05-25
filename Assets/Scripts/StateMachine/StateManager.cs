using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    protected State m_CurrentState;
    
    public void ChangeState(State newState)
    {
        m_CurrentState?.Exit();
        m_CurrentState = newState;
        m_CurrentState.Enter();
    }
}