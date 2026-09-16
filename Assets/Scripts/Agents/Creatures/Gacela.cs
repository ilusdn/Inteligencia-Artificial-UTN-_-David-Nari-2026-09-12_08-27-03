using UnityEngine;


public class Gacela : PackAgent, IDamageable
{
    [SerializeField] public int _maxHealth = 100;
    [SerializeField] public int _currentHealth = 100;
    [SerializeField] private float _respawnTime= 10f;

    public bool _isDead;
    public bool _canRecolect { get; private set; }

    public IEdible _foodTarget;

    public void Awake()
    {
        _fsm = new StateMachine();

        WanderState wanderState = new WanderState(this);
        DeadState deadSteate = new DeadState(this);
        RespawnState respawnState = new RespawnState(this, _respawnTime);
        FeedingState feedingState = new FeedingState(this);
        FleeingState fleeingState = new FleeingState(this); 

        _fsm.RegisterState(PublicEnums.GacelaStates.Wander, wanderState);
        _fsm.RegisterState(PublicEnums.GacelaStates.Dead, deadSteate);
        _fsm.RegisterState(PublicEnums.GacelaStates.Respawn, respawnState);
        _fsm.RegisterState(PublicEnums.GacelaStates.Feeding, feedingState);
        _fsm.RegisterState(PublicEnums.GacelaStates.Fleeing, fleeingState);
    }

    public void Start()
    {
        _currentHealth = _maxHealth;
        _isDead = false;
        _fsm.ChangeState(PublicEnums.GacelaStates.Wander);
    }
    public bool IsDepleted => _currentHealth <= 0f;
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        ui.SetHealth(_currentHealth);
        if (IsDepleted)
        {
            _isDead = true;
            _fsm.ChangeState(PublicEnums.GacelaStates.Dead);
        }
    }

    public void FindFood(IEdible food)
    {
        if (_foodTarget != null && !_foodTarget.IsDepleted) return;

        _foodTarget = food;
        _fsm.ChangeState(PublicEnums.GacelaStates.Feeding);
    }

    public void FoundMenace(Agent menace)
    {
        target = menace; 
        _fsm.ChangeState(PublicEnums.GacelaStates.Fleeing);
    }

    public void WasGathered()
    {
        _fsm.ChangeState(PublicEnums.GacelaStates.Respawn);
    }
}
