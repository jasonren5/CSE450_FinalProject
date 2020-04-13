using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Token : MonoBehaviour
{
    Vector3 startingPosition;
    bool isCollected = false;
    int order = 0;
    Transform playerRef;
    Vector3 velocity = Vector3.zero;
    private void Start()
    {
        startingPosition = transform.position;
    }


    //Set up collision event with the player to destroy the token
    void OnCollisionEnter2D(Collision2D other) {
        //Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()) {
            //increase the amount of tokens the player is carrying and then get that number
            other.gameObject.GetComponent<PlayerController>().incTokensCarrying();
            order = other.gameObject.GetComponent<PlayerController>().getNumTokensCarrying();

            //if for some reason the token has a rigidbody2d component, disable it -- we dont want the player colliding
            //  with a token that they collected
            if (this.GetComponent<Rigidbody2D>())
            {
                this.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            if(this.GetComponent<Collider>())
            {
                this.GetComponent<Collider>().enabled = false;
            }
            if (this.GetComponent<PolygonCollider2D>())
            {
                this.GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (this.GetComponent<CircleCollider2D>())
            {
                this.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (this.GetComponent<CapsuleCollider2D>())
            {
                this.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            if (this.GetComponent<BoxCollider2D>())
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }

            //set reference for player transform to follow
            playerRef = other.gameObject.transform;
            isCollected = true;

            //decrease size
            transform.localScale = transform.localScale * .75f;
        }
    }

    private void Update()
    {
        if (!isCollected)
        {
            transform.position = startingPosition + new Vector3(0, (Mathf.Sin(Time.time * 2)) / 15, 0);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerRef.transform.position, ref velocity, .35f * order);
        }
        
    }
}
