using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletAuthor : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifetime;
    
    private class BulletAuthorBaker : Baker<BulletAuthor>
    {
        public override void Bake(BulletAuthor authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BulletSpeed{Speed = authoring.bulletSpeed});
            AddComponent(entity, new BulletLifetime{LifeTime = authoring.bulletLifetime});
        }
    }

    
}

public struct BulletSpeed : IComponentData
{
    public float Speed;
}

public struct BulletLifetime : IComponentData
{
    public float LifeTime;
}