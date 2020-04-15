using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerPacMan20 : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer sprite;
    Animator animator;


    int tokensCollected = 0;
    int totalTokens = 7;

    public Text tokenText;
    public Image sprintBar;
    public float speed;
    public float maxSpeed;
    float stamina;
    public float maxStamina;
    float sprintModifier = 2f;
    float staminaDrain = .6f;
    float staminaRegen = .8f;

    float lastSprint;

    


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        stamina = maxStamina;
        lastSprint = Time.time;
    }


    bool rotatedUp = false;
    bool rotatedDown = false;

    // Update is called once per frame
    //logically the rotation code doesn't make sense,
    //but the code works to rotate pacman correctly
    void FixedUpdate()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;

            if (rotatedUp)
            {
                transform.Rotate(Vector3.forward * 90);
                rotatedUp = false;
            }
            else if (rotatedDown)
            {
                transform.Rotate(Vector3.forward * -90);
                rotatedDown = false;
            }

            rigidbody.AddForce(Vector2.left * speed);


        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;

            if (rotatedUp)
            {
                transform.Rotate(Vector3.forward * 90);
                rotatedUp = false;
            }
            else if (rotatedDown)
            {
                transform.Rotate(Vector3.forward * -90);
                rotatedDown = false;
            }

            rigidbody.AddForce(Vector2.right * speed);

        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            sprite.flipX = false;


            if (!rotatedUp && !rotatedDown)
            {
                transform.Rotate(Vector3.forward * 90);

            }
            else if (rotatedUp)
            {
                transform.Rotate(Vector3.forward * 180);
                rotatedUp = false;
            }

            //logically doesn't make sense, but the code works to rotate correctly
            rotatedDown = true;
            rotatedUp = false;

            rigidbody.AddForce(Vector2.up * speed);

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            sprite.flipX = false;

            if (!rotatedUp && !rotatedDown)
            {
                transform.Rotate(Vector3.forward * -90);
            }
            else if (rotatedDown)
            {
                transform.Rotate(Vector3.forward * -180);
                rotatedDown = false;
            }
            //logically doesn't make sense, but the code works to rotate correctly
            rotatedUp = true;
            rotatedDown = false;

            rigidbody.AddForce(Vector2.down * speed);

        }

        //sprint code
        //if sprinting
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {

            if (rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed * sprintModifier * sprintModifier)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed * sprintModifier;
            }
            stamina -= staminaDrain / 10;
            lastSprint = Time.time;
        }
        //if not sprinting
        else
        {
            if (rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
            }

            //if 2 seconds have passed since the last time the player was sprinting, begin regenerating stamina
            if (Time.time - lastSprint > 2f && stamina < 10f)
            {
                stamina += staminaRegen / 10;
            }
        }

        sprintBar.fillAmount = stamina / 10f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Token"))
        {
            other.gameObject.SetActive(false);
            tokensCollected++;
            if(tokensCollected > PlayerPrefs.GetInt("pacManBestScore"))
            {
                PlayerPrefs.SetInt("pacManBestScore", tokensCollected);
            }
            updateTokenText();
        }

        if (other.gameObject.GetComponent<PortalController>())
        {
            SceneManager.LoadScene("Hub-2.0");

            if (other.gameObject.GetComponent<HubLevelSelector>())
            {
                SceneManager.LoadScene(other.gameObject.GetComponent<HubLevelSelector>().sceneName);
            }
        }


    
    }

    void updateTokenText() {
        tokenText.text = tokensCollected.ToString() + "/" + totalTokens.ToString()
            + " Tokens";
    }



}
