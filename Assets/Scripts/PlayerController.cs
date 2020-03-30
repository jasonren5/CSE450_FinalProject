using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //speed of the player character
    public float speed;
    //speed modifier when sprinting
    public float sprintModifier;
    public float maxSpeed;
    public int tokensLeft;
    float sqrMaxSpeed;

    float stamina = 10f;
    public float staminaDrain;
    public float staminaRegen;

    float drag;
    float angularDrag;

    //time since last sprint
    float lastSprint;

    
    Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Token>()) {
            --tokensLeft;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component at start to avoid excess calls to getComponent
        rb = GetComponent<Rigidbody2D>();
        //accessing sqrMagnitude is quicker than magnitude, so use that when checking speed and then square maxSpeed
        sqrMaxSpeed = maxSpeed * maxSpeed;

        drag = rb.drag;
        angularDrag = angularDrag;
        lastSprint = Time.time;

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += speed * Vector2.up * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += speed * Vector2.left * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += speed * Vector2.down * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += speed * Vector2.right * Time.deltaTime;
        }

        //if sprinting
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            rb.drag = drag / 2;

            if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed * sprintModifier * sprintModifier)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed * sprintModifier;
            }
            stamina -= staminaDrain / 10;
            lastSprint = Time.time;
        }
        //if not sprinting
        else
        {
            rb.drag = drag;
            if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            //if 2 seconds have passed since the last time the player was sprinting, begin regenerating stamina
            if (Time.time - lastSprint > 2f)
            {
                stamina += staminaRegen / 10;
            }
        }

        


        if(tokensLeft == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PortalController>())
        {
            SceneManager.LoadScene("Hub");
        }

        //level selector
        if (collision.gameObject.GetComponent<HubLevelSelector>())
        {
            SceneManager.LoadScene(collision.gameObject.GetComponent<HubLevelSelector>().sceneName);
        }
    }
}
