using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _gameObject;
    public Weapon SetWeapon(Weapon weapon)
    {
        Weapon oldweapon = null;
        if (weapon != null){ oldweapon = _weapon; }
        
        _weapon = weapon;
        return oldweapon;
    }
    public void CreatePrefab()
    {
        _gameObject = (GameObject)Instantiate(Resources.Load(_weapon.GetPath()));
        _gameObject.transform.parent = gameObject.transform;
    }
}
