using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class PickupSystem : JobComponentSystem
{
    private EntityQuery m_Query;
    private BeginSimulationEntityCommandBufferSystem m_EntityCommandBuffer;
    protected override void OnCreate()
    {
        m_EntityCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        
        m_Query = GetEntityQuery(new EntityQueryDesc {
            All = new ComponentType[] { typeof(Translation), typeof(TC_Pickable) },
        });
        
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<Entity> lEntitiesToPickup = m_Query.ToEntityArray(Allocator.TempJob);
        NativeArray<Translation> entitiesTranslation = m_Query.ToComponentDataArray<Translation>(Allocator.TempJob);
        EntityCommandBuffer.Concurrent commandBuffer = m_EntityCommandBuffer.CreateCommandBuffer().ToConcurrent();
        
        
        JobHandle handle = Entities.
            WithAll<TC_PickHoldAction>().
            ForEach((Entity entity, int entityInQueryIndex, in Translation translation, in C_CanPick canPick, in MovementComponentData movementData) =>
            {
                int2 i2Direction = movementData.directionLook;
                float fMinLength = translation.Value.x;
                float fMaxLength = translation.Value.x + i2Direction.x;
            
                Debug.Log(fMinLength + " - " + fMaxLength);
            
                int iClosestEntity = -1;
                float lastDistance = -1;
                for (int i = 0; i < entitiesTranslation.Length; i++)
                {
                    Translation objTranslation = new Translation {Value = entitiesTranslation[i].Value};
                    if (!Utils.IsInRange(objTranslation.Value.x, fMinLength, fMaxLength)) continue;
                    var actualDistance = Utils.CalculateDistance(objTranslation.Value.x, translation.Value.x);
                    if(lastDistance > actualDistance) continue;
                    lastDistance = actualDistance;
                    iClosestEntity = i;
                }
                
                if(iClosestEntity == -1) {}
                else
                {
                    commandBuffer.RemoveComponent<C_CanPick>(entityInQueryIndex, entity);
                    commandBuffer.AddComponent<C_HoldComponentData>(entityInQueryIndex, entity);
                    commandBuffer.SetComponent(entityInQueryIndex, entity, new C_HoldComponentData
                    {
                        Item = lEntitiesToPickup[iClosestEntity]
                    });

                    commandBuffer.RemoveComponent<TC_Pickable>(entityInQueryIndex, lEntitiesToPickup[iClosestEntity]);
                    commandBuffer.AddComponent<C_InHold>(entityInQueryIndex, lEntitiesToPickup[iClosestEntity]);
                    commandBuffer.SetComponent(entityInQueryIndex, entity, new C_InHold
                    {
                        Owner = entity,
                    });
                }
                commandBuffer.RemoveComponent<TC_PickHoldAction>(entityInQueryIndex, entity);
                
        }).WithoutBurst().Schedule(inputDeps);

        handle.Complete();
        
        m_EntityCommandBuffer.AddJobHandleForProducer(handle);
        
        lEntitiesToPickup.Dispose(inputDeps);
        entitiesTranslation.Dispose(inputDeps);
        return handle;
    }
}
