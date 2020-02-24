using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEnemyController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    float sqrMaxSpeed;
    public float detectionRadius;

    //this is used as the position that the enemy is chasing towards (generally
    //      the point where they last saw the player
    Vector3 pursue = new Vector3();


    
    //used with AI
    enum States
    {
        rest,
        pursue
    }

    States state;

    // Start is called before the first frame update
    void Start()
    {
        state = States.rest;

        //accessing sqrMagnitude is quicker than magnitude, so use that when checking speed and then square maxSpeed
        // (currently enemies do not utilize this)
        sqrMaxSpeed = maxSpeed * maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        checkDetection();
    }

    private void FixedUpdate()
    {
        if (state == States.pursue)
        {
            //follow player or whatever
            transform.position = Vector2.MoveTowards(transform.position, pursue, speed * Time.deltaTime);
        }
    }

    void checkDetection()
    {
        //raycasts in a circle to see if the player is within a certain radius of the object
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, detectionRadius);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                state = States.pursue;
                pursue = hits[i].gameObject.transform.position;
            }
        }
    }

    //Set up collision event with the other player
    void OnCollisionEnter2D(Collision2D other) {
        //Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
