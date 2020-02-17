using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed of the player character
    public float speed;
    public float maxSpeed;
    float sqrMaxSpeed;

    
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component at start to avoid excess calls to getComponent
        rb = GetComponent<Rigidbody2D>();
        //accessing sqrMagnitude is quicker than magnitude, so use that when checking speed and then square maxSpeed
        sqrMaxSpeed = maxSpeed * maxSpeed;

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += speed * Vector2.up * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += speed * Vector2.left * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += speed * Vector2.down * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += speed * Vector2.right * Time.deltaTime;
        }

        
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
