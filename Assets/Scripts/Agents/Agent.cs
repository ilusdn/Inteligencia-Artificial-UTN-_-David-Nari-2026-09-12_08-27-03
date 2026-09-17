using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, ITargetable
{
    [Header("Stats")]
    [SerializeField] public PublicEnums.CreaturesType _creatureType;
    [SerializeField] public float _maxSpeed = 3f;
    [SerializeField] protected float _maxSteering = 3f;
    [SerializeField] private float _slowingDistance = 3f;
    [SerializeField] private float _minDistance = 0.1f;
    public Vector3 _velocity;

    [Header ("Sensors")]
    [SerializeField] public DetectorAgent detector;
    [SerializeField] protected List<PublicEnums.CreaturesType> treatCreatures;
    public PublicEnums.SteeringModes _currentSteering;
    
    [Header ("References")]
    [SerializeField] public ITargetable target;
    [SerializeField] public CharacterUI ui;
    [SerializeField] public GameObject body;

    public Vector3 Position => transform.position;
    public Vector3 Velocity => _velocity;

    public StateMachine _fsm;

    protected virtual void Update()
    {
       _fsm.Update();
    }

    public void BasicMovement()
    {
        _velocity += SteeringVector();

        transform.position += _velocity * Time.deltaTime;

        if (_velocity != Vector3.zero)
            transform.forward = _velocity * Time.deltaTime;

        transform.position = Bounds.Instance.OutOfBounds(transform.position);
    }

    public void SetBasicMovement(PublicEnums.SteeringModes steeringMode, ITargetable Target)
    {
        _currentSteering = steeringMode;
        target = Target;
    }

    protected Vector3 SteeringVector()
    {
        switch (_currentSteering)
        {
            case PublicEnums.SteeringModes.Seek:
                return Seek(target.Position);
            case PublicEnums.SteeringModes.Flee:
                return Flee(target.Position);
            case PublicEnums.SteeringModes.Arrive:
                return Arrival();
            case PublicEnums.SteeringModes.Pursuit:
                return Pursuit(target);
            case PublicEnums.SteeringModes.Evade:
                return Evade(target);
            default:
                return Vector3.zero;
        }
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxSteering * Time.deltaTime);
        return steering;
    }

    private Vector3 DesiredVector (Vector3 target)
    {
        Vector3 desired = (target - transform.position).normalized;
        desired *= _maxSpeed;
        return desired;
    }

    protected Vector3 Seek( Vector3 target)
    {
        var desired = DesiredVector(target);
        return CalculateSteering(desired);
    }

    protected Vector3 Flee(Vector3 target)
    {
        var desired = DesiredVector(target);        
        return CalculateSteering(-desired);
    }

    protected Vector3 Arrival()
    {
        Vector3 direction = target.Position - transform.position;
        float distance = direction.magnitude;

        if (distance < _minDistance)
        {
            return Vector3.zero;      
        }

        float targetSpeed = _maxSpeed * (distance / _slowingDistance);
        float desiredSpeed = Mathf.Min(targetSpeed, _maxSpeed);

        Vector3 desired = direction.normalized * desiredSpeed;
       
        return CalculateSteering(desired);
    }


    protected Vector3 CalculateFuture(ITargetable target)
    {
        Vector3 direction = target.Position - transform.position;
        float distance = direction.magnitude;

        var predictionLapse = distance / (_maxSpeed + target.Velocity.magnitude);

        Vector3 futurePosition = target.Position + target.Velocity * predictionLapse;

        return futurePosition;
    }

    protected Vector3 Pursuit(ITargetable target)
    {
        var futurePosition = CalculateFuture(target);

        return Seek(futurePosition);
    }

    protected Vector3 Evade(ITargetable target)
    {
        var futurePosition = CalculateFuture(target);

        return Flee(futurePosition);
    }


    public bool isMenace(PublicEnums.CreaturesType creature)
    {
        foreach (PublicEnums.CreaturesType enemigo in treatCreatures)
        {
            if (enemigo == creature)
                return true;
        }
        return false;
    }

}
