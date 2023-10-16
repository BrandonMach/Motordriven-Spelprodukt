using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image _icon;
    Item _item;

    public Image Icon { get => _icon; set => _icon = value; }

    public void AddItem(Item newItem)
    {
        if (newItem is Weapon weapon)
        {
            _item = weapon; // Might not work

            if (weapon.GetImage() != null)
            {
                _icon.sprite = weapon.GetImage();
                _icon.enabled = true;
            }
        }
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }
}
