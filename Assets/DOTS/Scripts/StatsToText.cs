using Unity.Entities;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class StatsToText : MonoBehaviour
{
    private EntityQuery _entityQuery;
    private TextMeshProUGUI _text;
    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<Asteroid>());
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        int entityCount = _entityQuery.CalculateEntityCount();
        float fps = 1.0f / Time.deltaTime;
        _text.SetText("DOTS" + "\nFPS: " + math.round(fps) + "\nAsteroids: " + entityCount);
    }
}
