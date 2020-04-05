using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        System.Array.Reverse(mesh.triangles);// = mesh.triangles.Reverse();//.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
