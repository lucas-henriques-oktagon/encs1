using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct C_PlayerInput : IComponentData
{
    public int horizontal;
    public int action;
    public int jump;
}