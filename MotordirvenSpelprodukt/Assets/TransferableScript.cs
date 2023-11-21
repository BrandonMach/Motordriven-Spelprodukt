using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferableScript : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
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
}
