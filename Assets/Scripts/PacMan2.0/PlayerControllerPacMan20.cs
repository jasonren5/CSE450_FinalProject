using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPacMan20 : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer sprite;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        animator.SetFloat("Speed",rigidbody.velocity.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody.AddForce(Vector2.left * 20f);
            sprite.flipX = true;
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            rigidbody.AddForce(Vector2.right * 20f);
            sprite.flipX = false;
        }
         if(Input.GetKey(KeyCode.UpArrow)) {
            rigidbody.AddForce(Vector2.up* 20f);
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            rigidbody.AddForce(Vector2.down * 20f);
            
        }
        
    }
}
