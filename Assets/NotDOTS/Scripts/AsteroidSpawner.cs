using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float spawnRate;
    [SerializeField] private int spawnAmount;
    
    private const float ScreenHeight = 5.20f;
    private float _spawnCooldown;
    private bool _spawnTop;
    private bool _spawnLeft;
    private int _asteroidCount;
    private int _wave;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If asteroid in scene, return
        if (GameObject.FindWithTag("Asteroid") != null) return;
        
        _spawnCooldown += Time.deltaTime;

        if (_spawnCooldown < spawnRate) return;
        
        _wave++;
        
        // wave = n
        // config.AsteroidSpawnAmount = x
        // (x * n) + n^2
        
        var amount = (spawnAmount * _wave + _wave*_wave);
        
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject asteroid = Instantiate(asteroidPrefab);
            float yPos;
            Vector2 direction;
            
            if (_spawnTop)
            {
                yPos = ScreenHeight;
                direction = new Vector2(Random.Range(-0.9f, 0.9f), Random.Range(-0.1f, -1.0f)).normalized;
            }
            else
            {
                yPos = -ScreenHeight;
                direction = new Vector2(Random.Range(-0.9f, 0.9f), Random.Range(0.1f, 1.0f)).normalized;
            }

            float xPos = Random.Range(-8.0f, 8.0f);
            
            asteroid.transform.position = new Vector3(xPos, yPos, 0);
            asteroid.GetComponent<Asteroid2>().Direction = direction;
            _spawnTop = !_spawnTop;
        }
        
        _spawnCooldown = 0;
    }
}
