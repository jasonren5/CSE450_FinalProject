using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEnemyController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    float sqrMaxSpeed;

    //determines the radius that the player needs to touch for the enemy to 'detect' the player
    // (this should probably be smaller than the follow distance)
    public float detectionRadius;

    //determines how far the player needs to be for them to 'lose sight' of the player
    public float followDistance;
    Rigidbody2D rigidbody;
    Ray ray = new Ray();

    int mask = (1 << 11) | (1 << 8) | (1 << 7) | (1 << 6) | (1 << 5) | (1 << 4) | (1 << 3) | (1 << 2) | (1 << 1);

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
        rigidbody = GetComponent<Rigidbody2D>();
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
            Vector2 dir = (pursue - transform.position).normalized;
            
            rigidbody.velocity += speed * dir * Time.deltaTime;
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);

            if (dir.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            //check if it has reached the position that is is chasing, and if true, then
            //      exit pursuing state
            if (transform.position == pursue)
            {
                state = States.rest;
            }
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
                //need to check if there is anything between the player and the enemy
                Vector2 angleToPlayer = (hits[i].gameObject.transform.position - transform.position).normalized;
                //RaycastHit2D[] checkObstruction = Physics2D.RaycastAll(transform.position, angleToPlayer, detectionRadius, mask);
                RaycastHit2D checkObstruction = Physics2D.Raycast(transform.position, angleToPlayer, detectionRadius, mask);

                //hit something
                //for (int j = 0; j < checkObstruction.Length; j++)
                //{
                //    if (checkObstruction[j].collider != null)
                //    {
                //        Debug.Log("collider name: " + checkObstruction[j].collider.gameObject.name);

                //        //hit player
                //        if (checkObstruction[j].collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                //        {
                //            state = States.pursue;
                //            pursue = hits[i].gameObject.transform.position;
                //        }
                //    }
                //}

                //hit something

                if (checkObstruction.collider != null)
                {
                    Debug.Log("collider name: " + checkObstruction.collider.gameObject.name);

                    //hit player
                    if (checkObstruction.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        state = States.pursue;
                        pursue = new Vector3(hits[i].gameObject.transform.position.x, hits[i].gameObject.transform.position.y, -1);
                    }
                }
                



            }
        }
    }


    //Set up collision event with the other player
    void OnCollisionEnter2D(Collision2D other) {
        //Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()) {
            SoundManager.instance.PlaySoundDeath();
            SceneManager.LoadScene("Hub-2.0");
        }
    }
}
