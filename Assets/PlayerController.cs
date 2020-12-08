using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class PlayerController : MonoBehaviour
{
    private int curExp = 0;
    //private int[] maxExp = { 2, 2, 6, 10, 20, 36, 56, 80 };
    private int[] maxExp = { 4, 6, 8, 10, 20, 36, 56, 80 };
    private int charLevel = 1;
    private int baseExp = 4;
    private int waveExp = 2;
    private bool isNextWave = false;
    


    public Image mask;
    public Image fill;
    public Color color;
    public ShopController shop;
    public Text moneyTxt;



    List<Pokemon> pokemonSquad = new List<Pokemon>();
    

    // Start is called before the first frame update
    void Start()
    {
        

        //PhotonNetwork.Instantiate("Prefabs/Pokemon", new Vector3(Random.Range(-30, 30), -20, 0), Quaternion.identity, 0);
       
    }


    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PhotonNetwork.InRoom);
        GetCurrentFill();

    }

    void GetCurrentFill()
    {
        
        float currentOffset = curExp;
        float maximumOffset = maxExp[charLevel - 1];
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
        fill.color = color;

        if (isNextWave == true)
        {
            curExp += waveExp;
            isNextWave = false;
        }
            

       
        Debug.Log(charLevel);
        Debug.Log(curExp);
    }

    public void BuyExp()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (shop.money >= baseExp)
        {
            shop.money -= baseExp;
            if (curExp + baseExp < maxExp[charLevel - 1])
                curExp += baseExp;
            else
            {
                curExp = curExp + baseExp - maxExp[charLevel - 1];
                charLevel++;
            }    
            
            moneyTxt.text = "Money: " + shop.money.ToString();
        }
    }

}
