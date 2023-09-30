using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTOplayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    [Tooltip("movespeed in units per second")]
    [Range(0f, 20)]
    private float moveSpeed = 5;

    private Transform targetWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        targetWaypoint = player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
    }
}
