using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct DeletionHandler : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<DeleteObject>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer buffer = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

        
        foreach ((RefRO<DeleteObject> deleteObjectTag, Entity entityToDelete) in SystemAPI.Query<RefRO<DeleteObject>>().WithEntityAccess())
        {
            buffer.DestroyEntity(entityToDelete);
            
        }
        
        
        buffer.Playback(state.EntityManager);
        buffer.Dispose();
    }
}

public struct DeleteObject : IComponentData{}