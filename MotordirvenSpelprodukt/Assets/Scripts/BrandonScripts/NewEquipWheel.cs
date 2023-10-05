using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewEquipWheel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _potionAmountText;
    [SerializeField] private int _potionAmount = 10;
    [SerializeField] private GameObject _PieChart;
    public bool _weaponIsEquiped;

    void Start()
    {
        _PieChart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //hold button to open 3 pie chart
        if (Input.GetKey(KeyCode.E))
        {
            _PieChart.SetActive(true);
            if (Input.GetKeyDown(KeyCode.I))
            {
                UsePotion();
            }
        }
        else
        {
            _PieChart.SetActive(false);
        }
    }




    public void EquipWeapon()
    {
        _weaponIsEquiped = !_weaponIsEquiped;
    }

    public void UnequipWeapon()
    {
        _weaponIsEquiped = !_weaponIsEquiped;
    }

    public void Taunt()
    {
        //Taunt
    }

    public void UsePotion()
    {
        if(_potionAmount != 0)
        {
            _potionAmount--;
            _potionAmountText.text = (_potionAmount + "x");
        }
       
    }


}
