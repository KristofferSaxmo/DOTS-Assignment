using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Config>();
        state.RequireForUpdate<Player>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Config config = SystemAPI.GetSingleton<Config>();
        Player player = SystemAPI.GetSingleton<Player>();
        var deltaTime = SystemAPI.Time.DeltaTime;
        
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var input = new float3(horizontal, vertical, 0) * deltaTime * config.PlayerSpeed;

        if (input.Equals(float3.zero)) return;
        
        RefRW<LocalTransform> playerTransform = SystemAPI.GetComponentRW<LocalTransform>(player.Entity);
        playerTransform.ValueRW.Position += input;
    }
}
