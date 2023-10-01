using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    private Transform m_perspective;
    private RaycastHit m_data;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float raycastDistance = 50f;
    [SerializeField] private float lerpRate = 0.1f;
    [SerializeField] private UnityEvent<int> OnGunChargeModified;
    [SerializeField] private ParticleSystem smokeSystem;

    [SerializeField] private float chargeTime;
    private float m_nextChargeTime = 0f;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    private bool canShoot = true;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public static float damage = 1;
    public static bool isGunCharged = false;

    public int NumCharges
    {
        get { return m_numCharges; }

        set { 
            if (m_numCharges != value)
            {
                m_numCharges = value;
                OnGunChargeModified.Invoke(value);
            } 
        }
    }
    private int m_numCharges;
    public int maxCharges = 7;

    private AudioSource audioSource;
    public float minPitch = 0.75f;
    public float maxPitch = 1.25f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_perspective = Camera.main.transform;
    }

    public void ChargeGun()
    {
        // just so we can't stack charges super fast
        if (Time.time >= m_nextChargeTime && NumCharges < maxCharges)
        {
            isGunCharged = true;
            NumCharges += 1;

            m_nextChargeTime = Time.time + chargeTime;
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
            ShootBullet(0);

            if (isGunCharged)
            {
                isGunCharged = false;

                for (int i = 1; i < NumCharges + 1; i+=1)
                {
                    ShootBullet(10f * i * -1);
                    ShootBullet(10f * i);
                }

                NumCharges = 0;

                smokeSystem.Play();
            }

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

        // orient gun to face center of screen point
        Vector3 dest =
            Physics.Raycast(m_perspective.position, m_perspective.forward, out m_data, raycastDistance, mask) 
            && m_data.distance > 1f ?
            (m_data.point - transform.position).normalized : m_perspective.forward;

        transform.forward = Vector3.Lerp(transform.forward, dest, lerpRate);
    }
}
