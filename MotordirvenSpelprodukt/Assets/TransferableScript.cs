using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferableScript : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    //[SerializeField] private FMODController _FMODcontroller;
    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
    }
    public Weapon GetWeapon()
    {
        return _weapon;
    }
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }
    //public FMODController GetFMODAM()
    //{
    //    return _FMODcontroller;
    //}
    //public void SetFMODAM(FMODController FMODAM)
    //{
    //    _FMODcontroller = FMODAM;
    //}
}
