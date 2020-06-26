using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class DynamicFOV : MonoBehaviour
{
    //VignetteModel.Settings vignetteSettings;
    //public PostProcessingProfile profile;
    public GameObject player;
    public Material circleEffectMaterial; //create material from shader and attatch here

    public float decaySpeed = 20f; 

    public float minViewValue = .5f;
    public float maxViewValue = 1.5f;
    

    Vector3 lastPosition;
    Vector3 lastOrientation;
    
    Rigidbody rigidbody;
    bool usingRigidbody;

    public float decayRate = -.001f;
    public float angluarSpeedModifier = .1f;
    public float translationSpeedModifier = .1f;
    public float testRad = .5f;
    

    Vector3 angularVelocity;
    Vector3 translationalVelocity;


    // Start is called before the first frame update
    //smallest is 60
    float checkTime = .1f;
    IEnumerator TrackVelocities(){
        Vector3 lastRotation = new Vector3(0,0,0);
        Vector3 lastPosition = transform.position;
        while(true){
            
            Vector3 rotationDelta = transform.localEulerAngles - lastRotation;
            lastRotation = transform.localEulerAngles;
            

            Vector3 translationDelta = transform.position - lastPosition;
            lastPosition = transform.position;

            

            angularVelocity = rotationDelta / checkTime;
            translationalVelocity = translationDelta / checkTime;

            yield return new WaitForSeconds(checkTime);
        }
    }
    

    void Start()
    {

        if(player == null){
            player = this.gameObject;
        }
        if(circleEffectMaterial == null){
            circleEffectMaterial = Resources.Load("GingerVR-master/SicknessReductionTechniques/Materials/CircleEffectMat") as Material;
        }

        lastPosition = player.transform.position;
        lastOrientation = player.transform.eulerAngles;

        rigidbody = player.GetComponent<Rigidbody>();
        if(rigidbody == null){
            usingRigidbody = false;
            StartCoroutine(TrackVelocities());
        }else{
            usingRigidbody = true;
        }
        //VignetteModel.Settings vSettings = profile.vignette.settings;
        //vSettings.intensity = 0;
    }



    
    float speed = 0;
    float angularSpeed = 0;
    public float viewRadius = 0f; //0 to .5



    void Update()
    {
        float translationalSpeed;
        float rotationalSpeed;


        translationalSpeed = translationalVelocity.magnitude;
        rotationalSpeed = angularVelocity.magnitude;
     

        float cRate = ((translationalSpeed - decaySpeed) * translationSpeedModifier) + (rotationalSpeed*angluarSpeedModifier);


        
        if(translationalSpeed <= decaySpeed){
            viewRadius -= decayRate;
            
           
        }else{
            viewRadius -= cRate;
            
        }
        if(viewRadius > maxViewValue){
                viewRadius = maxViewValue;
                
            }
        if(viewRadius < minViewValue){
                viewRadius = minViewValue;
        }

        circleEffectMaterial.SetFloat("_viewRadius", viewRadius);

        
        lastPosition = player.transform.position;
        

    }

    void OnRenderImage(RenderTexture src, RenderTexture dst){
    

        RenderTexture renderTexture = RenderTexture.GetTemporary(src.width, src.height);

        Graphics.Blit(src, renderTexture); //copies source texture to destination texture

        //apply the render texture as many iterations as specified

        RenderTexture tempTexture = RenderTexture.GetTemporary(src.width, src.height); //creates a quick temporary texture for calculations
        Graphics.Blit(renderTexture, tempTexture, circleEffectMaterial);
        RenderTexture.ReleaseTemporary(renderTexture); //releases the temporary texture we got from GetTemporary 
        renderTexture = tempTexture;

        Graphics.Blit(renderTexture, dst);
        RenderTexture.ReleaseTemporary(renderTexture);
    }
}
