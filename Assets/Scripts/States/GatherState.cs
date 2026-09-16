using Unity.VisualScripting;
using UnityEngine;

public class GatherState : State
{
    private Hunter _agent;
    public GatherState (Hunter agent)
    {
        _agent = agent;
    }

    private float gatherTimer = 0f;
    private float gatherDelay = 5f;

    public override void Enter()
    {
        _agent.ui.SetIcon(UIManager.StateIcon.Chasing);
        _agent.SetBasicMovement(PublicEnums.SteeringModes.Arrive, _agent._prey);
        _agent._gathering = true;
        gatherTimer = 0f;
    }

    public override void Tick()
    {
        if ((_agent.transform.position - _agent._prey.Position).magnitude > 2f)
        {
            _agent.BasicMovement();
        }
        else
        {
            _agent.ui.SetIcon(UIManager.StateIcon.Gather);
            if (gatherTimer < gatherDelay)
            {
                gatherTimer += Time.deltaTime;
            }
            else
            {
                _agent._prey.WasGathered();
                _agent._gathering = false;
                _agent._fsm.ChangeState(PublicEnums.HunterStates.Patrol);
            }
        }
    }



}