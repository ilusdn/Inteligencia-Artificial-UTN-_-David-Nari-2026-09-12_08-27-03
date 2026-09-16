using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State 
{
    private Hunter _agent;
    private PatrolData _data;
    private int currentNode;
    // private StateMachine stateMachine; 

    private float baitDelay = 10f;
    private float baitTimer = 0f;

    public PatrolState(Hunter agent, PatrolData data)
    {
        _agent = agent;
        _data = data;
    }

    public override void Enter()
    {
        baitTimer = 0f;
        SetPointTarget(_data.wayPoints[currentNode]);
        _agent.ui.SetIcon(UIManager.StateIcon.Patrol);
    }

    public override void Tick()
    {
        PatrolLoop();
        _agent.BasicMovement();

        if (baitTimer >= baitDelay)
        {
            _agent.SpawnBait();
            baitTimer = 0f;
        }
        else
        {
            baitTimer += Time.deltaTime;
        }
    }

    private void PatrolLoop()
    {  
        if (Vector3.Distance(_data.wayPoints[currentNode].position, _agent.transform.position) <= _data.waypointCheckDistance)
        {
            currentNode = currentNode + 1 < _data.wayPoints.Count ? currentNode + 1 : 0;
            SetPointTarget(_data.wayPoints[currentNode]);
        }   
    }


    private void SetPointTarget(Transform waypoint)
    {
        _agent.SetBasicMovement(PublicEnums.SteeringModes.Seek, new PointTarget(waypoint.position));
    }

}
