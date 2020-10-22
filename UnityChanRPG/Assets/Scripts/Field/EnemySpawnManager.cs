using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    public GameObject[] spawnPos;
    
    float spawnDelay = 2.0f;
    float spawnTimer = 0f;

    private void Start()
    {
        for(int i = 0; i < spawnPos.Length; i++)
        {
            Instantiate(enemyPrefab, spawnPos[i].transform.position, Quaternion.identity);
        }
    }


}
