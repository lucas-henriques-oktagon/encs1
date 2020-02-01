﻿using UnityEngine;
using System.Collections;
using Unity.Entities;

public struct MonoAnimated_C : IComponentData
{
    public int animator;
}

public struct PlayMonoAnimation_C : IComponentData
{
    public int animation;
}