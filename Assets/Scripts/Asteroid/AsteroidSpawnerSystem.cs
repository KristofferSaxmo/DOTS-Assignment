using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public partial struct AsteroidSpawnerSystem : ISystem
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
        var config = SystemAPI.GetSingleton<Config>();
        var dt = SystemAPI.Time.DeltaTime;
        
        _timeSinceLastSpawn += dt;

        if (_timeSinceLastSpawn < config.AsteroidSpawnRate) return;
        
        for (int i = 0; i < config.AsteroidSpawnAmount; i++)
        {
            Entity asteroid = state.EntityManager.Instantiate(config.AsteroidPrefab);
            float xPos = Random.Range(-8.0f, 8.0f);
            float yPos = Random.Range(5.1f, 5.4f);
            SystemAPI.GetComponentRW<LocalTransform>(asteroid).ValueRW.Position = new float3(xPos, yPos, 0);
        }
        
        _timeSinceLastSpawn = 0;
    }
}
