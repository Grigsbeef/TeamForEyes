using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementSprite : MonoBehaviour
{
    public Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x+moveSpeed, transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed);
        }
    }
}
