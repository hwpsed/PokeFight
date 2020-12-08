using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ShopController : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    public float money;
    public Text moneyTxt;
    //public GameObject moneyTXT;

    // Start is called before the first frame update
    void Start()
    {
        moneyTxt.text = "Money: " + money.ToString();

        //Item's ID      
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price     
        shopItems[2, 1] = 1;
        shopItems[2, 2] = 3;
        shopItems[2, 3] = 5;
        shopItems[2, 4] = 8;

        
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if(money >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            money -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            moneyTxt.text = "Money: " + money.ToString();
        }
    }
}
