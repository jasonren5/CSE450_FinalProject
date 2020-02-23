using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Token : MonoBehaviour
{
    //Set up collision event with the player to destroy the token
    void OnCollisionEnter2D(Collision2D other) {
        //Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()) {
            Destroy(gameObject);
        }
    }
}
