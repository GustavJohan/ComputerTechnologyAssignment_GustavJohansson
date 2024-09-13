using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public partial struct PlayerShootingHandler : ISystem
{
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRO<LocalTransform> transform, RefRO<BulletPrefab> bulletPrefab , RefRW<ShootThisFrame> shootThisFrame) in
                 SystemAPI.Query<RefRO<LocalTransform>, RefRO<BulletPrefab>, RefRW<ShootThisFrame>>())
        {
            if (shootThisFrame.ValueRO.shoot)
            {
                Entity newBullet = state.EntityManager.Instantiate(bulletPrefab.ValueRO.bullet);
                
                LocalTransform BulletTransform = transform.ValueRO;
                
                state.EntityManager.SetComponentData(newBullet, BulletTransform);
                

                shootThisFrame.ValueRW.shoot = false;
            }
            
        }
    }
}
