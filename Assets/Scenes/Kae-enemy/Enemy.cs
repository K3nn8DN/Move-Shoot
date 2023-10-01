using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float color;
    //[SerializeField] private float gunDamage;
    private Renderer gameObject1;
    private bool isDead;

    private float gunDamage = Gun.damage;
    
     
     

    private void Start(){
        gameObject1 = gameObject.GetComponent<Renderer>();
        color = .99f;
        maxHealth = 20;
        health = maxHealth;
        isDead = false;


    }

    private void Update()
    {

        //sets and changes enemy color based on health
        if (isDead == false)
        {
            gameObject1.material.color = Color.Lerp(Color.white, Color.red, color);
        }

        if (color >= health/maxHealth){
            color -= Time.deltaTime / 2;
        }
        
    }

    


      void OnTriggerEnter(Collider other)
    {
        if (isDead == false)
        {
            if (other.CompareTag("Bullet"))
            {
                
                health -= gunDamage;
        

                if (health <= 0)
                {
                    //insert add point here
                    Destroy(gameObject1);
                    gameObject.GetComponent<ParticleSystem>().Play();
                    Destroy(gameObject, 1);
                    isDead = true;

                }
                
                Destroy(other.gameObject);
            }
            

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (isDead == false)
        {
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject, 1);

            }


        }


    }




}
