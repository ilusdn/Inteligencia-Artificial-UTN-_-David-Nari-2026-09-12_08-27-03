using UnityEditor;
using UnityEngine;

public class FeedingState : State
{
    private Gacela _agent;

    private IEdible _food;
    private float eatingTimer = 0f;

    private float biteDelay = 5f;
    private int biteDmg = 5;


    public FeedingState(Gacela agent)
    {
        _agent = agent;
    }

    public override void Enter()
    {
        _food = _agent._foodTarget;
        _agent.SetBasicMovement(PublicEnums.SteeringModes.Arrive, _food);
        _agent.ui.SetIcon(UIManager.StateIcon.Eat);
    }

    public override void Tick()
    {
        if(_food.IsDepleted)
        {
            _agent._fsm.ChangeState(PublicEnums.GacelaStates.Wander);
            return;
        }
        
        if ((_agent.transform.position - _food.Position).magnitude > 1f)
        {
            _agent.BasicMovement();
        }
        else
        {
            if (eatingTimer < biteDelay)
            {
                eatingTimer += Time.deltaTime;
            }
            else
            {
                Debug.Log("Chomp");
                Eating();
                eatingTimer = 0f;
            }
        }
    }
    public override void Exit()
    {
        
    }

    private void Eating()
    {
        _food.TakeDamage(biteDmg);
    }

}
