using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    bool canUp, canDown, canLeft, canRight = false;
    Rigidbody rb;
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Move(float x, float z){
        rb.velocity = new Vector3(x*speed, x*speed/2, z*speed);
    }

    // Update is called once per frame
    void Update()
    {
       Move(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }
}
