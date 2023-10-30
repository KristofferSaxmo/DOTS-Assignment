using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;



[UpdateAfter(typeof(TransformSystemGroup))]
public partial struct ColissionSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Asteroid>();
        state.RequireForUpdate<Bullet>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();
        var deltaTime = SystemAPI.Time.DeltaTime;
        Player player = SystemAPI.GetSingleton<Player>();
        
        foreach (var (asteroidTransform, asteroidEntity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Asteroid>()
                     .WithEntityAccess())
        {
            foreach (var (bulletTransform, bulletEntity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Bullet>()
                         .WithEntityAccess())
            {
                if (CheckCollision(asteroidTransform.ValueRO.Position, bulletTransform.ValueRO.Position))
                {
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(asteroidEntity);
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(bulletEntity);
                }
            }
        }
    }
    
    [BurstCompile]
    private bool CheckCollision(float3 position1, float3 position2)
    {
        return position1.x > position2.x - 0.5f &&
               position1.x < position2.x + 0.5f &&
               position1.y > position2.y - 0.5f &&
               position1.y < position2.y + 0.5f;
    }
}