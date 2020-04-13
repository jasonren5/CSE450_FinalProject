using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Text badgesText;

    public GameObject portalPrefab;

    
    Rigidbody2D rb;
    Animator _animator;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Token>()) {
            --tokensLeft;
            if(tokensLeft == 0){
                badgesText.text = "0 Badges Remaining! Now find the portal and get out of here!";
                GameObject portal = Instantiate(portalPrefab);
                portal.transform.position = new Vector3(-1.485357f, -3.226716f, 0);
            }
            else{
                badgesText.text = tokensLeft + " Badges Remaining";
            }
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component at start to avoid excess calls to getComponent
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += speed * Vector2.left * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += speed * Vector2.down * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
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
            if (Time.time - lastSprint > 2f && stamina < 10f)
            {
                stamina += staminaRegen / 10;
            }
        }

        _animator.SetFloat("speed", rb.velocity.magnitude);
        if(rb.velocity.magnitude > 0.1f){
            _animator.SetFloat("movementX", rb.velocity.x);
            _animator.SetFloat("movementY", rb.velocity.y); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PortalController>())
        {
            SceneManager.LoadScene("Hub-2.0");
        }


        //level selector
        if (collision.gameObject.GetComponent<HubLevelSelector>())
        {
            SceneManager.LoadScene(collision.gameObject.GetComponent<HubLevelSelector>().sceneName);
        }
    }
}
