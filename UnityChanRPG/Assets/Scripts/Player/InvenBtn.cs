using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InvenBtn : MonoBehaviour
{
    GameObject player;
    PlayerInfo pi;
    GameObject invenUI;
    Button btn;

    void Start()
    {
        player = GameObject.Find("Player");
        invenUI = player.transform.Find("InvenCanvas").Find("Inventory").gameObject;
        btn = this.transform.GetComponent<Button>();
        //print("여긴오나?");
        //btn.onClick.AddListener(OpenInven);
    }

    void Update()
    {
        
    }

    //void OpenInven()
    //{
    //    print("나오나?");
    //    invenUI.GetComponent<InventoryUI>().OpenInven();
    //}
}
