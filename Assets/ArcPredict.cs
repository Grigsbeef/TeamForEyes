using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public float velocity;
    public float angle;
    public int resolution;

    float g; // force of gravity on the y axis
    float radianAngle;

    private void Awake(){
        lr = GetComponent<LineRenderer> ();
        g = Mathf.Abs (Physics2D.gravity.y);
    }

    private void OnValidate(){
        if (lr != null && Application.isPlaying){
            RenderArc ();
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        RenderArc();
    }

    // populating the LineRender with the appropriate settings
    void RenderArc()
    {
        lr.positionCount = resolution + 1;
        lr.SetPositions (CalculateArcArray ());
    }

    //create an array of Vector 3 Positions for arc
    Vector3[] CalculateArcArray(){
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++) {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint (t, maxDistance);
        }

        return arcArray;

    }

    //calculate height and distance of each vertex
    Vector3 CalculateArcPoint(float t, float maxDistance){
        float x = t;
        float y = x * Mathf.Tan(radianAngle)-((g*x*x)/(2 * velocity * velocity * Mathf.Cos(radianAngle)));
        return new Vector3(x,y);
    }
}