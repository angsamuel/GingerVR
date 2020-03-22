using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//double nose -.19, -.16, .45
//scale .1

//single nose z: .4 - .8
//single nose 
[ExecuteInEditMode]
public class SingleNose : MonoBehaviour
{
    GameObject cameraObject;
    
    [Range(0,1)]
    public float yPosition = .5f;
    [Range(0,1)]
    public float zPosition = .5f;

    [Range(0f,1f)]
    public float noseWidth = 1;

    [Range(0f, 1f)]
    public float noseFlatness = 1;

    public Color noseColor;


    void Awake()
    {
        //if we found the main camera, set ourselves up as a child
        cameraObject = transform.parent.gameObject;
        if(cameraObject != null){
            transform.parent = cameraObject.transform;
            transform.localPosition = new Vector3(0,0,0);
        }
    }

    // Update is called once per frame
    
    void Update()
    {
        float zPos = Mathf.Lerp(0.4f, 0.8f, zPosition);
        float yPos = Mathf.Lerp(-0.5f, 0.5f, yPosition);
        float xScale = Mathf.Lerp(0.05f,.15f, noseWidth);
        float yScale = Mathf.Lerp(0.05f, .25f, 1 - noseFlatness);
        float zScale = Mathf.Lerp(.03f, .15f, .5f);

        transform.localScale = new Vector3(xScale,yScale,zScale);
        
        transform.localPosition = new Vector3(transform.localPosition.x, 
                                                        yPos, 
                                                        zPos);
        
        GetComponent<Renderer>().sharedMaterial.color = noseColor;
        }
    }
