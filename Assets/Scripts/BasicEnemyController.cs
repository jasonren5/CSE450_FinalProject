using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float detectionRadius;

    
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
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.pursue)
        {
            //follow player or whatever
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
            }
        }
    }
}
