using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [Header("Player Weapon")]
    [SerializeField] private Weapon _weapon;
    [Header("Shop Menu")]
    [SerializeField] private int _currency;
    [SerializeField] private TextMeshProUGUI _currencyText;
    [Header("Upgrade Menu")]
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _weaponLevel;
    [SerializeField] private TextMeshProUGUI _weaponDamage;
    [SerializeField] private TextMeshProUGUI _weaponUpgradeCost;
    [Header("Reroll Setup")]
    [SerializeField] private TextMeshProUGUI _rerollButtonText;
    private int rerollCounter=1;
    private int rerollBaseline = 10;
    [Header("Test Resources")]
    private GameObject _gPrefab;

    private void Awake()
    {
        UpdateText();
        Weapontype sword = new Weapontype();
        _weapon = new Weapon();
        _weapon.SetUpWeapon(sword,0,0,0,0);
        UpdateUpgradeView();
        _rerollButtonText.text = (rerollCounter * rerollBaseline).ToString();
    }
    public void MakePurshase(GenerateWeapon g)
    {
        _weapon = TryPurshase(g);
        UpdateUpgradeView();
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
    private void UpdateUpgradeView()
    {
        if(_weapon.GetImage()!=null)
        {
            _weaponImage.enabled = true;
            _weaponImage.sprite = _weapon.GetImage();
            _weaponName.text = _weapon.GetName();
            _weaponLevel.text = _weapon.GetLevel().ToString();
            _weaponDamage.text = _weapon.GetDamage().ToString();
            _weaponUpgradeCost.text = ((_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel()) / 2).ToString();
            _gPrefab = Instantiate(Resources.Load("WeaponResources/" + _weapon.GetPath())) as GameObject;
        }
        else
        {          
            _weaponImage.enabled = false;
            _weaponName.text = "";
            _weaponLevel.text = "";
            _weaponDamage.text = "";
            _weaponUpgradeCost.text = "Slot Empty";
            _gPrefab = null;
        }
        
    }
    public void UpgradeWeapon()
    {
        if(_currency>= (int)((_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel()) / 2) && _weaponImage!=null)
        {
            _currency -= (int)((_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel()) / 2);
            _weapon.UpgradeWeapon();
            UpdateUpgradeView();
            UpdateText();
        }      
    }
    public void RerollShop()
    {
        if(_currency>=rerollCounter*rerollBaseline)
        {
            GetComponent<ShopPanel>().Generate();
            _currency -= rerollCounter*rerollBaseline;
            rerollCounter++;
            _rerollButtonText.text = (rerollCounter * rerollBaseline).ToString();
            UpdateText();
        }
        
    }
}
