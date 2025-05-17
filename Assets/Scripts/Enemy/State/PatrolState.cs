using UnityEngine;

public class PatrolState : BaseState
{
    public int _wayPointIndex;
    public float _waitTimer;

    public override void Enter()
    {
        // Set the initial destination when entering the patrol state
        _enemy.Agent.SetDestination(_enemy._path._waypoint[_wayPointIndex].position);
    }

    public override void Perform()
    {
        PatrolCycle();
        if(_enemy.CanSeePlayer())
        {
            _stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
        // Cleanup or reset logic if needed
    }

    public void PatrolCycle()
    {
        if (_enemy.Agent.remainingDistance < 0.2f)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer > 3)
            {
                if (_wayPointIndex < _enemy._path._waypoint.Count - 1)
                    _wayPointIndex++;
                else
                    _wayPointIndex = 0;

                _enemy.Agent.SetDestination(_enemy._path._waypoint[_wayPointIndex].position);
                _waitTimer = 0;
            }
        }
    }
}
