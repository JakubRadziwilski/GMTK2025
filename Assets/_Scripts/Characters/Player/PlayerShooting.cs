using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]Transform bulletSpawnPoint;
    [SerializeField]GameObject bulletPrefab;
    public float BulletSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Find("BulletSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.linearVelocity = transform.right * BulletSpeed;
    }
}