using UnityEngine;

public class PointTarget : ITargetable
{
    public Vector3 Position { get; set; }
    public Vector3 Velocity => Vector3.zero;

    public PointTarget(Vector3 position)
    {
        Position = position;
    }
}
