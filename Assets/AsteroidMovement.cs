using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct AsteroidMovement : ISystem
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
        var config = SystemAPI.GetSingleton<Config>();

        var dt = SystemAPI.Time.DeltaTime;

        foreach (var asteroidTransform in
                 SystemAPI.Query<RefRW<LocalTransform>>()
                     .WithAll<Asteroid>())
        {
            asteroidTransform.ValueRW.Position = new float3(
                asteroidTransform.ValueRO.Position.x,
                asteroidTransform.ValueRO.Position.y - config.AsteroidSpeed * dt,
                0);
            
        }
    }
}
