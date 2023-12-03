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
        if (GameManager.Instance != null)
        {
            coinsText.text = GameManager.PlayerCoins.ToString();
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
            damageText.text = PlayerWeaponHolder.Instance.CurrentWeapon.GetDamage().ToString();
        }
    }
}
