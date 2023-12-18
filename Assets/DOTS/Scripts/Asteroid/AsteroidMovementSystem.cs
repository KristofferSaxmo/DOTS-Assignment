using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

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

        var movementJob = new AsteroidMovementJob()
        {
            DeltaTime = deltaTime,
            AsteroidSpeed = config.AsteroidSpeed
        };

        movementJob.ScheduleParallel();
    }
}

[WithAll(typeof(Asteroid))]
[BurstCompile]
public partial struct AsteroidMovementJob : IJobEntity
{
    private const float ScreenWidth = 9.1f;
    private const float ScreenHeight = 5.25f;
    public float DeltaTime;
    public float AsteroidSpeed;

    public void Execute(ref LocalTransform transform, ref Asteroid asteroid)
    {
        transform.Position.xy += asteroid.Direction.xy * AsteroidSpeed * DeltaTime;
        
        if (transform.Position.y < -ScreenHeight)
            transform.Position = new float3(transform.Position.x, ScreenHeight, 0);
            
        else if (transform.Position.y > ScreenHeight)
            transform.Position = new float3(transform.Position.x, -ScreenHeight, 0);
            
        else if (transform.Position.x < -ScreenWidth)
            transform.Position = new float3(ScreenWidth, transform.Position.y, 0);
            
        else if (transform.Position.x > ScreenWidth)
            transform.Position = new float3(-ScreenWidth, transform.Position.y, 0);
    }
}