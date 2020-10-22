using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DialogueText
{
    [TextArea]
    public string npcName;
    public string dialogue;

}

public class Dialogue : MonoBehaviour
{
    public GameObject player;
    private PlayerInfo pi;
    [SerializeField] private ShopUI shop;
    [SerializeField] private InventoryUI inven;
    
    //private GameObject[] NPC;
    [SerializeField] private Image sprite_DialogueBar;
    [SerializeField] private Text NpcName;
    [SerializeField] private Text textDialouge;

    [SerializeField] private DialogueText[] dialogue;

    private bool isDialogue = false;
    private int count = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        pi = player.GetComponent<PlayerInfo>();
        shop = shop.GetComponent<ShopUI>();
        inven = player.transform.Find("InvenCanvas").Find("Inventory").gameObject.GetComponent<InventoryUI>();
    }

    public void OnOffDialogue(bool onOff)
    {
        gameObject.SetActive(onOff);
        sprite_DialogueBar.gameObject.SetActive(onOff);
        NpcName.gameObject.SetActive(onOff);
        textDialouge.gameObject.SetActive(onOff);
        isDialogue = onOff;
    }
    public void ShowDialogue()
    {
        OnOffDialogue(true);
        count = 0;
        Dialoguing(pi.npcNum);
        NpcName.text = dialogue[0].npcName;
        textDialouge.text = dialogue[0].dialogue;
        NextDialogue();
    }

    private void NextDialogue()
    {
        textDialouge.text = dialogue[count].dialogue;
        count++;
    }

    void Update()
    {
        if(isDialogue)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (count < dialogue.Length)
                    NextDialogue();
                else
                {
                    OnOffDialogue(false);
                    if (pi.npcNum == 4)
                    {
                        OnOffDialogue(false);
                        shop.gameObject.SetActive(true);
                        inven.gameObject.SetActive(true);
                    }
                }
                    
            }
        }
    }

    public void Dialoguing(int num)
    {
        switch (num)
        {
            case 1:
                Array.Resize<DialogueText>(ref dialogue, 3);
                dialogue[0].npcName = "큐리에";
                dialogue[0].dialogue = "잠깐! 너, 처음이지?" + System.Environment.NewLine + "좌측 하단의 이동 조이패드와 우측 하단의 카메라 회전 조이패드로 조작 할 수 있어";
                dialogue[1].npcName = "큐리에";
                dialogue[1].dialogue = "이 앞의 좁은 길을 따라가다 보면 작은 다리가 보이는데 그곳에 어떤 녀석이 있을거야.";
                dialogue[2].npcName = "큐리에";
                dialogue[2].dialogue ="그 녀석에게 말 걸어봐";
                break;
            case 2:
                Array.Resize<DialogueText>(ref dialogue, 1);
                dialogue[0].npcName = "블랙큐";
                dialogue[0].dialogue = "뭐지 넌? 처음보는 녀석인데";
                break;
            case 3:
                Array.Resize<DialogueText>(ref dialogue, 2);
                dialogue[0].npcName = "화이큐";
                dialogue[0].dialogue = "잠깐, 너 강해보이는데, 근교 가도에서 출현하는 드래곤 몬스터좀 잡아줄래?";
                dialogue[1].npcName = "화이큐";
                dialogue[1].dialogue = "잡으면 드래곤 그림들을 얻을 수 있을거야. 종류별로 하나씩 모아서 가져오면 좋은걸 줄게";
                break;
            case 4:
                Array.Resize<DialogueText>(ref dialogue, 1);
                dialogue[0].npcName = "사쿠라";
                dialogue[0].dialogue = "어서오세요 ^^" + System.Environment.NewLine + "필요한 물건 있으시면 골라보세요. ^^";
                break;
        }

    }
}
