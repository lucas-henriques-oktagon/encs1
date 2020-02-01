using Unity.Entities;
using Unity.Mathematics;

public struct DirectionData : IComponentData
{
    public int2 directionLook;
}

public struct MovementComponentData : IComponentData
{
    public float speed;
}