using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{

    #region Singleton


    private static PlayerWeaponHolder _instance;
    public static PlayerWeaponHolder Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;

        Debug.developerConsoleVisible = true;

    }



    #endregion


    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private GameObject weaponHand;
    [SerializeField] private GameObject weaponObject;
    public Weapon CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetWeapon(Weapon _weapon)
    {
        CurrentWeapon = _weapon;
        ReplaceWeapon();

    }
    public void ReplaceWeapon()
    {
        if (CurrentWeapon != null)
        {
            Debug.Log(CurrentWeapon.GetPath());

            GameObject weaponnew = (GameObject)Instantiate(Resources.Load("WeaponResources/" + CurrentWeapon.GetPath()));
            weaponnew.transform.parent = weaponHand.transform;
            weaponnew.transform.position = weaponObject.transform.position;
            weaponnew.transform.rotation = weaponObject.transform.rotation;
            weaponnew.transform.localScale = weaponObject.transform.localScale;
            weaponObject.transform.GetChild(0).parent = weaponnew.transform;
            WeaponVisualEffects wve = gameObject.GetComponent<WeaponVisualEffects>();


            //wve.SetNewTrail(weaponnew.transform.GetChild(0));
            
            Destroy(weaponObject);
            weaponObject = weaponnew;

        }

    }
}
