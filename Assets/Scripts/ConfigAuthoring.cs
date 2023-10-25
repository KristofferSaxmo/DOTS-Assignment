using Unity.Entities;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    [Header("Player")]
    public GameObject PlayerPrefab;
    public float PlayerSpeed;
    public float PlayerFireRate;
    public float PlayerBulletSpeed;
    
    [Header("Asteroid")]
    public GameObject AsteroidPrefab;
    public float AsteroidSpeed;
    public float AsteroidSpawnRate;
    public int AsteroidSpawnAmount;

    private class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new Config
            {
                PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
                PlayerSpeed = authoring.PlayerSpeed,
                PlayerFireRate = authoring.PlayerFireRate,
                PlayerBulletSpeed = authoring.PlayerBulletSpeed,
                AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
                AsteroidSpeed = authoring.AsteroidSpeed,
                AsteroidSpawnRate = authoring.AsteroidSpawnRate,
                AsteroidSpawnAmount = authoring.AsteroidSpawnAmount
            });
        }
    }
}

public struct Config : IComponentData
{
    public Entity PlayerPrefab;
    public float PlayerSpeed;
    public float PlayerFireRate;
    public float PlayerBulletSpeed;
    public Entity AsteroidPrefab;
    public float AsteroidSpeed;
    public float AsteroidSpawnRate;
    public int AsteroidSpawnAmount;
}
