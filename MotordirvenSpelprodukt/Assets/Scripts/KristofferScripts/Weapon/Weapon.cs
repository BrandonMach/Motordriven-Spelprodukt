using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon",menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private Weapontype weaponType;
    [SerializeField] private int weaponLevel=1;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float range;
    private float damageOffset=1, rangeOffset=1, speedOffset=1;   
    public void UpgradeWeapon()
    {
        weaponLevel++;
        UpdateWeaponDamage();
    }
    private void UpdateWeaponDamage() { weaponDamage = weaponType.GetDamage()*damageOffset * weaponLevel; }   
    private void UpdateRange() { range = weaponType.GetRange() * rangeOffset; }
    private void UpdateSpeed() { attackSpeed = weaponType.GetAttackSpeed() * speedOffset; }
    public void SetUpWeapon(Weapontype type, int level)
    {
        weaponLevel = level;
        weaponType = type;
    }
    public void SetOffSet(float damageOffset, float rangeOffset, float speedOffset)
    {
        this.damageOffset = damageOffset;
        this.rangeOffset = rangeOffset;
        this.speedOffset = speedOffset;
        UpdateWeaponDamage();
        UpdateRange();
        UpdateSpeed();
    }
    public float GetDamage() { return weaponDamage; }
    public float GetRange() { return range; }
    public float GetSpeed() { return attackSpeed; }
    public string GetName(){return weaponName;}
    public Animation GetAnimation() { return weaponType.GetAnimation(); }
}
