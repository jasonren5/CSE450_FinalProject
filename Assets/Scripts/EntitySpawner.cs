using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityToSpawn;
    public float spawnDelay;
    float lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawn = Time.time - spawnDelay;
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawn + spawnDelay)
        {
            Instantiate(entityToSpawn, new Vector3(transform.position.x, transform.position.y, 1), entityToSpawn.transform.rotation);
            lastSpawn = Time.time;
        }
    }
}
