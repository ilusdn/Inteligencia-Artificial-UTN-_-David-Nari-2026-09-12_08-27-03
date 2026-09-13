using System.Collections.Generic;
using UnityEngine;

public class FSMAgent: MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    [SerializeField] private float waypointCheckDistance = 0.1f;
    private int currentNode;
    private int direction = 1;

    public float speed = 3f;  
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        IdleState idleState = new IdleState(this);

        _stateMachine.RegisterState(PosibleStates.Test, idleState);
        _stateMachine.ChangeState(PosibleStates.Test);

    }

    private void Update()
    {
        _stateMachine.Update();
    }


    private void PatrolLoop()
    {
        var nextWaypoint = wayPoints[currentNode];

        if (Vector3.Distance(nextWaypoint.position, transform.position) <= waypointCheckDistance)
        {
            currentNode = currentNode + 1 < wayPoints.Count ? currentNode + 1 : 0;
        }
        var dir = nextWaypoint.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;

    }


}
