// UMD IMDM290 
// Instructor: Myungin Lee
// All the same Lerp but using audio


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AudioLerp : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 700; 
    float time = 0f;
    Vector3[] initPos;
    Vector3[] startPosition, endPosition;
    float lerpFraction; // Lerp point between 0~1
    float t;
    GameObject mother;

    // Start is called before the first frame update
    void Start()
    {
        // Assign proper types and sizes to the variables.
        spheres = new GameObject[numSphere];
        initPos = new Vector3[numSphere]; // Start positions
        startPosition = new Vector3[numSphere]; 
        endPosition = new Vector3[numSphere]; 
        mother = GameObject.Find("HardStar");
        
        // Define target positions. Start = random, End = heart 
        for (int i =0; i < numSphere; i++){
            // Random start positions
            float r = 40f;
            startPosition[i] = new Vector3(r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f), r * Random.Range(-1f, 1f));        
            // Heart shape end position
            t = i* 2 * Mathf.PI / numSphere;
            endPosition[i] = new Vector3( 
                        5f*Mathf.Sqrt(2f) * Mathf.Sin(t) *  Mathf.Sin(t) *  Mathf.Sin(t),
                        5f* (- Mathf.Cos(t) * Mathf.Cos(t) * Mathf.Cos(t) - Mathf.Cos(t) * Mathf.Cos(t) + 2 *Mathf.Cos(t)) + 3f,
                        10f + Mathf.Sin(time));
        }
        // Let there be spheres..
        for (int i =0; i < numSphere; i++){
            float r = 10f; // radius of the circle
            // Draw primitive elements:
           
            spheres[i] =  Instantiate(mother); 

            // Position
            initPos[i] = startPosition[i];
            spheres[i].transform.position = initPos[i];
            spheres[i].transform.localRotation = Quaternion.EulerAngles(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));

            // Color
            // Get the renderer of the spheres and assign colors.
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // HSV color space: https://en.wikipedia.org/wiki/HSL_and_HSV
        
            float blueHue = (float)i / numSphere; // Hue cycles through 0 to 1
           Color color = Color.HSVToRGB(Random.Range(0.50f,0.90f), 3f, Random.Range(1f, 5f)); // Full saturation and 

            sphereRenderer.material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Measure Time 
        time += Time.deltaTime; // Time.deltaTime = The interval in seconds from the last frame to the current one
        // what to update over time?
        //starting at 80 to have some stars drifiting
        for (int i =80; i < numSphere; i++){
            // Lerp : Linearly interpolates between two points.
           
            
            // lerpFraction variable defines the point between startPosition and endPosition (0~1)
            // let it oscillate over time using sin function
            lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;

            // Lerp logic. Update position       
            t = i* 2 * Mathf.PI / numSphere;
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);
        
            // For now, start positions and end positions are fixed. But what if you change it over time?
            // startPosition[i]; endPosition[i];

        
        }
    }
}