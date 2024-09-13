using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthor : MonoBehaviour
{
    public float PlayerSpeed;

    public float PlayerRotationSpeed;

    public GameObject BulletPrefab;
    
    private class PlayerAuthorBaker : Baker<PlayerAuthor>
    {
        public override void Bake(PlayerAuthor authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerMovementSpeed(){speed = authoring.PlayerSpeed, RotationSpeed = authoring.PlayerRotationSpeed});
            
            AddComponent(entity, new BulletPrefab{bullet = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic)});
            
            AddComponent(entity, new ShootThisFrame{shoot = false});
            
            AddComponent(entity, new PlayerTag());
            
            AddComponent(entity, new PlayerPosition());
            
            
        }
    }
    
}

public struct PlayerMovementSpeed : IComponentData
{
    public float speed;
    public float RotationSpeed;
}

public struct PlayerMoveInputValue : IComponentData
{
    public Vector2 moveValue;
}

public struct BulletPrefab : IComponentData
{
    public Entity bullet;
}

public struct ShootThisFrame : IComponentData
{
    public bool shoot;
}

public struct PlayerTag : IComponentData
{
    
}

public struct PlayerPosition : IComponentData
{
    public float3 pos;
}