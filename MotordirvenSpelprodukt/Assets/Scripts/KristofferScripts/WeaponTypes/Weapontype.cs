using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapontype : ScriptableObject
{
    [SerializeField] private Image _image;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _weaponRange;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _baseCost;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Animation _weaponAnimation;
    [SerializeField] private string _nameList;

    [SerializeField] public List<AttackSO> LightAnimationType;
    [SerializeField] public List<AttackSO> HeavyAnimationType;

    public Image GetImage(){return _image;}
    public Animation GetAnimation(){return _weaponAnimation;}
    public float GetDamage(){return _baseDamage;}
    public float GetRange() { return _weaponRange; }
    public float GetAttackSpeed(){return _attackSpeed;}
    public float GetBaseCost(){return _baseCost;}
    public GameObject GetPrefab() { return _prefab; }
    public string GetNameList() { return _nameList; }
}
