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
        state.RequireForUpdate<Player>();
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
                }
            }
        }
    }
    
    [BurstCompile]
    private bool CheckCollision(float3 position1, float3 position2)
    {
        float precision = 0.2f;
        return position1.x > position2.x - precision &&
               position1.x < position2.x + precision &&
               position1.y > position2.y - precision &&
               position1.y < position2.y + precision;
    }
}