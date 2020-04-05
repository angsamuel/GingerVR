using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTester : MonoBehaviour
{
    public GameObject subject;
    public bool locking = false;
    public bool lockX;
    public bool lockY;
    public bool lockZ;
    Vector3 savedRotation;
    bool needToSave = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(locking){
            if(needToSave){
                savedRotation = subject.transform.localEulerAngles;
                needToSave = false;
            }


            Vector3 inverseRot = Quaternion.Inverse(subject.transform.localRotation).eulerAngles;
            
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(0,0,0);

            //transform.rotation = (Quaternion.Inverse(subject.transform.localRotation));

            // transform.RotateAround(subject.transform.position, new Vector3(1,0,0), inverseRot.x);
            transform.RotateAround(subject.transform.position, new Vector3(0,1,0), inverseRot.y);
            //inverseRot = Quaternion.Inverse(subject.transform.localRotation).eulerAngles;
            //transform.RotateAround(subject.transform.position, new Vector3(0,0,1), inverseRot.z);

            //transform.RotateAround(subject.transform.position, new Vector3(1,0,0), savedRotation.x);
            transform.RotateAround(subject.transform.position, new Vector3(0,1,0), savedRotation.y);
            //inverseRot = Quaternion.Inverse(subject.transform.localRotation).eulerAngles;
            //transform.RotateAround(subject.transform.position, new Vector3(0,0,1), savedRotation.z);
        }
        
    }
}
