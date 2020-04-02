using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    //outlet
    public string rollDirection;
    public float speed;

    //0 = up, 1 = right, 2 = down, 3 = left
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        if (rollDirection.ToLower() == "up")
        {
            direction = Vector2.up;
        }
        if (rollDirection.ToLower() == "left")
        {
            direction = Vector2.left;
        }
        if (rollDirection.ToLower() == "down")
        {
            direction = Vector2.down;
        }
        if (rollDirection.ToLower() == "right")
        {
            direction = Vector2.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("barrel collision");
        Destroy(this.gameObject);
    }
}
