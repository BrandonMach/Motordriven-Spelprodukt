using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ShopSystem : MonoBehaviour
{
    [Header("Player Weapon")]
    [SerializeField] private Weapon _weapon;
    [Header("Shop Menu")]
    //[SerializeField] private float _currency;
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
    [Header("Prefab Test")]
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

    private void Update()
    {
        //_currency = GameManager.PlayerCoins;
        Debug.Log(GameManager.PlayerCoins + " Player money");
    }
    public void MakePurshase(GenerateWeapon g)
    {


        _weapon = TryPurshase(g);

        UpdateUpgradeView();

        Inventory.Instance.Add(_weapon);
    }
    public Weapon TryPurshase(GenerateWeapon g)
    {
        if(!g.Bought())
        {
            if (g.Price()<= GameManager.PlayerCoins)
            {
                GameManager.PlayerCoins -= g.Price();
                UpdateText();
                                   
                return g.Purshase();
            }               
        }
        return null;
    }
    private void UpdateText()
    {
        _currencyText.text = GameManager.PlayerCoins.ToString();
    }
    private void UpdateUpgradeView()
    {
        if(_weapon.GetImage()!=null)
        {
            _weaponImage.sprite = _weapon.GetImage();
            _weapon.GeneratePrefab();
        }
        _weaponName.text = _weapon.GetName();
        _weaponLevel.text = _weapon.GetLevel().ToString();
        _weaponDamage.text = _weapon.GetDamage().ToString();
        _weaponUpgradeCost.text = ((_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel()) / 2).ToString();
        
    }
    public void UpgradeWeapon()
    {
        if(GameManager.PlayerCoins >= (int)((_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel()) / 2))
        {
            GameManager.PlayerCoins -= (int)((_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel()) / 2);
            _weapon.UpgradeWeapon();
            UpdateUpgradeView();
            UpdateText();
        }      
    }
    public void RerollShop()
    {
        if(GameManager.PlayerCoins >= rerollCounter*rerollBaseline)
        {
            GetComponent<ShopPanel>().Generate();
            GameManager.PlayerCoins -= rerollCounter*rerollBaseline;
            rerollCounter++;
            _rerollButtonText.text = (rerollCounter * rerollBaseline).ToString();
            UpdateText();
        }
        
    }

    public void PlayBuySound()
    {
        //FMODSFXController.Instance.PlayCoinDrop();
    }
}
