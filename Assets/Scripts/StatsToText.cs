using Unity.Entities;
using Unity.Mathematics;
using TMPro;
using UnityEngine;

public class StatsToText : MonoBehaviour
{
    public EntityQuery entityQuery;
    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<Asteroid>());
    }
    private void Update()
    {
        int entityCount = entityQuery.CalculateEntityCount();
        float fps = 1.0f / Time.deltaTime;
        GetComponent<TextMeshProUGUI>().SetText("FPS: " + fps + "\nAsteroids: " + entityCount);
    }
}
