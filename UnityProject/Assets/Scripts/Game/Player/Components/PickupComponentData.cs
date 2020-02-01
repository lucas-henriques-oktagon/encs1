﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct C_CanPick : IComponentData
{
    public int PickupDistance;
}
public struct TC_Pickable : IComponentData {}

public struct TC_PickAction : IComponentData {}

// This will hold the picked Entity
public struct C_HoldComponentData : IComponentData
{
    public Entity Item;
}
