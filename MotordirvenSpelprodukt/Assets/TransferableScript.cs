using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferableScript : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    //[SerializeField] private FMODController _FMODcontroller;

    [SerializeField] public List<FoodHeal> HealItems;

    #region Singleton


    private static TransferableScript _instance;
    public static TransferableScript Instance { get => _instance; set => _instance = value; }

    #endregion

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
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
