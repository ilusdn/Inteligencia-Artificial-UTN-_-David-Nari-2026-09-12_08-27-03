using UnityEngine;

public class PackAgent : Agent
{
    [SerializeField] protected float _seprarationRadius = 2f;
    
    [SerializeField, Range(0f, 3f)] private float _separationWeight = 1f;
    [SerializeField, Range(0f, 3f)] private float _cohesionWeight = 1f;
    [SerializeField, Range(0f, 3f)] private float _alignmentWeight = 1f;


    protected override void Update()
    {
        base.Update();
    }


    public void PackMovement()
    {
        _velocity += Flocking() + SteeringVector();
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        transform.position += _velocity * Time.deltaTime;

        if (_velocity != Vector3.zero)
            transform.forward = _velocity;

        transform.position = Bounds.Instance.OutOfBounds(transform.position);
    }

    protected Vector3 CalculateSeparation() 
    {        
        if (detector.NearAgents.Count == 0) return Vector3.zero;

        Vector3 desired = default;
        foreach(var item in detector.NearAgents)
        {
            if (Vector3.Distance(item.transform.position, transform.position) <= _seprarationRadius)
            {
                desired += (item.transform.position - transform.position);
            }
        }

        desired /= detector.NearAgents.Count;

        return CalculateSteering(-desired.normalized * _maxSpeed);
    }

    protected Vector3 CalculateAlignment()
    {
        if (detector.NearAgents.Count == 0) return Vector3.zero;
        
        Vector3 desired = default;
        foreach (var item in detector.NearAgents)
        {
            desired += item._velocity;
        }
             
        desired /= detector.NearAgents.Count;
       
        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    protected Vector3 CalculateCohesion()
    {
        if (detector.NearAgents.Count == 0) return Vector3.zero;

        Vector3 desired = default;
        foreach (var item in detector.NearAgents)
        {
            desired += item.transform.position;
        }

        desired /= detector.NearAgents.Count;

        return Seek(desired);
    }

    protected Vector3 Flocking()
    {
        return CalculateSeparation() *_separationWeight 
            + CalculateAlignment() *_alignmentWeight 
            + CalculateCohesion() * _cohesionWeight;
    }

} 
