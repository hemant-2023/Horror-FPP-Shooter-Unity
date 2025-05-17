public abstract class BaseState 
{
    //instance of enemy class
    public Enemy _enemy;
    //instance of state machine class
    public StateMachine _stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
