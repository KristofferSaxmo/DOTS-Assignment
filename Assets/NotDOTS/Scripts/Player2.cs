using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private const float ScreenWidth = 9.1f;
    private const float ScreenHeight = 5.25f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float fireRate;
    private float _fireTimer;

    void Update()
    {
        _fireTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && _fireTimer > fireRate)
        {
            FireBullet();
            _fireTimer = 0;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (horizontal != 0)
            RotatePlayer(horizontal);
        
        if (vertical != 0)
            MovePlayer(vertical);
        
        var position = transform.position;
        
        if (position.y < -ScreenHeight)
            position = new Vector3(position.x, ScreenHeight, 0);
        
        else if (position.y > ScreenHeight)
            position = new Vector3(position.x, -ScreenHeight, 0);
        
        else if (position.x < -ScreenWidth)
            position = new Vector3(ScreenWidth, position.y, 0);
        
        else if (position.x > ScreenWidth)
            position = new Vector3(-ScreenWidth, position.y, 0);
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up * 0.5f, Quaternion.identity);
        bullet.GetComponent<Bullet2>().Direction = transform.up;
    }

    private void MovePlayer(float vertical)
    {
        transform.position += transform.up * (vertical * movementSpeed * Time.deltaTime);
    }

    private void RotatePlayer(float horizontal)
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime * -horizontal));
    }
}
