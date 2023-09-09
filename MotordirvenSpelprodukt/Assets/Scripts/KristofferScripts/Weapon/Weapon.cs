using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon",menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string _weaponName;
    [SerializeField] private Weapontype _weaponType;
    [SerializeField] private GameObject _weaponPrefab; 
    [SerializeField] private int _weaponLevel=1;
    [Header("Values to be observed")]
    [SerializeField] private float _weaponDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _range;
    private float _damageOffset=1, _rangeOffset=1, _speedOffset=1;   
    public void UpgradeWeapon()
    {
        _weaponLevel++;
        UpdateWeaponDamage();
    }
    private void UpdateWeaponDamage() { _weaponDamage = _weaponType.GetDamage()*_damageOffset * _weaponLevel; }   
    private void UpdateRange() { _range = _weaponType.GetRange() * _rangeOffset; }
    private void UpdateSpeed() { _attackSpeed = _weaponType.GetAttackSpeed() * _speedOffset; }
    public void SetUpWeapon(Weapontype type, int level, float damageOffset, float rangeOffset, float speedOffset)
    {
        _weaponLevel = level;
        _weaponType = type;
        this._damageOffset = damageOffset;
        this._rangeOffset = rangeOffset;
        this._speedOffset = speedOffset;
        UpdateWeaponDamage();
        UpdateRange();
        UpdateSpeed();
    }

    public float GetDamage() { return _weaponDamage; }
    public float GetRange() { return _range; }
    public float GetSpeed() { return _attackSpeed; }
    public string GetName(){return _weaponName;}
    public Animation GetAnimation() { return _weaponType.GetAnimation(); }
    public GameObject GetPrefab() { return _weaponPrefab; }
}
