using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class PlayerInfo : MonoBehaviour
{
    public int MaxHP = 2000;
    public int CurrHP;
    public int ATK;

    private int gold { get; set; } = 3000;

    public int npcNum;

    public GameObject ui;
    //[SerializeField] private GameObject[] NPC;
    public GameObject dialBtn; //Button
    //북측 사냥터로 넘어갈 트리거 큐브 오브젝트
    public GameObject NFTrigger;
    //사냥터 필드로 넘어갔을 때 스폰되는 위치
    public GameObject cityToFieldPos;
    //북측 사냥터에서 다시 시티맵으로 돌아왔을 때 스폰되는 위치
    public GameObject fieldToCityPos;
    //최초 위치
    public GameObject startPos;

    //인벤토리
    private InventoryUI inven;

    //퀘스트 아이템
    public QuestItemList qItemList;
    public GameObject[] qiSlot;

    //씬 전환
    public bool isCityToField = false; //시티 -> 필드
    public bool isFieldToCity = false; //시티 <- 필드

    public int goldRetrun
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
        }
    }

    void Start()
    {
        ui = GameObject.Find("UI");
        gameObject.transform.position = startPos.transform.position;
        //gameObject.transform.position = fieldToCityPos.transform.position;
        dialBtn.gameObject.SetActive(false);
        CurrHP = MaxHP;
        inven = this.transform.Find("InvenCanvas").Find("Inventory").GetComponent<InventoryUI>();
    }

    /*void Update()
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Npc npc = other.gameObject.GetComponent<Npc>();

        //if (npc == null)
        //    return;

        if(other.tag == "NPC")
        {
            dialBtn.gameObject.SetActive(true);
            npcNum = npc.npcNum;
        }

        //시티에서 필드로 이동 트리거
        if(SceneManager.GetActiveScene().name == "CityScene" && other.tag == "FieldGate")
        {
            //SceneManager.LoadScene("FieldScene");
            LoadingSceneManager.LoadScene("FieldScene");
            isFieldToCity = false;
            isCityToField = true;
        }
        //필드에서 시티로 이동 트리거
        if (SceneManager.GetActiveScene().name == "FieldScene" && other.tag == "CityGate")
        {
            //SceneManager.LoadScene("CityScene");
            LoadingSceneManager.LoadScene("CityScene");
            isCityToField = false;
            isFieldToCity = true;
        }

        //아이템, 골드 드랍 획득
        if (SceneManager.GetActiveScene().name == "FieldScene" && other.tag == "Gold")
        {
            int goldAdd = Random.Range(350, 500);
            goldRetrun += goldAdd;
            print("소지금 : " + gold);
            Destroy(other.gameObject);
        }

        if (SceneManager.GetActiveScene().name == "FieldScene" && other.tag == "Item")
        {
            //퀘스트 아이템 검사
            int slotNum = Random.Range(0, 3);
            print("아이템 드랍");
            inven.DropItem(qiSlot[slotNum].GetComponent<Slot>().item);
            //qItemList.OnDrop(qiSlot[slotNum].GetComponent<Slot>());
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            dialBtn.gameObject.SetActive(false);
        }
    }

    //공격력 전달용
    public int Atk()
    {
        ATK = Random.Range(75, 110);
        return ATK;
    }
}
