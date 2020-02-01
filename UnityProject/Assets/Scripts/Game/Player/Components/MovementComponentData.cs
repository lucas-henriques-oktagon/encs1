using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MovementComponentData : IComponentData
{
    public float speed;
    public int2 directionLook;
}
