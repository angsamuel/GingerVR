using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFOVFilter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject filter;

        //VignetteModel.Settings vignetteSettings;
    //public PostProcessingProfile profile;

    public float decaySpeed = 20f; 

    public float minFilterScale = .5f;
    public float maxFilterScale = 1.5f;
    

    Vector3 lastPosition;
    Vector3 lastOrientation;
    
    Rigidbody rigidbody;
    bool usingRigidbody;

    public float decayRate = -.001f;
    public float angluarSpeedModifier = .1f;
    public float translationSpeedModifier = .1f;
    

    Vector3 angularVelocity;
    Vector3 translationalVelocity;

    
    float speed = 0;
    float angularSpeed = 0;
    float filterScale = 0f;


    float checkTime = .1f;
    IEnumerator TrackVelocities(){
        Vector3 lastRotation = new Vector3(0,0,0);
        Vector3 lastPosition = transform.position;
        while(true){
            Vector3 rotationDelta = transform.eulerAngles - lastRotation;
            lastRotation = transform.eulerAngles;

            Vector3 translationDelta = transform.position - lastPosition;
            lastPosition = transform.position;

            angularVelocity = rotationDelta / checkTime;
            translationalVelocity = translationDelta / checkTime;

            yield return new WaitForSeconds(checkTime);
        }
    }
    

    void Start()
    {

        lastPosition = gameObject.transform.position;
        lastOrientation = gameObject.transform.eulerAngles;

        rigidbody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(TrackVelocities());
        //VignetteModel.Settings vSettings = profile.vignette.settings;
        //vSettings.intensity = 0;
    }



    

    void Update()
    {
        float translationalSpeed;
        float rotationalSpeed;
        translationalSpeed = translationalVelocity.magnitude;
        rotationalSpeed = angularVelocity.magnitude;
        float cRate = ((translationalSpeed - decaySpeed) * translationSpeedModifier) + (rotationalSpeed*angluarSpeedModifier);


        
        if(translationalSpeed <= decaySpeed){
            filterScale -= decayRate;
            Debug.Log("DECAYING");
        }else{
            filterScale -= Mathf.Abs(cRate);
        }
        if(filterScale > maxFilterScale){
            filterScale = maxFilterScale;  
        }
        if(filterScale < minFilterScale){
            filterScale = minFilterScale;
        }


        //update scale of filter
        filter.transform.localScale = new Vector3(filterScale,filterScale,1);

        
        lastPosition = gameObject.transform.position;
        

    }

    
    
}
