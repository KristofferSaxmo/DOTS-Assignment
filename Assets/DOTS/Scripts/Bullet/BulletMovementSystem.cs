using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct BulletMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Bullet>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();
        var deltaTime = SystemAPI.Time.DeltaTime;
        float screenWidth = 9.1f;
        float screenHeight = 5.25f;
        
        foreach (var (bulletTransform, bullet, bulletEntity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<Bullet>>().WithAll<Bullet>()
                     .WithEntityAccess())
        {
            bulletTransform.ValueRW.Position += bullet.ValueRO.Direction * config.BulletSpeed * deltaTime;
            
            if (bulletTransform.ValueRO.Position.y < -screenHeight ||
                bulletTransform.ValueRO.Position.y > screenHeight ||
                bulletTransform.ValueRO.Position.x < -screenWidth ||
                bulletTransform.ValueRO.Position.x > screenWidth)
            {
                SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(bulletEntity);
            }
        }
    }
}