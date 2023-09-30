using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private bool canShoot = true;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public float damage;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            canShoot = false;
            nextFireTime = Time.time + fireRate;
        }

        if (!canShoot && Time.time >= nextFireTime)
        {
            canShoot = true;
        }
    }
}
