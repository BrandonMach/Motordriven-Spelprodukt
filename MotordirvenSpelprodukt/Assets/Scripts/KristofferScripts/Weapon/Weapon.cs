using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon",menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _weaponName;
    [SerializeField] private Weapontype _weaponType;
    [SerializeField] private int _weaponLevel=1;
    [SerializeField] private string _prefabPath;
    private Sprite spr;
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
    public void SetUpWeapon(Weapontype type, int level, float damageOffset=1, float rangeOffset=1, float speedOffset=1)
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
    public void SetImage(Sprite spr)
    {
        _image = spr;     
    }  
    public void SetPrefabPath(string path) { _prefabPath = path; }
    public void SetName(string weaponName) { _weaponName = weaponName; }
    public float GetDamage() { return _weaponDamage; }
    public float GetRange() { return _range; }
    public float GetSpeed() { return _attackSpeed; }
    public string GetName(){return _weaponName;}
    public Sprite GetImage(){return _image;}
    public string GetPath(){return _prefabPath;}
    public int GetLevel() { return _weaponLevel; }
    public Weapontype GetWeaponType() { return _weaponType; }   
}
