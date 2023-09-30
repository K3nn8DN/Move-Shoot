using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     [SerializeField] private float health;
     [SerializeField] private float maxHealth;
     [SerializeField] private float color;
     

    private void Start(){
        color = .9f;
        maxHealth = 20;
        health = maxHealth;
        
    }

    private void Update()
    {

        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.white,Color.red,color);

        if(color >= health/maxHealth){
            color -= Time.deltaTime / 2;
        }
        
    }

    
}
