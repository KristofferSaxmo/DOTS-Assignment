using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class AsteroidAuthoring : MonoBehaviour
{
    private class Baker : Baker<AsteroidAuthoring>
    {
        public override void Bake(AsteroidAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Asteroid()
            {
                Entity = entity
            });
        }
    }
}

public struct Asteroid : IComponentData
{
    public Entity Entity;
    public float3 Direction;
}