using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public enum StateIcon { Chasing, Danger, Dead, Eat, Gather, MeleeAttack, Patrol, RangeAttack, Wander }

    [Header("Icons")]
    [SerializeField] private Sprite chasingSprite;
    [SerializeField] private Sprite dangerSprite;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private Sprite eatSprite;
    [SerializeField] private Sprite gatherSprite;
    [SerializeField] private Sprite meleeAttackSprite;
    [SerializeField] private Sprite patrolSprite;
    [SerializeField] private Sprite rangeAttackSprite;
    [SerializeField] private Sprite wanderSprite;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Sprite GetIcon(StateIcon icon)
    {
        return icon switch
        {
            StateIcon.Chasing => chasingSprite,
            StateIcon.Danger => dangerSprite,
            StateIcon.Dead => deadSprite,
            StateIcon.Eat => eatSprite,
            StateIcon.Gather => gatherSprite,
            StateIcon.MeleeAttack => meleeAttackSprite,
            StateIcon.Patrol => patrolSprite,
            StateIcon.RangeAttack => rangeAttackSprite,
            StateIcon.Wander => wanderSprite,
            _ => null
        };
    }
}