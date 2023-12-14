using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopPanel : MonoBehaviour
{
    [SerializeField] private WeapontypeList _weaponTypeList;
    [SerializeField] private List<GenerateWeapon> panelList;
    private List<Weapontype> _weaponList;
   
    private void Awake()
    {
        _weaponList = new List<Weapontype>();
    }
    private void Start()
    {
        Generate();
    }
    public void Generate()
    {
        _weaponList = _weaponTypeList.GetTypeList();
        foreach (GenerateWeapon p in panelList)
        {
            int r = Random.Range(0, _weaponList.Count);
            int r2 = Random.Range(1, 4);
            p.GenerateWeaponPanel(_weaponList[r], r2);
        }
    }
}
