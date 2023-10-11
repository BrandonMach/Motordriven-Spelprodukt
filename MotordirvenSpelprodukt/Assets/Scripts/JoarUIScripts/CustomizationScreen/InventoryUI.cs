using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]Transform _itemsParent;
    Inventory _inventory;
    InventorySlot[] _slots;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.onItemChangedCallback += UpdateUI;

        _slots = _itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.InventoryList.Count)
            {
                _slots[i].AddItem(_inventory.InventoryList[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
