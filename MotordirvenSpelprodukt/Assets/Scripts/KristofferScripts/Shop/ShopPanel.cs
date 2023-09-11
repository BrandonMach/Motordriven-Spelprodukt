using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopPanel : MonoBehaviour
{
    [SerializeField] private WeapontypeList _weaponTypeList;
    [SerializeField] private List<GenerateWeaponPanel> panelList;
    private List<Weapontype> _weaponList;
    private void Awake()
    {
        _weaponList = new List<Weapontype>();
    }
    private void Start()
    {
        _weaponList = _weaponTypeList.GetTypeList();
        foreach (GenerateWeaponPanel p in panelList)
        {
            int r = Random.Range(0, panelList.Count);
            p.GenerateWeapon(_weaponList[r],r);           
        }
    }
}
