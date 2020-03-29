using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //this is the game object that the camera will follow
    public GameObject target;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private float cameraZ = 0;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        //save z pos
        cameraZ = transform.position.z;
        transform.position = target.transform.position;
        camera = GetComponent<Camera>();
    }


    private void FixedUpdate()
    {
        if (target != null)
        {
            
            Vector3 destination = target.transform.position;
            destination.z = cameraZ;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        
        
        }

    }
}
