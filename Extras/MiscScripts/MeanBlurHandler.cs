using UnityEngine;

[ExecuteInEditMode]
public class MeanBlurHandler : MonoBehaviour
{
    public Material meanBlurMaterial; //create material from shader and attatch here

    [Range(0, 40)]
    public int intensity;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        RenderTexture renderTexture = RenderTexture.GetTemporary(src.width, src.height);

        Graphics.Blit(src, renderTexture); //copies source texture to destination texture

        //apply the render texture as many iterations as specified
        for (int i = 0; i < intensity; i++)
        {
            RenderTexture tempTexture = RenderTexture.GetTemporary(src.width, src.height); //creates a quick temporary texture for calculations
            Graphics.Blit(renderTexture, tempTexture, meanBlurMaterial);
            RenderTexture.ReleaseTemporary(renderTexture); //releases the temporary texture we got from GetTemporary 
            renderTexture = tempTexture;
        }

        Graphics.Blit(renderTexture, dst);
        RenderTexture.ReleaseTemporary(renderTexture);
    }
}