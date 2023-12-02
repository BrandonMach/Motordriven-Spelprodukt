using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestaurangScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlayerMoney;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerMoney.text = GameManager.PlayerCoins.ToString();
    }


    public void BuyHealItem(FoodHeal food) 
    {
       if(food.Price< GameManager.PlayerCoins)
       {
            GameManager.PlayerCoins -= food.Price;
            TransferableScript.Instance.HealItems.Add(food);

       }



    }
}
