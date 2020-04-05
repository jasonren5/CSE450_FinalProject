using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Token : MonoBehaviour
{
    Vector3 startingPosition;
    private void Start()
    {
        startingPosition = transform.position;
    }


    //Set up collision event with the player to destroy the token
    void OnCollisionEnter2D(Collision2D other) {
        //Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()) {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.position = startingPosition + new Vector3(0, (Mathf.Sin(Time.time * 2)) / 15, 0);
    }
}
