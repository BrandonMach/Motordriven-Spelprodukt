using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.onItemChangedCallback += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
