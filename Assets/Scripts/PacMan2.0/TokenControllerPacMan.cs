using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TokenControllerPacMan : MonoBehaviour
{

    int tokensCollected = 0;
    int totalTokens = 7;

    int order;

    bool isCollected = false;
    Vector3 startingPosition;

    Transform playerRef;
    Vector3 velocity = Vector3.zero;
    

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.GetComponent<PlayerControllerPacMan20>())
        {

            if (this.GetComponent<Rigidbody2D>())
            {
                this.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            if (this.GetComponent<Collider>())
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

            tokensCollected++;
            order = tokensCollected;

            isCollected = true;
            playerRef = collision.gameObject.transform;
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollected)
        {
            transform.position = startingPosition + new Vector3(0, (Mathf.Sin(Time.time * 2)) / 10, 0);
        }
        if (isCollected)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerRef.transform.position, ref velocity, .35f * order);
        }
    }
}
