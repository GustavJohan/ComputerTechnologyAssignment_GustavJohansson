using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = System.Numerics.Quaternion;

public partial class PlayerMovementSystem : SystemBase
{
    private PlayerInput MoveInput;
    private Entity Player;

    
    protected override void OnCreate()
    {
        RequireForUpdate<PlayerMovementSpeed>();

        MoveInput = new PlayerInput();
        
        MoveInput.Enable();

        
    }

    protected override void OnStartRunning()
    {
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();

        if (Player == null)
        {
            Debug.Log("Player is identified");
        }

    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        float2 MoveInput = this.MoveInput.PlayerActions.MoveAction.ReadValue<Vector2>();

        foreach ((RefRW<LocalTransform> transform, RefRO<PlayerMovementSpeed> playerMovementSpeed) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerMovementSpeed>>())
        {
            transform.ValueRW.Position.xy += transform.ValueRW.Up().xy *
                                             playerMovementSpeed.ValueRO.speed * MoveInput.y * SystemAPI.Time.DeltaTime;


             quaternion rotation = transform.ValueRO.Rotation;
             rotation = math.mul(rotation, quaternion.AxisAngle(new float3(0, 0, 1),
                 MoveInput.x * SystemAPI.Time.DeltaTime * -playerMovementSpeed.ValueRO.RotationSpeed));
             transform.ValueRW.Rotation = rotation;

             SystemAPI.SetSingleton(new PlayerPosition{pos = transform.ValueRO.Position});
        }
    }
}
