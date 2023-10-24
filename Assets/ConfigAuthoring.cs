using Unity.Entities;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour
{
    [Header("Asteroid")]
    public GameObject AsteroidPrefab;
    public float AsteroidSpeed;
    public float AsteroidSpawnRate;

    private class Baker : Baker<ConfigAuthoring>
    {
        public override void Bake(ConfigAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new Config
            {
                AsteroidPrefab = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic),
                AsteroidSpeed = authoring.AsteroidSpeed,
                AsteroidSpawnRate = authoring.AsteroidSpawnRate,
            });
        }
    }
}

public struct Config : IComponentData
{
    public Entity AsteroidPrefab;
    public float AsteroidSpeed;
    public float AsteroidSpawnRate;
}
