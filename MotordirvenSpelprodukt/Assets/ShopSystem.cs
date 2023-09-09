using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private int _currency;
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private Weapon weapon;
    private void Awake()
    {
        UpdateText();
    }
    public void MakePurshase(GenerateWeapon g)
    {
        weapon = TryPurshase(g);
    }
    public Weapon TryPurshase(GenerateWeapon g)
    {
        if(!g.Bought())
        {
            if (g.Price()<=_currency)
            {
                _currency-=g.Price();
                UpdateText();
                return g.Purshase();
            }               
        }
        return null;
    }
    private void UpdateText()
    {
        _currencyText.text = _currency.ToString();
    }
}
