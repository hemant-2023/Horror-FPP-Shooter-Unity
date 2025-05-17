using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState _activeState;

    public void Initialise()
    {
        // Set default state to Patrol
        ChangeState(new PatrolState());
    }

    void Update()
    {
        _activeState?.Perform(); // C# shortcut for null check
    }

    public void ChangeState(BaseState _newState)
    {
        _activeState?.Exit(); // Clean up old state

        _activeState = _newState;

        if (_activeState != null)
        {
            _activeState._stateMachine = this;
            _activeState._enemy = GetComponent<Enemy>();
            _activeState.Enter();
        }
    }
}
