using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemySmallPrefab;
    public GameObject enemyHeavyPrefab;
    

    private Dictionary<string, IEnemyPrototype> prototypes = new Dictionary<string, IEnemyPrototype>();
    
    public Transform[] spawnPoints;

    void Start()
    {
        InitializePrototypes();
    }

    private void InitializePrototypes()
    {
        var enemySmallPrototype = new EnemySmall();
        enemySmallPrototype.Initialize(enemySmallPrefab);
        prototypes.Add("EnemySmall", enemySmallPrototype);

        var enemyHeavyPrototype = new EnemyHeavy();
        enemyHeavyPrototype.Initialize(enemyHeavyPrefab);
        prototypes.Add("EnemyHeavy", enemyHeavyPrototype);

    }

    public GameObject CreateRandomEnemyClone(Vector3 position, Quaternion rotation)
    {
        // Randomly select between "EnemySmall" and "EnemyHeavy"
        string[] enemyTypes = { "EnemySmall", "EnemyHeavy" };
        string randomEnemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];

        if (prototypes.ContainsKey(randomEnemyType))
        {
            return prototypes[randomEnemyType].Clone(position, rotation);
        }
        else
        {
            Debug.LogError("Key: " + randomEnemyType + " not found in dictionary.");
            return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedSpawnPoint = spawnPoints[randomSpawnIndex];

            float spawnX = selectedSpawnPoint.position.x + Random.Range(-2f, 2f);
            float spawnZ = selectedSpawnPoint.position.z + Random.Range(-2f, 2f);
            Vector3 spawnPosition = new Vector3(spawnX, selectedSpawnPoint.position.y, spawnZ);

            CreateRandomEnemyClone(spawnPosition, Quaternion.Euler(0f, 180f, 0f));
        }

       
    }
}
