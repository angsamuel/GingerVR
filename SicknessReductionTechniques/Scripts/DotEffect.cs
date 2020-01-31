using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotEffect : MonoBehaviour
{
    public GameObject subject; //camera or other object we want to attach the effect to
    public GameObject dot;

    [Range(0,20)]
    public float spacing = 20;

    [Range(0,10)]
    public float dotSize;

    public int matrixSize = 12;

    List<GameObject> dots;
    Vector3 cachedPosition;

    // Start is called before the first frame update
    void Start()
    {

        dots = new List<GameObject>();
        transform.position = subject.transform.position;
        cachedPosition = transform.position;

        for(int x = -matrixSize/2; x < matrixSize/2; x++){
            for(int y = -matrixSize/2; y < matrixSize/2; y++){
                for(int z = -matrixSize/2; z < matrixSize/2; z++){
                    GameObject newDot = Instantiate(dot, transform);
                    newDot.transform.localScale = new Vector3(dotSize,dotSize,dotSize);
                    dots.Add(newDot);
                    newDot.transform.position = new Vector3(x*spacing,y*spacing,z*spacing) + transform.position;
                }
            }
        }
        
        
    }




    // Update is called once per frame
    void Update()
    {
            
            Vector3 velocity = subject.transform.position - cachedPosition;
            cachedPosition = subject.transform.position;

            transform.position += velocity * 2;

            if(Mathf.Abs(transform.position.x - subject.transform.position.x) > spacing){
                transform.position = new Vector3(subject.transform.position.x,transform.position.y, transform.position.z);
            }

            if(Mathf.Abs(transform.position.y - subject.transform.position.y) > spacing){
                transform.position = new Vector3(transform.position.x,subject.transform.position.y, transform.position.z);
            }

            if(Mathf.Abs(transform.position.z - subject.transform.position.z) > spacing){
                transform.position = new Vector3(transform.position.x,transform.position.y, subject.transform.position.z);
            }
        
        

  
    }
}
