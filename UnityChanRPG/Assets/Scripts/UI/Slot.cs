﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //[HideInInspector]
    public ItemProperty item;
    public Image image;
    public Text name;
    public Text detaile;
    public Text price;
    public Text stock;

    public void SetItem(ItemProperty item)
    {
        this.item = item;
        if(item == null)
        {
            image.enabled = false;
            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = item.name;
            image.sprite = item.sprite;
            name.text = item.name;
            detaile.text = item.detaile;
            price.text = item.price.ToString();
            stock.text = item.stock.ToString();
        }
    }
}