using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private Weapontype weaponType;
    [SerializeField] private int weaponLevel=1;
    private int weaponDamage;

    public int GetDamage(){return weaponDamage;}
    public float GetRange() { return weaponType.GetRange(); }
    public void UpgradeWeapon()
    {
        weaponLevel++;
        UpdateWeaponDamage();
    }
    private void UpdateWeaponDamage(){ weaponDamage = (int)weaponType.GetDamage() * weaponLevel; }   
    public Animation GetAnimation(){ return weaponType.GetAnimation();}
    public void SetUpWeapon(Weapontype type, int level)
    {
        weaponLevel = level;
        weaponType = type;
    }
    public string GetName(){return weaponName;}
}
