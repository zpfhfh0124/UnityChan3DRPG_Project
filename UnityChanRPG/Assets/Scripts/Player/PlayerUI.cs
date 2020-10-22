using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public GameObject quitMenu; //QuitMenu
    public GameObject hpBar;    //Slider

    private PlayerInfo pi;
    private Slider hpBarSlider;
    private Text hpText;
    private InventoryUI inven;
    private QuestItemList qItemList;

    //씬 전환 후 딜레이 용
    private float currTime = 0;
    private float sceneChangeDelay = 5.0f;

    void Start()
    {
        inven = this.transform.Find("InvenCanvas").Find("Inventory").GetComponent<InventoryUI>();
        pi = gameObject.GetComponent<PlayerInfo>();
        hpBar = GameObject.Find("HP_Bar");
        hpText = hpBar.GetComponentInChildren<Text>();
        hpBarSlider = hpBar.GetComponent<Slider>();
    }

    void Update()
    {
        hpBarSlider.value = (float)pi.CurrHP / (float)pi.MaxHP;
        hpText.text = pi.CurrHP.ToString() + " / " + pi.MaxHP.ToString();

        if(Input.GetKeyDown(KeyCode.Escape) && !quitMenu.gameObject.activeSelf)
        {
            quitMenu.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(SceneManager.GetActiveScene().ToString());
            Debug.Log("시티 -> 필드 : " + pi.isCityToField.ToString());
            Debug.Log("시티 <- 필드 : " + pi.isFieldToCity.ToString());
            Debug.Log("씬전환 딜레이 currTime : " + currTime.ToString());
        }

        //시티에서 필드로 나가면
        if (SceneManager.GetActiveScene().name == "FieldScene" && pi.isCityToField)
        {
            pi.ui = GameObject.Find("UI");
            pi.cityToFieldPos = GameObject.Find("SpawnPos");
            pi.transform.position = pi.cityToFieldPos.transform.position;

            quitMenu = GameObject.Find("UI").transform.Find("QuitMenu").gameObject;
            hpBar = GameObject.Find("HP_Bar");
            hpText = hpBar.GetComponentInChildren<Text>();
            hpBarSlider = hpBar.GetComponent<Slider>();
 
            qItemList = transform.Find("InvenCanvas").Find("QuestItemList").GetComponent<QuestItemList>();
            qItemList.player = GameObject.Find("Player");
            qItemList.qItemBuffer = GameObject.Find("QuestItemBuffer").GetComponent<QuestItemBuffer>();
            qItemList.inven = qItemList.player.transform.Find("InvenCanvas").Find("Inventory").GetComponent<InventoryUI>();
            qItemList.gameObject.SetActive(true);
            inven.gameObject.SetActive(true);

            currTime += Time.deltaTime;
            if(currTime > sceneChangeDelay)
            {
                qItemList.gameObject.SetActive(false);
                inven.gameObject.SetActive(false);
                pi.isCityToField = false;
                
                currTime = 0;
            }
        }
        //필드에서 시티로 들어오면
        if (SceneManager.GetActiveScene().name == "CityScene" && pi.isFieldToCity)
        {
            pi.ui = GameObject.Find("UI");
            pi.dialBtn = pi.ui.transform.Find("DialogueBtn").gameObject;
            pi.NFTrigger = GameObject.Find("NorthFieldTrigger");
            pi.fieldToCityPos = GameObject.Find("FieldToCityPos");
            pi.startPos = GameObject.Find("StartPos");
            pi.transform.position = pi.fieldToCityPos.transform.position;
            inven.shop = GameObject.Find("UI").transform.Find("Shop").gameObject.GetComponent<ShopUI>();

            quitMenu = GameObject.Find("UI").transform.Find("QuitMenu").gameObject;
            hpBar = GameObject.Find("HP_Bar");
            hpText = hpBar.GetComponentInChildren<Text>();
            hpBarSlider = hpBar.GetComponent<Slider>();

            currTime += Time.deltaTime;
            if (currTime > sceneChangeDelay)
            {
                pi.isFieldToCity = false;
                currTime = 0;
            }
        }
        
    }

    
}
