using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo pi;

    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    private List<Slot> slots;

    public System.Action<ItemProperty> onBuyBtn;
    

    void Start()
    {
        pi = GameObject.Find("Player").GetComponent<PlayerInfo>();

        slots = new List<Slot>();

        int slotCnt = slotRoot.childCount;

        for(int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if(i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }

            slots.Add(slot);
        }
    }

    public void OnBuyBtn(Slot slot)
    {
        if(onBuyBtn != null)
        {
            //소지금 비교
            if(pi.goldRetrun >= slot.item.price)
            {
                onBuyBtn(slot.item);
            }
        }
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
