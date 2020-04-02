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
        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawn + spawnDelay)
        {
            Instantiate(entityToSpawn);
            lastSpawn = Time.time;
        }
    }
}
