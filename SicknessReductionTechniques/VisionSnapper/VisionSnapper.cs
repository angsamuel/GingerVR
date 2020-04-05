using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionSnapper : MonoBehaviour
{
    public Material visionSnapperMaterial;

    public GameObject yRotator;
    public GameObject cameraObject;

    public float transitionTime = 1f;
    public float fadeTime = .1f; 
    public float snappingAngle = 22.5f;
    public float speedThreshold = 50f;
    

    Vector3 savedRotation;
    Vector3 angularVelocity;


    bool inSnapRoutine = false;
    public bool yLocked = false;
    bool needToSaveRotation = true;

    // Start is called before the first frame update
    void Start()
    {
        if(cameraObject == null){
            cameraObject = Camera.main.gameObject;
        }
        if(visionSnapperMaterial == null){
            visionSnapperMaterial = Resources.Load("GingerVR-master/SicknessReductionTechniques/VisionSnapper/VisionSnapperMat") as Material;
        }
        
        yRotator.transform.position = cameraObject.transform.position;
        yRotator.transform.parent = cameraObject.transform.parent;
        cameraObject.transform.parent = yRotator.transform;

        savedRotation = cameraObject.transform.eulerAngles;

        StartCoroutine(DelayActivity());
        StartCoroutine(TrackAngularVelocity());
    }
    //if speed above threshold
    //begin fade out
    //lock cameraObject for however long

    IEnumerator DelayActivity(){
        inSnapRoutine = true;
        yield return new WaitForSeconds(1f);
        inSnapRoutine = false;
    }
    
    IEnumerator SnapRoutine(){
        yield return null;
        float direction;
        if(angularVelocity.y == 0){
            direction = 0;
        }else{
            direction = Mathf.Abs(angularVelocity.y) / angularVelocity.y;
        }
        int snaps = 0;
        
        float fadeOutProgress = 0f;
        //fade out image;
        while(fadeOutProgress < fadeTime){
            fadeOutProgress += Time.deltaTime;
            
            //fadeImage.color = new Color(0,0,0,fadeOutProgress/fadeTime);

            visionSnapperMaterial.SetFloat("_darkness", fadeOutProgress/fadeTime);
            yield return null;
        }
        visionSnapperMaterial.SetFloat("_darkness",1);
        yLocked = true;
        float waitProgress = 0f;
        //yLocked
        
        while(waitProgress < transitionTime){
            waitProgress += Time.deltaTime;
            

            if(waitProgress >= transitionTime && Mathf.Abs(angularVelocity.y) >= speedThreshold){
                waitProgress = 0f;
                snaps += 1;
            }
            yield return null;
        }
        //unyLocked
        yLocked = false;
        //rotate Y to match 22.5
        
        yRotator.transform.RotateAround(cameraObject.transform.position, new Vector3(0,1,0), snappingAngle*direction);
        //cameraObject.transform.Rotate(0,snappingAngle*direction,0);
        float fadeInProgress = 0f;
        while(fadeInProgress < fadeTime){
            fadeInProgress += Time.deltaTime;
            visionSnapperMaterial.SetFloat("_darkness",1 -  (fadeOutProgress/fadeTime));
            yield return null;
        }
        visionSnapperMaterial.SetFloat("_darkness",0);

        
        
        inSnapRoutine = false;

    }

    // Update is called once per frame

    float checkTime = 0.1f;
    IEnumerator TrackAngularVelocity(){
        yield return null;
        Vector3 lastRotation = new Vector3(0,0,0);
        while(true){
            yield return new WaitForSeconds(checkTime);
            Vector3 delta = cameraObject.transform.localEulerAngles - lastRotation;
            lastRotation = cameraObject.transform.localEulerAngles;
            angularVelocity = delta / checkTime;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst){
        RenderTexture renderTexture = RenderTexture.GetTemporary(src.width, src.height);

        Graphics.Blit(src, renderTexture); //copies source texture to destination texture

        RenderTexture tempTexture = RenderTexture.GetTemporary(src.width, src.height); //creates a quick temporary texture for calculations
        Graphics.Blit(renderTexture, tempTexture, visionSnapperMaterial);
        RenderTexture.ReleaseTemporary(renderTexture); //releases the temporary texture we got from GetTemporary 
        renderTexture = tempTexture;

        Graphics.Blit(renderTexture, dst);
        RenderTexture.ReleaseTemporary(renderTexture);
    }


    void Update()
    {
        //angularVelocity = (cameraObject.transform.eulerAngles - savedRotation) / Time.deltaTime;
       //
       
       if(Mathf.Abs(angularVelocity.y) >= speedThreshold && !inSnapRoutine){
           inSnapRoutine = true;
           savedRotation = cameraObject.transform.eulerAngles;
           StartCoroutine(SnapRoutine());
       } 
       //keep camera locked
       if(yLocked){

            //rotate around the inverse of current rotation
            yRotator.transform.RotateAround(cameraObject.transform.position, new Vector3(0,1,0),-cameraObject.transform.eulerAngles.y);
            
            //rotate around by savedRotation
            yRotator.transform.RotateAround(cameraObject.transform.position, new Vector3(0,1,0),savedRotation.y);
          
       }else{
           needToSaveRotation = true;
       }


    }

    void RotateCamera(float degrees){

    }


}
