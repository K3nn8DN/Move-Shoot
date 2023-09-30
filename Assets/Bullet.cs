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
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
