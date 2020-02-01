using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;

public class JumpingSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float gravity = 2; //use utils

        Entities.ForEach((ref PhysicsVelocity vel, in JumpComponentData jump) =>
        {
            if (vel.Linear.y > 0)
            {
                vel.Linear.y -= deltaTime * gravity;
            }

        }).Run();

        return inputDeps;
    }
}
