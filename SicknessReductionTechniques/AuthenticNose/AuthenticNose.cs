using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AuthenticNose : MonoBehaviour
{
    public GameObject leftEyeNoseModel;
    public GameObject rightEyeNoseModel;

    GameObject leftCameraObject;
    GameObject rightCameraObject;
   
    
    [Range(-2f,2f)]
    public float spacing = 0;


    [Range(0,1)]
    public float yPosition = .5f;
    [Range(-1,1)]
    public float zPosition = 0f;

    [Range(0f,1f)]
    public float noseWidth = 1;

    [Range(0f, 1f)]
    public float noseFlatness = 1;
    public Color noseColor;

    
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(leftEyeNoseModel.activeSelf && rightEyeNoseModel.activeSelf){

            
            leftEyeNoseModel.transform.localPosition = new Vector3(spacing,
                                                            leftEyeNoseModel.transform.localPosition.y,
                                                            leftEyeNoseModel.transform.localPosition.z);
            rightEyeNoseModel.transform.localPosition = new Vector3(-spacing,
                                                            leftEyeNoseModel.transform.localPosition.y,
                                                            leftEyeNoseModel.transform.localPosition.z);


            float zPos = Mathf.Lerp(0.4f, 0.8f, zPosition);
            float yPos = Mathf.Lerp(-0.5f, 0.5f, yPosition);
            float xScale = Mathf.Lerp(0.05f,.15f, noseWidth);
            float yScale = Mathf.Lerp(0.05f, .25f, noseFlatness);
            float zScale = Mathf.Lerp(.03f, .15f, .5f);
            
            leftEyeNoseModel.transform.localScale = new Vector3(xScale,yScale,zScale);
            
            leftEyeNoseModel.transform.localPosition = new Vector3(leftEyeNoseModel.transform.localPosition.x, 
                                                            yPos, 
                                                            zPos);

            leftEyeNoseModel.gameObject.GetComponent<Renderer>().sharedMaterial.color = noseColor;


            rightEyeNoseModel.transform.localScale = new Vector3(xScale,yScale,zScale);
            
            rightEyeNoseModel.transform.localPosition = new Vector3(rightEyeNoseModel.transform.localPosition.x, 
                                                            yPos, 
                                                            zPos);
                                                            
            rightEyeNoseModel.gameObject.GetComponent<Renderer>().sharedMaterial.color = noseColor;
        }                                             
    }
}
