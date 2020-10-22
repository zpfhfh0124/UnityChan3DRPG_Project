using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private GameObject playerSpawnPos;
    [SerializeField] private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null) player.transform.position = playerSpawnPos.transform.position;
    }

    void Update()
    {
        
    }
}
