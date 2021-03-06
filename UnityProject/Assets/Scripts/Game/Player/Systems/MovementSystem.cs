﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics;

[AlwaysSynchronizeSystem]
public class MovementSystem : JobComponentSystem
{

    public BeginSimulationEntityCommandBufferSystem begin;

    protected override void OnCreate()
    {
        begin = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer entityCommandBuffer = begin.CreateCommandBuffer();

        float deltaTime = Time.DeltaTime;

        JobHandle jobHandle = Entities
            .ForEach((Entity entity, int entityInQueryIndex, ref Translation trans, ref MovementComponentData moveData, ref DirectionData directionData, in TC_MovingComponentData movingData) =>
        {
            trans.Value = new float3(trans.Value.x + (deltaTime * moveData.speed * movingData.Value), trans.Value.y, trans.Value.z);
            Debug.Log(trans.Value);
            directionData.directionLook = new int2((int)math.round(moveData.speed), directionData.directionLook.y);

        }).WithBurst().Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
