using UnityEngine;

public class PublicEnums
{
    public enum SteeringModes { Seek, Flee, Arrive, Pursuit, Evade }

    public enum CreaturesType { Gacela, Humano, Rinoceronte }

    public enum GacelaStates { Wander, Dead, Respawn, Feeding, Fleeing }
    public enum HunterStates { Gathering, Patrol, Attack, Trap}
}
