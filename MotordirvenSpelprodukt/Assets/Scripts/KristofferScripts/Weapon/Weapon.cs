using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon",menuName ="Weapon")]
public class Weapon : Item
{
    [SerializeField] private string _weaponName;
    [SerializeField] private Weapontype _weaponType;
    [SerializeField] private int _weaponLevel=1;
    [SerializeField] private string _prefabPath;
    [SerializeField] private Sprite _image;
    [Header("Values to be observed")]
    [SerializeField] private float _weaponDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _range;
    private float _damageOffset=1, _rangeOffset=1, _speedOffset=1;   
    [SerializeField] private string _attackTreeDescriptionTextfile;
    // L�gg till en bleed damage property p� vapnet?
    public void UpgradeWeapon()
    {
        _weaponLevel++;
        UpdateWeaponDamage();
    }
    private void UpdateWeaponDamage() { _weaponDamage = _weaponType.GetDamage()*_damageOffset * _weaponLevel; }   
    private void UpdateRange() { _range = 2;/*_weaponType.GetRange() * _rangeOffset;*/ }
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
    public void GeneratePrefab()
    {
        Instantiate(Resources.Load("WeaponResources/" + _prefabPath)) ;       
    }
    public void SetName(string weaponName) { _weaponName = weaponName; }
    public void SetImage(Sprite weaponImage) { _image = weaponImage; }
    public void SetPrefabPath(string str) { _prefabPath = str; }
    public float GetDamage() { return _weaponDamage; }
    public float GetRange() { return _range; }
    public float GetSpeed() { return _attackSpeed; }
    public string GetName(){return _weaponName;}
    public int GetLevel() { return _weaponLevel; }
    public Sprite GetImage() { return _image; }
    public string GetPath() { return _prefabPath; }
    public string GetAttackTreeDescriptionPath() { return _attackTreeDescriptionTextfile; }
    public Weapontype GetWeaponType() { return _weaponType; }   
}
