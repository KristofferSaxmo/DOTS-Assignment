using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

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
        Config config = SystemAPI.GetSingleton<Config>();
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (asteroidTransform, asteroidPrefab) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Asteroid>()
                     .WithEntityAccess())
        {
            asteroidTransform.ValueRW.Position = new float3(
                asteroidTransform.ValueRO.Position.x,
                asteroidTransform.ValueRO.Position.y - config.AsteroidSpeed * deltaTime,
                0);
        }
        //var job = new AsteroidMovementJob()
        //{
        //    DeltaTime = SystemAPI.Time.DeltaTime,
        //    AsteroidSpeed = config.AsteroidSpeed
        //};
        //job.ScheduleParallel();
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
