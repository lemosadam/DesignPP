using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToInstantiate; 
    public Transform[] spawnPoints; 
    public GameObject[] enemies;

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

            int randomEnemyIndex = Random.Range(0, enemies.Length);

            objectToInstantiate = enemies[randomEnemyIndex];

            float spawnX = selectedSpawnPoint.position.x + Random.Range(-5f, 5f);
            float spawnZ = selectedSpawnPoint.position.z + Random.Range(-5f, 5f); 
            Vector3 spawnPosition = new Vector3(spawnX, selectedSpawnPoint.position.y, spawnZ);
            Instantiate(objectToInstantiate, spawnPosition, Quaternion.Euler(0f, 180f, 0f));
        }
    }
}
