using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRW<LocalTransform> bulletTransform, RefRO<BulletSpeed> bulletSpeed) in 
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<BulletSpeed>>())
        {
            bulletTransform.ValueRW.Position += bulletTransform.ValueRO.Up() * bulletSpeed.ValueRO.Speed * SystemAPI.Time.DeltaTime;
        }
    }
}
