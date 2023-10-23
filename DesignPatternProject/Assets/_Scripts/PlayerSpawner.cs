using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject objectToInstantiate;
    public Transform[] spawnPoints;
    public GameObject[] playerUnits;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);

            //a range of distances from the spawn point

            //selectedSpawnPoint
            Transform selectedSpawnPoint = spawnPoints[randomSpawnIndex];

            int randomPlayerIndex = Random.Range(0, playerUnits.Length);

            objectToInstantiate = playerUnits[randomPlayerIndex];

            float spawnX = selectedSpawnPoint.position.x + Random.Range(-3f, 3f);
            float spawnZ = selectedSpawnPoint.position.z + Random.Range(-3f, 3f);
            Vector3 spawnPosition = new Vector3(spawnX, selectedSpawnPoint.position.y, spawnZ);
            Instantiate(objectToInstantiate, spawnPosition, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
