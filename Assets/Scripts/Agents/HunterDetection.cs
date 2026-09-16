using UnityEngine;


public class HunterDetection : DetectorAgent
{
    protected Hunter OwnerAsHunter => owner as Hunter;
    protected override void OnTriggerEnter(Collider collider)
    {
        Gacela agent = collider.GetComponentInParent<Gacela>();
        if (agent != null)
        {
            if (OwnerAsHunter.isPrey(agent._creatureType))
            {
                OwnerAsHunter.FoundPrey(agent);
            }
        }
    }
}

