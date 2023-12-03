using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image _icon;
    Item _item;

    public Image Icon { get => _icon; set => _icon = value; }
    private TooltipTrigger trigger;
    private void Awake()
    {
        trigger = GetComponent<TooltipTrigger>();
    }
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
            TooltipSetup(weapon);
        }
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }
    private void TooltipSetup(Weapon weapon)
    {
        trigger.header = weapon.GetName();       
        trigger.content = "Level: " + weapon.GetLevel() + "\n" + "Damage:" + weapon.GetDamage();
    }
    public void OnPress()
    {
        if (_item as Weapon && _item != null /*&& Player.Instance!=null*/)
        {
            Weapon weapon = (Weapon)_item;
            PlayerWeaponHolder.Instance.SetWeapon(weapon);
            SetTransferWeapon();
        }            
    }
    private void SetTransferWeapon()
    {
        GameObject.Find("Transferables").GetComponent<TransferableScript>().SetWeapon((Weapon)_item);
    }
}
