using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    private Hunter _agent;


    private float MeleeAttackRadius=5f;
    private float MeleeAttackRange = 3f;

    private float RangeAttackRadius = 6f;

    public AttackState(Hunter agent)
    {
        _agent = agent;
    }

    public override void Enter()
    {
        _agent.SetBasicMovement(PublicEnums.SteeringModes.Pursuit, _agent._prey);
        _agent.ui.SetIcon(UIManager.StateIcon.Chasing);
    }

    public override void Tick()
    {
        _agent.ui.SetIcon(UIManager.StateIcon.MeleeAttack);
        float distance = (_agent.transform.position - _agent._prey.Position).magnitude;
        
        if (distance >= MeleeAttackRadius)
        {
            _agent.ui.SetIcon(UIManager.StateIcon.RangeAttack);
            if (distance < RangeAttackRadius)
            {
                if (_agent.TBATimer <= 0)
                    RangeAttack();
            }
            else
            {
                if (distance > 10f)
                {
                    _agent._fsm.ChangeState(PublicEnums.HunterStates.Patrol);
                }
                else
                {
                    _agent.BasicMovement();
                }
            }
        }
        else
        {
            if (distance <= MeleeAttackRange)
            {
                if (_agent.TBATimer <= 0)
                    MeleeAttack();
            }
            else
                _agent.BasicMovement();
        }
           
        if (_agent._prey._isDead)
        {
            _agent._fsm.ChangeState(PublicEnums.HunterStates.Gathering);
        }
 
    }


    public void RangeAttack()
    {        
        Debug.Log("Pum");
        _agent.TBATimer = _agent.TimeBetweenAttacks;
        _agent.ui.setTBATimer(_agent.TBATimer);
        _agent._prey.TakeDamage(_agent.damage);
    }
    public void MeleeAttack()
    {
        Debug.Log("Slash");
        _agent.TBATimer = _agent.TimeBetweenAttacks;
        _agent.ui.setTBATimer(_agent.TBATimer);
        _agent._prey.TakeDamage(_agent.damage*2);
    }

    public override void Exit()
    {

    }
}
