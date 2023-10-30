using System.Linq;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct AsteroidMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Asteroid>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();
        
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (asteroidTransform, asteroid, asteroidEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Asteroid>>().WithAll<Asteroid>()
                     .WithEntityAccess())
        {
            asteroidTransform.ValueRW.Position += asteroid.ValueRO.Direction * config.AsteroidSpeed * deltaTime;
        }
        
        float screenWidth = 9.1f;
        float screenHeight = 5.25f;
        
        // Move asteroids to the reverse side if they go out of bounds
        foreach (var (asteroidTransform, asteroid, asteroidEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Asteroid>>().WithAll<Asteroid>()
                     .WithEntityAccess())
        {
            if (asteroidTransform.ValueRO.Position.y < -screenHeight)
                asteroidTransform.ValueRW.Position = new float3(asteroidTransform.ValueRO.Position.x, screenHeight, 0);
            
            else if (asteroidTransform.ValueRO.Position.y > screenHeight)
                asteroidTransform.ValueRW.Position = new float3(asteroidTransform.ValueRO.Position.x, -screenHeight, 0);
            
            else if (asteroidTransform.ValueRO.Position.x < -screenWidth)
                asteroidTransform.ValueRW.Position = new float3(screenWidth, asteroidTransform.ValueRO.Position.y, 0);
            
            else if (asteroidTransform.ValueRO.Position.x > screenWidth)
                asteroidTransform.ValueRW.Position = new float3(-screenWidth, asteroidTransform.ValueRO.Position.y, 0);
        }
    }
}

[WithAll(typeof(Asteroid))]
[BurstCompile]
public partial struct AsteroidMovementJob : IJobEntity
{
    public float DeltaTime;
    public float AsteroidSpeed;

    public void Execute(ref LocalTransform transform)
    {
        transform.Position = new float3(
            transform.Position.x,
            transform.Position.y - AsteroidSpeed * DeltaTime,
            0);
    }
}
