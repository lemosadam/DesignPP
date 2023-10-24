using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject objectToInstantiate;
    public Transform[] spawnPoints;
    public GameObject[] playerUnits;
    public GameObject playerSmallPrefab;
    private Dictionary<string, IPlayerPrototype> playerPrototypes = new Dictionary<string, IPlayerPrototype>();
    [SerializeField] protected Transform selectedSpawnPoint;
    public Vector3 spawnPosition;

    void Start()
    {
        InitializePlayerPrototypes();
    }

    private void InitializePlayerPrototypes()
    {
        var playerSmallPrototype = new PlayerSmall();
        playerSmallPrototype.Initialize(playerSmallPrefab);
        playerPrototypes.Add("PlayerSmall", playerSmallPrototype);
    }
    public GameObject CreatePlayerSmallClone(Vector3 spawnPosition, Quaternion rotation)
    {
        if (playerPrototypes.ContainsKey("PlayerSmall"))
        {
            return playerPrototypes["PlayerSmall"].Clone(spawnPosition, rotation);
        }
        else
        {
            Debug.LogError("Key: PlayerSmall not found in dictionary.");
            return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           CreatePlayerSmallClone(spawnPosition, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
               SelectLane0();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SelectLane1();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SelectLane2();
        }
    }

    private void SelectLane0()
    {
        Transform selectedSpawnPoint = spawnPoints[0];
        float spawnX = selectedSpawnPoint.position.x + Random.Range(-3f, 3f);
        float spawnZ = selectedSpawnPoint.position.z + Random.Range(-3f, 3f);
        spawnPosition = new Vector3(spawnX, selectedSpawnPoint.position.y, spawnZ);
        
        
    }

    private void SelectLane1()
           {
               Transform selectedSpawnPoint = spawnPoints[1];
               float spawnX = selectedSpawnPoint.position.x + Random.Range(-3f, 3f);
               float spawnZ = selectedSpawnPoint.position.z + Random.Range(-3f, 3f);
               spawnPosition = new Vector3(spawnX, selectedSpawnPoint.position.y, spawnZ);
           }

    private void SelectLane2()
    {
        Transform selectedSpawnPoint = spawnPoints[2];
        float spawnX = selectedSpawnPoint.position.x + Random.Range(-3f, 3f);
        float spawnZ = selectedSpawnPoint.position.z + Random.Range(-3f, 3f);
        spawnPosition = new Vector3(spawnX, selectedSpawnPoint.position.y, spawnZ);
    }
}
