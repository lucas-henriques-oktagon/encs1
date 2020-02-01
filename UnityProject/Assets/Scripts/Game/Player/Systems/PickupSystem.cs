using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

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

        ComponentDataFromEntity<Translation> xesque = GetComponentDataFromEntity<Translation>(true);
        
        JobHandle handle = Entities.
            WithAll<C_CanPick, TC_PickAction>().
            ForEach((Entity entity, int entityInQueryIndex, in Translation translation, in Rotation rotation, in C_CanPick canPick) =>
        {
            int2 Direction = new int2 {x = 1, y = 0};
            float MinLength = translation.Value.x;
            float MaxLength = translation.Value.x + Direction.x;
            
            
            int iClosestEntity = -1;
            float lastDistance = -1;
            for (int i = 0; i < lEntitiesToPickup.Length; i++)
            {
                Translation objTranslation = xesque[lEntitiesToPickup[i]];
                if (!Utils.IsInRange(objTranslation.Value.x, MinLength, MaxLength)) continue;
                var actualDistance = Utils.CalculateDistance(objTranslation.Value.x, translation.Value.x);
                lastDistance = (lastDistance < actualDistance ? actualDistance : lastDistance);
                iClosestEntity = (lastDistance < actualDistance ? i : iClosestEntity);

            }
            
        }).Schedule(inputDeps);

        handle.Complete();
        
        m_EntityCommandBuffer.AddJobHandleForProducer(handle);
        
        lEntitiesToPickup.Dispose(inputDeps);
        return handle;
    }
}
