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

            AddComponent<Asteroid>(entity);
        }
    }
}

public struct Asteroid : IComponentData
{
    
}