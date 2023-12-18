using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Asteroid2 : MonoBehaviour
{
    private const float ScreenWidth = 9.1f;
    private const float ScreenHeight = 5.25f;
    private const float Speed = 1.0f;
    
    public Vector2 Direction { private get; set; }
    
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Direction * (Speed * Time.deltaTime));

        var position = transform.position;
        
        if (position.y < -ScreenHeight)
            transform.position = new float3(position.x, ScreenHeight, 0);
            
        else if (position.y > ScreenHeight)
            transform.position = new float3(position.x, -ScreenHeight, 0);
            
        else if (position.x < -ScreenWidth)
            transform.position = new float3(ScreenWidth, position.y, 0);
            
        else if (position.x > ScreenWidth)
            transform.position = new float3(-ScreenWidth, position.y, 0);
    }
}
