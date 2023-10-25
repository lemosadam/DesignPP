using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public PlayerSpawner playerSpawner;

    public TMP_Text moneyText;
    public int money = 30;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemySmall = spawnManager.CreateRandomEnemyClone(new Vector3(0, 0, 0), Quaternion.identity);
        GameObject enemyHeavy = spawnManager.CreateRandomEnemyClone(new Vector3(0, 0, 0), Quaternion.identity);
        GameObject playerSmall = playerSpawner.CreatePlayerSmallClone(new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + money.ToString();
    }
}
