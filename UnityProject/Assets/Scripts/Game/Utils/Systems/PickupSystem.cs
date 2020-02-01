using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class S_PickupSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithAll<C_CooldownComponent>().ForEach((Entity entity, int entityInQueryIndex, ) =>
        {
            
        })).Schedule(inputDeps);
        
        throw new System.NotImplementedException();
    }
}
