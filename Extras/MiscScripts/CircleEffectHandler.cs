using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CircleEffectHandler : MonoBehaviour
{

    public Material circleEffectMaterial; //create material from shader and attatch here
    public float speedThreshold;
    
    GameObject cameraObject;
    float lagTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        angularVelocity = new Vector3(0,0,0);
        StartCoroutine(TrackAngularVelocity());
        cameraObject = GetComponent<Camera>().gameObject;
        circleEffectMaterial.SetFloat("_blackRatio", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CircleFadeRoutine(){
        yield return null;
        float t = 0;
        //fade in
        while(t < lagTime){
            
            t+=Time.deltaTime;
            circleEffectMaterial.SetFloat("_blackRatio", 1f - t/lagTime);
            yield return null;
        }
        circleEffectMaterial.SetFloat("_blackRatio", 0);

        while(angularVelocity.magnitude > speedThreshold){
            circleEffectMaterial.SetFloat("_blackRatio", 0);
            yield return null;
        }

        t = 0;
        //fade in
        while(t < lagTime){
            t+=Time.deltaTime;
            circleEffectMaterial.SetFloat("_blackRatio", t/lagTime);
            yield return null;
        }
        circleEffectMaterial.SetFloat("_blackRatio",1);
        lagging = false;
    }

    Vector3 angularVelocity;
    bool lagging = false;
    IEnumerator TrackAngularVelocity(){
        Vector3 lastRotation = new Vector3(0,0,0);
        while(true){
            yield return null;
            Vector3 delta = cameraObject.transform.localEulerAngles - lastRotation;
            lastRotation = cameraObject.transform.localEulerAngles;
            angularVelocity = delta / Time.deltaTime;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst){
        

        if(angularVelocity.magnitude > speedThreshold && !lagging){
            lagging = true;
            StartCoroutine(CircleFadeRoutine());
        }




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
