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

        
        if (_timeSinceLastSpawn >= config.AsteroidSpawnRate)
        {
            Entity asteroid = state.EntityManager.Instantiate(config.AsteroidPrefab);
            float xPos = Random.Range(-8.0f, 8.0f);
            SystemAPI.GetComponentRW<LocalTransform>(asteroid).ValueRW.Position = new float3(xPos, 5.25f, 0);
            _timeSinceLastSpawn = 0;
        }
        else
        {
            _timeSinceLastSpawn += dt;
        }
    }
}
