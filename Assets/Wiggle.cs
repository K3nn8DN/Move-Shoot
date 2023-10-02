using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    public float xRot = 0f;
    public float yRot = 0f;
    public float zRot = 0f;

    public float mult = 3f;

    private void FixedUpdate()
    {
        float cos = Mathf.Cos(Time.time * Mathf.PI * mult);
        transform.Rotate(xRot * Time.fixedDeltaTime * cos, yRot * Time.fixedDeltaTime * cos, zRot * Time.fixedDeltaTime * cos);
    }
}
