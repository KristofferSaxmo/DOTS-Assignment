using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;



[UpdateAfter(typeof(TransformSystemGroup))]
public partial struct CollisionSystem : ISystem
{
    private const float Precision = 0.4f;
    const float PrecisionSquared = Precision * Precision;
    
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
        foreach (var (bulletTransform, bulletEntity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Bullet>().WithEntityAccess())
        {
            foreach (var (asteroidTransform, asteroidEntity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Asteroid>().WithEntityAccess())
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
        float3 delta = position1 - position2;
        float distanceSquared = math.dot(delta, delta);
        
        return distanceSquared < PrecisionSquared;
    }
}