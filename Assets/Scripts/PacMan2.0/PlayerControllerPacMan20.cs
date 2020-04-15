using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerPacMan20 : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer sprite;
    Animator animator;


    int tokensCollected = 0;
    int totalTokens = 7;

    public Text tokenText;


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

    bool rotatedUp = false;
    bool rotatedDown = false;

    // Update is called once per frame
    //logically the rotation code doesn't make sense,
    //but the code works to rotate pacman correctly
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            sprite.flipX = true;
             
            if(rotatedUp){
                transform.Rotate(Vector3.forward * 90);
                rotatedUp= false;
            }
            else if(rotatedDown){
                transform.Rotate(Vector3.forward * -90);
                rotatedDown = false;
            }
            
            rigidbody.AddForce(Vector2.left * 20f);
            

        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            sprite.flipX = false;
            
            if(rotatedUp){
                transform.Rotate(Vector3.forward * 90);
                rotatedUp = false;
            }
            else if(rotatedDown){
                transform.Rotate(Vector3.forward * -90);
                rotatedDown = false;
            }
            
            rigidbody.AddForce(Vector2.right * 20f);
            
        }
         if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            sprite.flipX = false;


            if (!rotatedUp && !rotatedDown)
            {
                transform.Rotate(Vector3.forward * 90);

            }
            else if (rotatedUp)
            {
                transform.Rotate(Vector3.forward * 180);
                rotatedUp = false;
            }

            //logically doesn't make sense, but the code works to rotate correctly
            rotatedDown = true;
            rotatedUp = false;
            
            rigidbody.AddForce(Vector2.up* 20f);
            
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            sprite.flipX = false;

            if (!rotatedUp && !rotatedDown)
            {
                transform.Rotate(Vector3.forward * -90);
            }
            else if (rotatedDown)
            {
                transform.Rotate(Vector3.forward * -180);
                rotatedDown = false;
            }
            //logically doesn't make sense, but the code works to rotate correctly
            rotatedUp = true;
            rotatedDown = false;

            rigidbody.AddForce(Vector2.down * 20f);
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Token"))
        {
            other.gameObject.SetActive(false);
            tokensCollected++;
            if(tokensCollected > PlayerPrefs.GetInt("pacManBestScore"))
            {
                PlayerPrefs.SetInt("pacManBestScore", tokensCollected);
            }
            updateTokenText();
        }
    }

    void updateTokenText() {
        tokenText.text = tokensCollected.ToString() + "/" + totalTokens.ToString()
            + " Tokens";
    }

}
