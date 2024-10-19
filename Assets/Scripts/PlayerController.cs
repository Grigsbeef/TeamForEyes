using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 5f;
    public float jumpHeight = 3f;
    public float jumpForward = 2f;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();   
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
       vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space))
        {
            rb2d.AddForce(transform.up * jumpHeight);
            rb2d.AddForce(transform.forward * jumpHeight);
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, vertical * speed);
        
    }

    void Jump()
    {
        
    }

}
