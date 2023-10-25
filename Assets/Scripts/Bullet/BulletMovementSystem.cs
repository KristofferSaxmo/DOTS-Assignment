using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct BulletMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Bullet>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (bulletTransform, bulletPrefab) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Bullet>()
                     .WithEntityAccess())
        {
            bulletTransform.ValueRW.Position = new float3(
                bulletTransform.ValueRO.Position.x,
                bulletTransform.ValueRO.Position.y + config.BulletSpeed * deltaTime,
                0);
        }
    }
}