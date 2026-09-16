using UnityEngine;

public class GacelaDetection : DetectorAgent
{
    protected Gacela OwnerAsGacela => owner as Gacela;
    protected override void OnTriggerEnter(Collider collider)
    {
        Agent agent = collider.GetComponentInParent<Agent>();
        if (agent != null)
        {
            if (owner.isMenace(agent._creatureType))
                OwnerAsGacela.FoundMenace(agent);
            else
                NearAgents.Add(agent);
        }

        IEdible food = collider.GetComponentInParent<IEdible>();
        if (food != null)
            OwnerAsGacela.FindFood(food);

    }

    protected override void OnTriggerExit(Collider collider)
    {
        Agent agent = collider.GetComponentInParent<Agent>();

        if (agent != null)
            NearAgents.Remove(agent);
    }

    protected void OnTriggerStay(Collider collider)
    {
        Agent agent = collider.GetComponentInParent<Agent>();
        if (agent != null)
        {
            if (owner.isMenace(agent._creatureType))
                OwnerAsGacela.FoundMenace(agent);
        }
    }



}
