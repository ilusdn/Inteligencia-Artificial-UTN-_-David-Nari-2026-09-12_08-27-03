using UnityEngine;

public class PackAgent : Agent
{
    [SerializeField] protected float _seprarationRadius = 2f;
    
    [SerializeField, Range(0f, 3f)] private float _separationWeight = 1f;
    [SerializeField, Range(0f, 3f)] private float _cohesionWeight = 1f;
    [SerializeField, Range(0f, 3f)] private float _alignmentWeight = 1f;

    protected Vector3 CalculateSeparation() 
    {
        if (_nearAgents.Count == 0) return Vector3.zero;
        
        Vector3 desired = default;
        foreach(var item in _nearAgents)
        {
            if (Vector3.Distance(item.transform.position, transform.position) <= _seprarationRadius)
            {
                desired += (item.transform.position - transform.position);
            }
        }

        desired /= _nearAgents.Count;

        return CalculateSteering(-desired.normalized * _maxSpeed);
    }

    protected Vector3 CalculateAlignment()
    {
        if (_nearAgents.Count == 0) return Vector3.zero;
        
        Vector3 desired = default;
        foreach (var item in _nearAgents)
        {
            desired += item._velocity;
        }
             
        desired /= _nearAgents.Count;
       
        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    protected Vector3 CalculateCohesion()
    {
        if (_nearAgents.Count == 0) return Vector3.zero;

        Vector3 desired = default;
        foreach (var item in _nearAgents)
        {
            desired += item.transform.position;
        }

        desired /= _nearAgents.Count;

        return Seek(desired);
    }

    protected Vector3 Flocking()
    {
        return CalculateSeparation() *_separationWeight 
            + CalculateAlignment() *_alignmentWeight 
            + CalculateCohesion() * _cohesionWeight;
    }

} 
