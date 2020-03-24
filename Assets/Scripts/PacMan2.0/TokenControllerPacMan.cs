using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TokenControllerPacMan : MonoBehaviour
{

    int tokensLeft = 7;
    

    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
        tokensLeft--;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
