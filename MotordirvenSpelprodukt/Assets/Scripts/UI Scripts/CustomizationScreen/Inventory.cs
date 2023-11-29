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
        //if (_instance != null)
        //{
        //    Debug.LogWarning("More than one instance of Inventory found");
        //    return;
        //}
        //_instance = this;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    private List<Item> _inventoryList = new List<Item>();
    private int _capacity = 25;

    public List<Item> InventoryList { get => _inventoryList; set => _inventoryList = value; }
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

        //else if (item is Potion potion)
        //{
        //    // Example
        //}

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        if (item is Weapon weapon)
        {
            _inventoryList.Remove(weapon);
        }

        //else if (item is Potion potion)
        //{
        //    // Example
        //}

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
