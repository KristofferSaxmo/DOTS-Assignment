using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSpawnerSystem : ISystem
{
    private float _timeSinceLastSpawn;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Player>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();
        Player player = SystemAPI.GetSingleton<Player>();
        
        _timeSinceLastSpawn += SystemAPI.Time.DeltaTime;
        
        if (!Input.GetKey(KeyCode.Space)) return;
        if (_timeSinceLastSpawn < config.PlayerFireRate) return;
        
        RefRO<LocalTransform> playerTransform = SystemAPI.GetComponentRO<LocalTransform>(player.Entity);
        
        Entity bullet = state.EntityManager.Instantiate(config.BulletPrefab);
        float3 forwardVector = math.mul(playerTransform.ValueRO.Rotation, new float3(0, 1, 0));
        float3 spawnOffset = forwardVector * 0.25f;
        state.EntityManager.SetComponentData(bullet, new LocalTransform
        {
            Position = playerTransform.ValueRO.Position + spawnOffset,
            Rotation = playerTransform.ValueRO.Rotation,
            Scale = 1
        });
        SystemAPI.GetComponentRW<Bullet>(bullet).ValueRW.Direction = forwardVector;
        _timeSinceLastSpawn = 0;
    }
}
