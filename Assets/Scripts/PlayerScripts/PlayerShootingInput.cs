using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

using Unity.Transforms;

public partial class PlayerShootingInput  : SystemBase
{
    private PlayerInput playerShooting;
    
    protected override void OnCreate()
    {
        playerShooting = new PlayerInput();
        
        playerShooting.Enable();

        playerShooting.PlayerActions.Shoot.performed += spawnBullet;
    }

    [BurstCompile]
    void spawnBullet(InputAction.CallbackContext callbackContext)
    {
        foreach ((RefRW<ShootThisFrame> shootThisFrame, RefRO<PlayerTag> tag) in
                 SystemAPI.Query<RefRW<ShootThisFrame>, RefRO<PlayerTag>>())
        {
            shootThisFrame.ValueRW.shoot = true;
        }
    }

    protected override void OnUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
