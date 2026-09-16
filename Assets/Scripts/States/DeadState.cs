using UnityEngine;

public class DeadState : State
{

    private Agent _agent;

    public DeadState(Agent agent)
    {
        _agent = agent;
    }


    public override void Enter()
    {
        // Animación o algo?
        _agent.ui.SetIcon(UIManager.StateIcon.Dead);
        _agent.detector.gameObject.SetActive(false);
    }

    public override void Tick()
    {
        // Espero a ser recolectado
    }
    public override void Exit() 
    {

    }

  

}
      
