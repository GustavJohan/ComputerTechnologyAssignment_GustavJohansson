using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Burst;

public partial struct BulletLifetimeSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BulletLifetime>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer buffer = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        
        foreach ((RefRW<BulletLifetime> bulletLifetime, Entity entity) in 
                 SystemAPI.Query<RefRW<BulletLifetime>>().WithEntityAccess())
        {
            bulletLifetime.ValueRW.LifeTime -= SystemAPI.Time.DeltaTime;

            if (bulletLifetime.ValueRO.LifeTime < 0)
            {
                
                
                buffer.AddComponent<DeleteObject>(entity);
                
            }
        }
        
        buffer.Playback(state.EntityManager);
        buffer.Dispose();
    }
}
