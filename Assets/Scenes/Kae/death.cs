using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    public int cubesPerAxis = 8;
    public float delay = 1f;
    public float force = 300f;
    public float radius = 2f;


    private void Start()
    {
       
    }

    private void Main()
    {
        for(int x=0; x< cubesPerAxis; x++)
        {
            for (int y =0; y< cubesPerAxis; y++)
            {
                for(int z=0 ; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }
    }

    void CreateCube(Vector3 coordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer rd = cube.GetComponent<Renderer>();

        rd.material = GetComponent<Renderer>().material;

       // cube.transform.localScale=transform.localScale /c



    }
}
