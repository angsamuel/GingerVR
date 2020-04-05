using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VisionLock : MonoBehaviour
{
    Vector3 offset;
    public GameObject megaParent;
    public string axisToActivate = "Fire1";
    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.gameObject.transform.position;
        transform.eulerAngles =  (Camera.main.gameObject.transform.rotation).eulerAngles;
        if(Input.GetAxisRaw(axisToActivate) != 0){
            megaParent.transform.parent = Camera.main.transform;
        }else{
            megaParent.transform.parent = null;
        }
        
    }
}
