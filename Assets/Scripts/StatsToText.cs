using Unity.Entities;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class StatsToText : MonoBehaviour
{
    private EntityQuery _entityQuery;
    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<Asteroid>());
    }
    private void Update()
    {
        int entityCount = _entityQuery.CalculateEntityCount();
        float fps = 1.0f / Time.deltaTime;
        GetComponent<TextMeshProUGUI>().SetText("FPS: " + math.round(fps) + "\nAsteroids: " + entityCount);
    }
}
