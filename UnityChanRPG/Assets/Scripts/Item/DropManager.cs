using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    //드랍용
    public GameObject enemyPrefab;
    private EnemyController ec;

    public GameObject itemPrefab;
    public GameObject goldPrefab;

    private void Start()
    {
        ec = enemyPrefab.GetComponent<EnemyController>();
    }

    public void DropItem(Vector3 pos)
    {
        Instantiate(itemPrefab, pos, Quaternion.identity, null);
        Instantiate(goldPrefab, new Vector3(pos.x + 0.2f, pos.y, pos.z + 0.2f), Quaternion.identity, null);
        print("아이템 생성");
    }
}
