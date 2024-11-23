using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public Color startColor = Color.green;
    public Color endColor = Color.red;
    public float velocityIncrease;
    public float velocityLimit;
    public float angle;
    public int resolution;

    private float curVelocity;
    float velocity = 0;
    float g; // force of gravity on the y axis
    float radianAngle;
    bool keyDown = false;

    private void Awake(){
        lr = GetComponent<LineRenderer> ();
        g = Mathf.Abs (Physics2D.gravity.y);
    }

    private void OnValidate(){
        if (lr != null && Application.isPlaying){
            RenderArc ();
        }
    }

    public GameObject weapon;

    private GameObject playerObj = null;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        
        if (playerObj == null)
            playerObj = GameObject.Find("Player");

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

     private void Update()
    {
        Debug.Log("Player Position: X = " + playerObj.transform.position.x + " --- Y = " + playerObj.transform.position.y + " --- Z = " + 
        playerObj.transform.position.z);

        //While E is held down
        if (Input.GetKey(KeyCode.E))
        {
            keyDown = true;
            if (velocity >= velocityLimit)
                velocity = velocityLimit;
            else
                velocity += velocityIncrease;
            
            RenderArc();

        } else {
            if (keyDown) {
                keyDown = false;
                curVelocity = velocity;
                velocity = 0;
                lr.positionCount = 0;
                Instantiate(weapon, transform.position, quaternion.identity);
            }
        }
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
        float x = (t * maxDistance);
        float y = x * Mathf.Tan(radianAngle)-((g*x*x)/(2 * velocity * velocity * Mathf.Cos(radianAngle)));
        
        x += playerObj.transform.position.x;
        y += playerObj.transform.position.y;
        
        return new Vector3(x,y);
    }

    public float getCurVelocity(){
        return curVelocity;
    }
public GameObject getWeapon(){
    return weapon;
}

}
