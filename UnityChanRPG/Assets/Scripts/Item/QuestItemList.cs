using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemList : MonoBehaviour
{
    //[SerializeField]
    //private PlayerInfo pi;

    public QuestItemBuffer qItemBuffer;
    public Transform slotRoot;

    private List<Slot> slots;

    //인벤토리
    public GameObject player;
    public InventoryUI inven;


    void Start()
    {
        //pi = GameObject.Find("Player").GetComponent<PlayerInfo>();
        player = GameObject.Find("Player");
        inven = player.transform.Find("InvenCanvas").Find("Inventory").GetComponent<InventoryUI>();

        slots = new List<Slot>();

        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if (i < qItemBuffer.items.Count)
            {
                slot.SetItem(qItemBuffer.items[i]);
            }

            slots.Add(slot);
        }
    }

    public void OnDrop(Slot slot)
    {
        inven.DropItem(slot.item);
    }
}
