using System.Collections;
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
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation trans, ref MovementComponentData moveData) =>
        {
            trans.Value.x += deltaTime * moveData.speed;

            //if (moveData.directionLook.x != -1)
            //{
            //    moveData.directionLook = new int2 (-1, moveData.directionLook.y);
            //}

        }).Run();

        return inputDeps;
    }
}
