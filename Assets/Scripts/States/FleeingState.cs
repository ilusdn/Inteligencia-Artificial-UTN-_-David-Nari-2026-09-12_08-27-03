using UnityEngine;

public class FleeingState : State
{
    private Gacela _agent;

    private float _scapeTimer = 0f;
    private float _scapeLapse = 10f;

    private float _scapetDistance = 25f;

    private ITargetable _menace;

    public FleeingState(Gacela agent)
    {
        _agent = agent;
    }

    public override void Enter()
    {
        _menace = _agent.target;
        _agent.SetBasicMovement(PublicEnums.SteeringModes.Evade, _menace);
        _agent.ui.SetIcon(UIManager.StateIcon.Danger);
    }

    public override void Tick()
    {
        if ((_agent.transform.position - _menace.Position).magnitude < _scapetDistance)
        {
            _agent.BasicMovement();
        }
        else
        {
            _agent._fsm.ChangeState(PublicEnums.GacelaStates.Wander);
        }
    }
    public override void Exit()
    {

    }

}
