using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class DropSystem : JobComponentSystem
{
    
    private EntityQuery m_Query;
    private BeginSimulationEntityCommandBufferSystem m_EntityCommandBuffer;
    protected override void OnCreate()
    {
        m_EntityCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        
    }
    
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = Entities
            .WithAll<TC_PickHoldAction>()
            .ForEach((Entity entity, int entityInQueryIndex, in Translation translation, in C_HoldComponentData canPick, in MovementComponentData movementData) =>
            {
                
            }).Schedule(inputDeps);
        
        
        jobHandle.Complete();

        return jobHandle;

    }

    
    
}
