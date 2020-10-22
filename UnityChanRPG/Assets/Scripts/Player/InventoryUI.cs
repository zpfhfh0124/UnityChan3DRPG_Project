using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
    public Transform slotRoot;
    public ShopUI shop;

    [SerializeField] GameObject player;
    [SerializeField] private Text goldAmount;

    private PlayerInfo pi;
    private List<Slot> slots;

    void Start()
    {
        player = GameObject.Find("Player");
        pi = player.GetComponent<PlayerInfo>();
        goldAmount.text = pi.goldRetrun.ToString();

        slots = new List<Slot>();

        int slotCnt = slotRoot.childCount;

        for(int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            slots.Add(slot);
        }

        if(SceneManager.GetActiveScene().name == "CityScene")
        {
            shop.onBuyBtn += BuyItem;
        }
    }

    private void Update()
    {
        goldAmount.text = pi.goldRetrun.ToString();
    }

    //상점에서 구매하는 경우
    void BuyItem(ItemProperty item)
    {
        //구매하려는 아이템과 같은 아이템이 인벤토리에 있는지 찾기
        var stockSlot = slots.Find(t =>
        {
            return t.item.name == item.name;
        });

        //구매하려는 아이템과 같은 아이템이 인벤토리에 있음
        if(stockSlot != null)
        {
            stockSlot.stock.text = item.stock++.ToString();
            pi.goldRetrun -= item.price;
            Debug.Log("잔 액 : " + pi.goldRetrun);
            return;
        }

        //빈칸 찾기
        var emptySlot = slots.Find(t =>
        {
            return t.item == null || t.item.name == string.Empty;
        });

        //인벤토리에 없는 아이템 구매
        if(emptySlot != null)
        {
            emptySlot.SetItem(item);
            pi.goldRetrun -= item.price;
            Debug.Log("잔 액 : " + pi.goldRetrun);
        }
    }

    //몬스터로부터 드랍하는 경우
    public void DropItem(ItemProperty item)
    {
        //드랍하는 아이템과 같은 아이템이 인벤토리에 있는지 찾기
        var stockSlot = slots.Find(t =>
        {
            return t.item.name == item.name;
        });
        //드랍하는 아이템과 같은 아이템이 인벤토리에 있음
        if (stockSlot != null)
        {
            stockSlot.stock.text = item.stock++.ToString();
            return;
        }
        //빈칸 찾기
        var emptySlot = slots.Find(t =>
        {
            return t.item == null || t.item.name == string.Empty;
        });
        //인벤토리에 없는 아이템 드랍
        if (emptySlot != null)
        {
            emptySlot.SetItem(item);
        }
    }

    public void OpenInven()
    {
        gameObject.SetActive(true);
    }

    public void CloseInven()
    {
        gameObject.SetActive(false);
    }
}
