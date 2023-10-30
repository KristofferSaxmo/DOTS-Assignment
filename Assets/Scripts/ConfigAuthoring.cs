using Unity.Entities;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    [Header("Player")]
    public GameObject PlayerPrefab;
    public float PlayerRotationSpeed;
    public float PlayerSpeed;
    public float PlayerFireRate;
    
    [Header("Asteroid")]
    public GameObject AsteroidPrefab;
    public float AsteroidSpeed;
    public float AsteroidSpawnRate;
    public int AsteroidSpawnAmount;
    
    [Header("Bullet")]
    public GameObject BulletPrefab;
    public float BulletSpeed;

    private class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new Config
            {
                PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
                PlayerRotationSpeed = authoring.PlayerRotationSpeed,
                PlayerSpeed = authoring.PlayerSpeed,
                PlayerFireRate = authoring.PlayerFireRate,
                BulletSpeed = authoring.BulletSpeed,
                AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
                AsteroidSpeed = authoring.AsteroidSpeed,
                AsteroidSpawnRate = authoring.AsteroidSpawnRate,
                AsteroidSpawnAmount = authoring.AsteroidSpawnAmount,
                BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public struct Config : IComponentData
{
    public Entity PlayerPrefab;
    public float PlayerRotationSpeed;
    public float PlayerSpeed;
    public float PlayerFireRate;
    public Entity AsteroidPrefab;
    public float AsteroidSpeed;
    public float AsteroidSpawnRate;
    public int AsteroidSpawnAmount;
    public Entity BulletPrefab;
    public float BulletSpeed;
}
