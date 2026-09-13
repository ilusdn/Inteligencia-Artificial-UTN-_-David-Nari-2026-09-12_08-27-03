using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


public enum SteeringModes {Seek, Flee, Arrive, Pursuit, Evade }

public class Agent : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Agent target;
    
    [Header ("Stats")]
    [SerializeField] protected float _maxSpeed = 3f;
    [SerializeField] protected float _maxSteering = 3f;
    [SerializeField] private float _slowingDistance = 3f;
    [SerializeField] private float _minDistance = 0.1f;

    public SteeringModes _currentSteering;
    
    protected List<Agent> _nearAgents;
    public Vector3 _velocity;

    private void Awake()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1, 1), 0f, Random.Range(-1, -1));
        _velocity += randomDirection.normalized * _maxSpeed;
    }

    private void Update()
    {
        _velocity += SteeringVector();

        transform.position += _velocity * Time.deltaTime;
        
        if (_velocity != Vector3.zero)
        transform.forward = _velocity;
    }

    private Vector3 SteeringVector()
    {
        switch (_currentSteering)
        {
            case SteeringModes.Seek:
                return Seek(target.transform.position);
            case SteeringModes.Flee:
                return Flee(target.transform.position);
            case SteeringModes.Arrive:
                return Arrival();
            case SteeringModes.Pursuit:
                return Pursuit(target);
            case SteeringModes.Evade:
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
        Vector3 direction = target.transform.position - transform.position;
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


    protected Vector3 CalculateFuture(Agent target)
    {
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        var predictionLapse = distance / (_maxSpeed + target._velocity.magnitude);

        Vector3 futurePosition = target.transform.position + target._velocity * predictionLapse;
        
        return futurePosition;
    }

    protected Vector3 Pursuit(Agent target)
    {
        var futurePosition = CalculateFuture(target);

        return Seek(futurePosition);
    }

    protected Vector3 Evade(Agent target)
    {
        var futurePosition = CalculateFuture(target);

        return Flee(futurePosition);
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Agent>(out Agent agente))
        {
            _nearAgents.Add(agente);
        }
    }

    protected void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Agent>(out Agent agente))
        {
            _nearAgents.Remove(agente);
        }
    }

}
