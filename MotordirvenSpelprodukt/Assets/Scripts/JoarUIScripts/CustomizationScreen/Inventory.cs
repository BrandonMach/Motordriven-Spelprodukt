using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    private static Inventory _instance;
    public static Inventory Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    // If we decide to have other items in inventory than weapons, create a baseclass Item that weapon and other items inherit
    private List<Weapon> _inventoryList = new List<Weapon>();
    private int _capacity = 25;

    public List<Weapon> InventoryList { get => _inventoryList; set => _inventoryList = value; }
    public int Capacity { get => _capacity; set => _capacity = value; }

    public bool Add(Item item)
    {
        if (_inventoryList.Count >= _capacity)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        if (item is Weapon weapon)
        {
            _inventoryList.Add(weapon);
        }
        else if (item is Armour armour)
        {
            _inventoryList.Add(armour);
        }

        

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void RemoveWeapon(Weapon weapon)
    {
        _inventoryList.Remove(weapon);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
