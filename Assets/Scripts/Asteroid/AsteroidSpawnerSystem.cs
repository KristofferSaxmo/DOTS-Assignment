using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public partial struct AsteroidSpawnerSystem : ISystem
{
    private float _spawnCooldown;
    private bool _spawnTop;
    private bool _spawnLeft;
    private int _wave;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Return if 1 or more asteroids exists in scene. Really ugly I just couldn't figure out how to do it properly.
        foreach (var (asteroidTransform, asteroidPrefab) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Asteroid>()
                     .WithEntityAccess())
        {
            return;
        }
        
        var config = SystemAPI.GetSingleton<Config>();
        var dt = SystemAPI.Time.DeltaTime;
        
        _spawnCooldown += dt;

        if (_spawnCooldown < config.AsteroidSpawnRate) return;
        
        _wave++;
        
        // wave = n
        // config.AsteroidSpawnAmount = x
        // (x * n) + n^2
        
        int spawnAmount = config.AsteroidSpawnAmount * _wave + _wave*_wave;
        
        for (int i = 0; i < spawnAmount; i++)
        {
            Entity asteroid = state.EntityManager.Instantiate(config.AsteroidPrefab);
            float xPos;
            float yPos;
            float3 direction;
            float screenWidth = 9.1f;
            float screenHeight = 5.20f;
            
            if (_spawnTop)
            {
                yPos = screenHeight;
                direction = math.normalize(new float3(Random.Range(-0.9f, 0.9f), Random.Range(-0.1f, -1.0f), 0));
            }
            else
            {
                yPos = -screenHeight;
                direction = math.normalize(new float3(Random.Range(-0.9f, 0.9f), Random.Range(0.1f, 1.0f), 0));
            }
            
            xPos = Random.Range(-8.0f, 8.0f);

            _spawnTop = !_spawnTop;
            SystemAPI.GetComponentRW<LocalTransform>(asteroid).ValueRW.Position = new float3(xPos, yPos, 0);
            SystemAPI.GetComponentRW<Asteroid>(asteroid).ValueRW.Direction = direction;
        }
        _spawnCooldown = 0;
    }
}
