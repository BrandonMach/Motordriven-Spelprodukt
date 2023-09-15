using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GenerateWeaponPanel : MonoBehaviour
{
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] private TextMeshProUGUI _weaponDamageText;
    [SerializeField] private TextMeshProUGUI _weaponSpeedText;
    [SerializeField] private TextMeshProUGUI _weaponRangeText;
    private Weapon weapon;
    
    public void GenerateWeapon(Weapontype type, int level)
    {       
        weapon.SetUpWeapon(type,level, 
            Random.Range(0.8f, 1.2f), 
            Random.Range(0.8f, 1.2f), 
            Random.Range(0.8f, 1.2f));
        weapon.SetName(GenerateName());
        UpdatePanel();
    }

    private string GenerateName()
    {
        return "deb";
    }
    private void UpdatePanel()
    {
        _weaponImage = weapon.GetImage();
        _weaponNameText.text = weapon.GetName();
        _weaponDamageText.text = weapon.GetDamage().ToString() + " Dmg";
        _weaponSpeedText.text = weapon.GetSpeed().ToString() + " Spd";
        _weaponRangeText.text = weapon.GetRange().ToString() + " Range";
    }
}
