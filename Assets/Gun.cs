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
    public float numCharges = 0;
    public float maxCharges = 7;

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
        if (numCharges < maxCharges)
        {
            numCharges += 1;
        }
    }

    private void ShootBullet(float angle)
    {
        Quaternion spreadRotation = Quaternion.Euler(0, angle, 0);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * spreadRotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
    }

    public void ShootGun(InputAction.CallbackContext context)
    {
        if (canShoot && context.performed)
        {
            //write a function that determines the number of bullets to shoot (based on numCharges)
            //if (numCharges % 2 == 0) //even spread
            //{

            //}
            //else
            //{

            //}

            ShootBullet(0);

            isGunCharged = false;

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
