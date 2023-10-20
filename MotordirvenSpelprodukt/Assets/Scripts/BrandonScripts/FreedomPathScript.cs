using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FreedomPathScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currencyText;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currencyText.text = "Currency: " + GameManager._playerCoins;
        Debug.LogError(GameManager._playerCoins);
    }
}
