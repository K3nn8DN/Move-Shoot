using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private bool canShoot = true;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public static float damage = 1;
    public static bool isGunCharged = false;
    public static float numCharges;

    private AudioSource audioSource;
    public float minPitch = 0.75f;
    public float maxPitch = 1.25f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChargeGun()
    {
        isGunCharged = true;
    }

    public void ShootGun(InputAction.CallbackContext context)
    {
        if (canShoot && context.performed)
        { 
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
            canShoot = false;
            nextFireTime = Time.time + fireRate;
        }
    }


    void Update()
    {
        if (audioSource.isPlaying)
        {
            float randomPitch = Random.Range(minPitch, maxPitch);

            audioSource.pitch = randomPitch;
        }

        if (!canShoot && Time.time >= nextFireTime)
        {
            canShoot = true;
        }
    }
}
