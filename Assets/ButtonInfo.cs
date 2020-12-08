using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text Price;
    public GameObject ShopManager;


    void Update()
    {
        Price.text = "$" + ShopManager.GetComponent<ShopController>().shopItems[2, ItemID].ToString();
    }
}
