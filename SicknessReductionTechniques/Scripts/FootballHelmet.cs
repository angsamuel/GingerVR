using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FootballHelmet : MonoBehaviour
{
    GameObject helmetModel;
    GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        helmetModel = transform.GetChild(0).gameObject;
        cameraObject = GameObject.Find("CenterEyeAnchor");

        //if we found the main camera, set ourselves up as a child
        if(cameraObject != null){
            transform.parent = cameraObject.transform;
            transform.localPosition = new Vector3(0,0,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
