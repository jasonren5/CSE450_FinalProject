using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    // Start is called before the first frame update
    Transform transform;
    Vector3 localScale;
    void Start()
    {
        transform = GetComponent<Transform>();
        localScale = transform.localScale;
    }   

    // Update is called once per frame
    void Update()
    {
        float scale = (Mathf.Sin(Time.time * 2) / 4) + 1;
        transform.localScale = localScale * scale;
    }

}
