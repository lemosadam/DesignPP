using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerSmallPrefab;
    public GameObject playerHeavyPrefab;
    private Dictionary<string, IPlayerPrototype> playerPrototypes = new Dictionary<string, IPlayerPrototype>();
    [SerializeField] protected Transform selectedSpawnPoint;
    public Vector3 spawnPosition;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InitializePlayerPrototypes();
    }

    private void InitializePlayerPrototypes()
    {
        var playerSmallPrototype = new PlayerSmall();
        playerSmallPrototype.Initialize(playerSmallPrefab);
        playerPrototypes.Add("PlayerSmall", playerSmallPrototype);

        var playerHeavyPrototype = new PlayerHeavy();
        playerHeavyPrototype.Initialize(playerHeavyPrefab);
        playerPrototypes.Add("PlayerHeavy", playerHeavyPrototype);
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

    public GameObject CreatePlayerHeavyClone(Vector3 spawnPosition, Quaternion rotation)
    {
        if (playerPrototypes.ContainsKey("PlayerHeavy"))
        {
            return playerPrototypes["PlayerHeavy"].Clone(spawnPosition, rotation);
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

    public void SpawnPlayerSmall()
    {
        gameManager.money -= 3;
        CreatePlayerSmallClone(spawnPosition, Quaternion.identity);
    }

    public void SpawnPlayerHeavy()
    {
        gameManager.money -= 10;
        CreatePlayerHeavyClone(spawnPosition, Quaternion.identity);
    }
}
