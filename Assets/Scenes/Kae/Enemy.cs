using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     [SerializeField] private float health;
    public float c11;
    public float c12;
    public float c13;
    public Color c1 = new Color(0, 0, 0);

    

    private void Start(){
        



    }

    private void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = c1;
    }
}
