using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 5f;
    public float jumpHeight = 100f;
    public float jumpForward = 2f;
    float horizontal;
    float vertical;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();   
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
       horizontal = Input.GetAxisRaw("Horizontal");
       //vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //rb2d.velocity = new Vector2(vertical, speed);
        if (Input.GetKey(KeyCode.W) && isGrounded == true)
        {
            Jump();
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(-transform.right * speed, ForceMode2D.Impulse);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;
        }
    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
        //rb2d.AddForce(transform.forward * jumpHeight, ForceMode2D.Impulse);
        isGrounded = false;
    }

}
