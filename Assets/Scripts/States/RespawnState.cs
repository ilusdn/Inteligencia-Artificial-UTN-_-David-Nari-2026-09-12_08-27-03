using UnityEngine;


public class RespawnState : State
{
    private Gacela _agent;
    private float _timer;
    private float _respawnTime = 10f;

    public RespawnState(Gacela agent, float respawnTime)
    {
        _agent = agent;
        _respawnTime = respawnTime;
    }

    public override void Enter()
    {
        _timer = _respawnTime;
        _agent.body.SetActive(false);
        _agent.GetComponent<CapsuleCollider>().enabled = false;
    }

    public override void Tick()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
            _agent._fsm.ChangeState(PublicEnums.GacelaStates.Wander); 
    }

    public override void Exit() 
    {
        Respawn();
    }

    public void Respawn()
    {
        _agent.transform.position = new Vector3(
            Random.Range(-Bounds.Instance.Width / 2, Bounds.Instance.Width / 2),
            0,
            Random.Range(-Bounds.Instance.Height / 2, Bounds.Instance.Height / 2)
            );
        _agent.body.SetActive(true);
        _agent.detector.gameObject.SetActive(true);
        _agent.GetComponent<CapsuleCollider>().enabled = true;
        _agent._currentHealth = _agent._maxHealth;
        _agent.ui.SetHealth(_agent._currentHealth);
        _agent._isDead = false;
    }

}
