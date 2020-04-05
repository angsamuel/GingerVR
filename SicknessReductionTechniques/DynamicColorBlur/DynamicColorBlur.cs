using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DynamicColorBlur : MonoBehaviour
{
    public Material blurMaterial; //create material from shader and attatch here
    public float sigmaMaximum = 15f;
    public float angularSpeedThreshold = 40f;
    public float angularSpeedModifier = 0.0001f;
    [Range(0, 10)]
    public int smoothness;

    [Range(0,4)]
    public float brightnessThreshold;

    [Range(0,1)]
    public float redThreshold;

    [Range(0,1)]
    public float greenThreshold;

    [Range(0,1)]
    public float blueThreshold;

    public bool flipThresholds = false;


    [Range(.01f,10)]
    public float sigma;
    float[] kernel;

    void Start(){
        //initialize to some random matrix
        if(blurMaterial == null){
            blurMaterial = Resources.Load("GingerVR-master/SicknessReductionTechniques/DynamicColorBlur/ColorBlurMat") as Material;
        }
        kernel = new float[121];
        angularSpeed = new Vector3(0,0,0);
        StartCoroutine(TrackAngularSpeed());
    }

    
    Vector3 angularSpeed;
    IEnumerator TrackAngularSpeed(){
        Vector3 lastRotation = new Vector3(0,0,0);
        while(true){
            yield return null;
            Vector3 delta = transform.localEulerAngles - lastRotation;
            lastRotation = transform.localEulerAngles;
            angularSpeed = delta / Time.deltaTime;
        }
    }

    public bool useEditorValue;
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        
        if(!useEditorValue){
            sigma = Mathf.Lerp(0.01f, sigmaMaximum, (angularSpeed.magnitude - angularSpeedThreshold) * angularSpeedModifier );
        }

        kernel = new float[121];
        //initialize to some 
        for(int x = 0; x<11; x++){
            for(int y = 0; y<11; y++){
                kernel[y*11 + x] = GaussianFunction(x-5.0f, y-5.0f,sigma); //update kernel
            }
        }
        //calculate sum for later
        float kernelSum = 0;
        for(int i = 0; i<kernel.Length; i++){
            kernelSum+= kernel[i];
        }

        for(int i = 0; i<kernel.Length; i++){
            kernel[i] *= (1f/kernelSum);
        }
        
        //Debug.Log(kernel[4]);
        //Debug.Log(kernel[5]);

        //communicate kernel through skybox
        //float[] kernel = {2f,2fs,2f};
        blurMaterial.SetFloatArray("_kernel",kernel);
        blurMaterial.SetFloat("_kernelSum", kernelSum);
        blurMaterial.GetFloatArray("_kernel");
        blurMaterial.SetFloat("_brightnessThreshold", brightnessThreshold);
        blurMaterial.SetFloat("_redThreshold", redThreshold);
        blurMaterial.SetFloat("_greenThreshold", greenThreshold);
        blurMaterial.SetFloat("_blueThreshold", blueThreshold);
        if(flipThresholds){
            blurMaterial.SetFloat("_darkSaliency", 1);
        }else{
            blurMaterial.SetFloat("_darkSaliency", 0);
        }
        

        RenderTexture renderTexture = RenderTexture.GetTemporary(src.width, src.height);

        Graphics.Blit(src, renderTexture); //copies source texture to destination texture

        //apply the render texture as many iterations as specified
        for (int i = 0; i < smoothness; i++)
        {
            RenderTexture tempTexture = RenderTexture.GetTemporary(src.width, src.height); //creates a quick temporary texture for calculations
            Graphics.Blit(renderTexture, tempTexture, blurMaterial);
            RenderTexture.ReleaseTemporary(renderTexture); //releases the temporary texture we got from GetTemporary 
            renderTexture = tempTexture;
        }

        Graphics.Blit(renderTexture, dst);
        RenderTexture.ReleaseTemporary(renderTexture);

       
    }

    float GaussianFunction(float x, float y, float sigma){
        float p1 = 1f/ ((2f*Mathf.PI) * Mathf.Pow(sigma,2f));
        float eExponent = -(Mathf.Pow(x,2) + Mathf.Pow(y,2)) / (2*Mathf.Pow(sigma,2));
        float answer = p1 * Mathf.Exp(eExponent);
        return answer;
    }
}