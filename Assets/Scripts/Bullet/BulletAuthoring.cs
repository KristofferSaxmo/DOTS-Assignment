using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    private class Baker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<Bullet>(entity);
        }
    }
}

public struct Bullet : IComponentData
{
    
}
