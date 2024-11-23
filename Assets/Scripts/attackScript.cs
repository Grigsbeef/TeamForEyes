using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    
    public GameObject weapon;
    public GameObject Player;
    private LaunchArcRenderer arcVariables;
    
    
    // Start is called before the first frame update
    void Start()
    {
        arcVariables = Player.GetComponent<LaunchArcRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(arcVariables.getCurVelocity());
    }
}
