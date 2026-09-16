using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class Hunter : Agent
{
    [Header("Sensors")]
    [SerializeField] protected List<PublicEnums.CreaturesType> preyCreatures;

    [SerializeField] private PatrolData dataPatrol;

    [SerializeField] private List<GameObject> _baitPrefabInstances;   
    private List<GameObject> _availableBait = new List<GameObject>();
    private int _activeBaitCount = 0;
    
    [SerializeField] public float TimeBetweenAttacks = 10f;
    public float TBATimer;
    private float BaitTimer = 10f;

    public bool _gathering;
    public Gacela _prey;

    public int damage = 50;


    private void Awake()
    {
        _fsm = new StateMachine();

        PatrolState patrolState = new PatrolState(this, dataPatrol);        
        GatherState gatherState = new GatherState(this);
        AttackState attackState = new AttackState(this);

        _fsm.RegisterState(PublicEnums.HunterStates.Patrol, patrolState);
        _fsm.RegisterState(PublicEnums.HunterStates.Gathering, gatherState);
        _fsm.RegisterState(PublicEnums.HunterStates.Attack, attackState);


        foreach (var bait in _baitPrefabInstances)
        {
            bait.SetActive(false);
            _availableBait.Add(bait);
        }
    }
    public void Start()
    {
        _gathering = false;
        _fsm.ChangeState(PublicEnums.HunterStates.Patrol);
    }

    public void Update()
    {
        base.Update();
        
        if (TBATimer > 0)
        {
            TBATimer -= Time.deltaTime;
        }
    }

    public void SpawnBait()
    {
        if (_availableBait.Count == 0) return;

        GameObject bait = _availableBait[0];
        _availableBait.RemoveAt(0);

        bait.transform.position = transform.position;
        bait.SetActive(true);
    }

    public void ReturnBait(GameObject bait)
    {
        _availableBait.Add(bait);
    }

    public bool isPrey(PublicEnums.CreaturesType creature)
    {
        foreach (PublicEnums.CreaturesType prey in preyCreatures)
        {
            if (prey == creature)
                return true;
        }
        return false;
    }


    public void FoundPrey( Gacela prey)
    {        
        if (!_gathering && TBATimer <= 0f)
        {
            _prey = prey;
            _fsm.ChangeState(PublicEnums.HunterStates.Attack);
        }
    }
}