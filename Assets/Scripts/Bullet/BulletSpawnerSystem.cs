using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public partial struct BulletSpawnerSystem : ISystem
{
    private float _timeSinceLastSpawn;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
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
        SystemAPI.GetComponentRW<LocalTransform>(bullet).ValueRW.Position = new float3(playerTransform.ValueRO.Position.x, playerTransform.ValueRO.Position.y + 0.5f, 0);
        
        _timeSinceLastSpawn = 0;
    }
}
