using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public partial struct EnemyMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemySpeed>();
        state.RequireForUpdate<LocalTransform>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Vector3 playerPos = SystemAPI.GetSingleton<PlayerPosition>().pos;
        
        foreach ((RefRW<LocalTransform> enemyTransform, RefRO<EnemySpeed> enemySpeed) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnemySpeed>>())
        {
            Vector3 Movedirection =  playerPos - (Vector3)enemyTransform.ValueRO.Position;
            
            Movedirection.Normalize();

            enemyTransform.ValueRW.Position += new float3(Movedirection * enemySpeed.ValueRO.speed*SystemAPI.Time.DeltaTime);
        }
        
        
    }
}
