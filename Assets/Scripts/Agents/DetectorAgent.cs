using System.Collections.Generic;
using UnityEngine;

public class DetectorAgent : MonoBehaviour
{
        [SerializeField] private LayerMask detectLayer;
        [SerializeField] protected Agent owner; 
        public List<Agent> NearAgents { get; } = new();

        protected virtual void OnTriggerEnter(Collider collider){ }

        protected virtual void OnTriggerExit(Collider collider){ }    
}
    