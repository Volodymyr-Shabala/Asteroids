public abstract class State
{
    protected readonly StateManager m_StateManager;
    
    public State(StateManager stateManager)
    {
        m_StateManager = stateManager;
    }
    
    public virtual void Enter(){}
    public virtual void Update(){}
    public virtual void Exit(){}
}