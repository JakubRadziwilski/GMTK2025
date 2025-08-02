using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]Transform bulletSpawnPoint;
    [SerializeField]GameObject bulletPrefab;
    public float BulletSpeed = 10f;

    public float ShootingSpeed = 0.25f;

    [SerializeField]float cooldownEndTimeInSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Find("BulletSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed) Attack();
    }

    public void Attack()
    {
        Shoot();
        //if not on cooldown perform shoot() and add shootingspeed to cooldown
        if (cooldownEndTimeInSeconds < Time.time)
        {
            Shoot();
            AddCoolDownInSeconds(ShootingSpeed);
        }
    }

     public void AddCoolDownInSeconds(float addedTime)
    {
        cooldownEndTimeInSeconds = Time.time + addedTime;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.linearVelocity = transform.right * BulletSpeed;
    }
}