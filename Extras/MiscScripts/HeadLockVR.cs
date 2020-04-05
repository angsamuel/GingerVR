using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLockVR : MonoBehaviour
{
    public string lockAxis = "Jump";
    bool cameraLocked = false;
    Quaternion lockedRotation;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraAngles = Camera.main.transform.localEulerAngles;
        Vector3 parentAngles = new Vector3(cameraAngles.x-90, cameraAngles.y-90, cameraAngles.z-90);

        parent.transform.eulerAngles = parentAngles;
        //target.transform.rotation = Camera.main.transform.rotation * -1;
        //target.transform.Rotate(-cameraRotation.x, -cameraRotation.y, -cameraRotation.z);
        //Debug.Log(Camera.main.transform.localEulerAngles);
        //Camera.main.transform.LookAt(target.transform);
        //OVRManager.display.RecenterPose(); 
        //Camera.main.transform.rotation = lockedRotation;
        if(Input.GetAxisRaw(lockAxis) != 0){
            Debug.Log("Locked Camera");
            if(!cameraLocked){
                cameraLocked = true;
                lockedRotation = Quaternion.identity;
            }
            Camera.main.transform.rotation = lockedRotation;
        }else if(Input.GetAxisRaw(lockAxis) == 0){
            cameraLocked = false;
        }
    }
}
