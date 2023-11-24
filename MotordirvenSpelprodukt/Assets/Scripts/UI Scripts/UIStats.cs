using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStats : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI coinsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinsText();
        UpdateDamageText();
    }

    private void UpdateCoinsText()
    {
        if (GameLoopManager.Instance != null)
        {
            coinsText.text = GameLoopManager.PlayerCoins.ToString();
        }
        else
        {
            coinsText.text = "0";
        }
    }

    private void UpdateDamageText()
    {
        if (Player.Instance != null)
        {
            damageText.text = Player.Instance.CurrentWeapon.GetDamage().ToString();
        }
    }
}