using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    private Rigidbody rb;
    private int maxRicochets = 3;
    private int currentRicochets = 0;
    private Vector3 lastVelocity;

    public AudioSource ricochetAudio;
    //public AudioSource bulletImpactAudio;
    public float minPitch = 0.5f;
    public float maxPitch = 1.75f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void LateUpdate()
    {
        lastVelocity = rb.velocity;

    }

    void OnCollisionEnter(Collision collision)
    {

        if (currentRicochets < maxRicochets)
        {
            Vector3 reflectionDirection = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);

            rb.velocity = reflectionDirection;

            currentRicochets++;

            float randomPitch = Random.Range(minPitch, maxPitch);
            ricochetAudio.pitch = randomPitch;
            if (ricochetAudio != null && ricochetAudio.clip != null)
            {
                ricochetAudio.Play();
            }

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(collision.gameObject);
            //Destroy(gameObject);

            //float randomPitch = Random.Range(minPitch+0.25f, maxPitch-0.5f);
            //bulletImpactAudio.pitch = randomPitch;
            //if (bulletImpactAudio != null && bulletImpactAudio.clip != null)
            //{
            //    bulletImpactAudio.Play();
            //}

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
