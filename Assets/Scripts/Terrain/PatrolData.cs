using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolData
{
    public List<Transform> wayPoints = new List<Transform>();
    [SerializeField] public float waypointCheckDistance = 1.5f;

}
