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

    //bool rotatedUp = false;
    //bool rotatedDown = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            sprite.flipX = true;
            /* 
            if(rotatedUp){
                transform.Rotate(Vector3.forward * 90);
                rotatedUp= false;
            }
            else if(rotatedDown){
                transform.Rotate(Vector3.forward * -90);
                rotatedDown = false;
            }
            */
            rigidbody.AddForce(Vector2.left * 20f);
            

        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            sprite.flipX = false;
            /* 
            if(rotatedUp){
                transform.Rotate(Vector3.forward * 90);
                rotatedUp = false;
            }
            else if(rotatedDown){
                transform.Rotate(Vector3.forward * -90);
                rotatedDown = false;
            }
            */
            rigidbody.AddForce(Vector2.right * 20f);
            
        }
         if(Input.GetKey(KeyCode.UpArrow)) {
            //sprite.flipX = false;
            /* 
            if(!rotatedUp && !rotatedDown){
                transform.Rotate(Vector3.forward * -90);
            }
            else if(rotatedDown){
                transform.Rotate(Vector3.forward * -180);
                rotatedDown = false;
            }

            rotatedUp = true;
            */
            rigidbody.AddForce(Vector2.up* 20f);
            
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            //sprite.flipX = false;
            /* 

            if(!rotatedUp && !rotatedDown){
                transform.Rotate(Vector3.forward * 90);
                
            }
            else if(rotatedUp){
                transform.Rotate(Vector3.forward * 180);
                rotatedUp = false;
            }

            rotatedDown = true;
            */
            rigidbody.AddForce(Vector2.down * 20f);
            
        }
        
    }
}
