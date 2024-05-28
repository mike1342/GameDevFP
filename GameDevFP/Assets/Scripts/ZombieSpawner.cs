using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject zombiePrefab;
    public int numToSpawn = 3;
    public float timeBetweenSpawns;
    int numSpawned = 0;
    float timeSinceLastSpawn = 0.0f;

    public float xMin = -20;
    public float xMax = 20;
    public float yMin = 5;
    public float yMax = 20;
    public float zMin = -20;
    public float zMax = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numSpawned < numToSpawn && timeSinceLastSpawn >= timeBetweenSpawns) {
            spawnZombie();
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    void spawnZombie() {
        Vector3 relativeZombiePosition;

        relativeZombiePosition.x = Random.Range(xMin, xMax);
        relativeZombiePosition.y = Random.Range(yMin, yMax);
        relativeZombiePosition.z = Random.Range(zMin, zMax);

        GameObject zombie = Instantiate(zombiePrefab, transform.position + relativeZombiePosition, transform.rotation)
            as GameObject;

        zombie.transform.parent = gameObject.transform;
        numSpawned++;
        timeSinceLastSpawn = 0.0f;
    }
}
