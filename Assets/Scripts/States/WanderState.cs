using UnityEngine;

public class WanderState : State
{
    private PackAgent _agent;
    public WanderState(PackAgent agent) => _agent = agent;

    private float newDirectionTimer = 0f;
    private float newDirectionDelay = 10f;

    private float wanderRadius = 25f;

    public override void Enter()
    {
        ChangeDirection();
        _agent.ui.SetIcon(UIManager.StateIcon.Wander);
    }

    public override void Tick()
    {
        newDirectionTimer += Time.deltaTime;

        if (newDirectionTimer > newDirectionDelay)
        {
            newDirectionTimer = 0f;
            ChangeDirection();
        }

        _agent.PackMovement();

    }

    public override void Exit()
    {

    }

    private void ChangeDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * wanderRadius;
        _agent.SetBasicMovement(PublicEnums.SteeringModes.Arrive, new PointTarget(_agent.Position + randomDirection));        
    }

}
