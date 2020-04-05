using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyepatcher : MonoBehaviour
{
    // Start is called before the first frame update
    public bool blindLeftEye;
    public bool blindRightEye;

     GameObject leftEyeBlock;
     GameObject rightEyeBlock;

    public GameObject eyePatchBlock;
    void Start()
    {
        GameObject leftEyeAnchor = GameObject.Find("LeftEyeAnchor");
        GameObject rightEyeAnchor = GameObject.Find("RightEyeAnchor");

        leftEyeBlock = Instantiate(eyePatchBlock,leftEyeAnchor.transform);
        leftEyeBlock.transform.localPosition = new Vector3(0,0,0.5f);
        leftEyeBlock.layer = LayerMask.NameToLayer("LeftEye");


        
        rightEyeBlock = Instantiate(eyePatchBlock,rightEyeAnchor.transform);
        rightEyeBlock.transform.localPosition = new Vector3(0,0,0.5f);
        rightEyeBlock.layer = LayerMask.NameToLayer("RightEye");


        
    }

    // Update is called once per frame
    void Update()
    {
        if(blindLeftEye){
            leftEyeBlock.SetActive(true);
        }else{
            leftEyeBlock.SetActive(false);
        }
        if(blindRightEye){
            rightEyeBlock.SetActive(true);
        }else{
            rightEyeBlock.SetActive(false);
        }
    }
}
