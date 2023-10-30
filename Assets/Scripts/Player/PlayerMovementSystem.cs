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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        RefRW<LocalTransform> playerTransform = SystemAPI.GetComponentRW<LocalTransform>(player.Entity);
        
        if (horizontal != 0)
            RotatePlayer(playerTransform, config, deltaTime, horizontal);
        
        if (vertical != 0)
            MovePlayer(playerTransform, config, deltaTime, vertical);
        
        float screenWidth = 9.1f;
        float screenHeight = 5.25f;
        
        if (playerTransform.ValueRO.Position.y < -screenHeight)
            playerTransform.ValueRW.Position = new float3(playerTransform.ValueRO.Position.x, screenHeight, 0);
        
        else if (playerTransform.ValueRO.Position.y > screenHeight)
            playerTransform.ValueRW.Position = new float3(playerTransform.ValueRO.Position.x, -screenHeight, 0);
        
        else if (playerTransform.ValueRO.Position.x < -screenWidth)
            playerTransform.ValueRW.Position = new float3(screenWidth, playerTransform.ValueRO.Position.y, 0);
        
        else if (playerTransform.ValueRO.Position.x > screenWidth)
            playerTransform.ValueRW.Position = new float3(-screenWidth, playerTransform.ValueRO.Position.y, 0);
    }

    private static void MovePlayer(RefRW<LocalTransform> playerTransform, Config config, float deltaTime, float vertical)
    {
        float3 direction = math.mul(playerTransform.ValueRO.Rotation, new float3(0, 1, 0));
        playerTransform.ValueRW.Position += direction * config.PlayerSpeed * deltaTime * vertical;
    }

    private static void RotatePlayer(RefRW<LocalTransform> playerTransform, Config config, float deltaTime, float horizontal)
    {
        quaternion currentRotation = playerTransform.ValueRO.Rotation;
        float rotation = config.PlayerRotationSpeed * deltaTime * -horizontal;

        playerTransform.ValueRW.Rotation = math.mul(currentRotation, quaternion.RotateZ(rotation));
    }
}
