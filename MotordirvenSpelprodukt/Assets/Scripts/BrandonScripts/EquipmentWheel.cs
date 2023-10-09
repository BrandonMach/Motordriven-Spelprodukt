using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentWheel : MonoBehaviour
{
    public Transform Center;
    public Transform SelectObject;
    public GameObject UnequipSprite;

    public GameObject EquipmentMenuRoot;
    bool _isEquipmentMenuActive;

    public TextMeshProUGUI SelectionText;

    public GameObject[] EquipedItems;
    void Start()
    {
        _isEquipmentMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            _isEquipmentMenuActive = !_isEquipmentMenuActive;

            if (_isEquipmentMenuActive)
            {
                EquipmentMenuRoot.SetActive(true);
            }
            else
            {
                EquipmentMenuRoot.SetActive(false);
            }
        }


        if (_isEquipmentMenuActive)
        {
            //Calculate angle

            Vector2 delta = Center.position - Input.mousePosition; //Eller Handkontroll, kolla upp så att et funkar
            float angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg;
            angle += 180;

            //Debug.LogError(angle);














            if (angle >= 46 && angle < 135)
            {
                SelectObject.eulerAngles = new Vector3(0, 0, 270);
                SelectionText.text = "Right Hand";
                CheckIfEquiped(0);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(0);
                }
            }
            else if (angle >= 136 && angle < 226)
            {
                SelectObject.eulerAngles = new Vector3(0, 0, 180);
                SelectionText.text = "Armour";
                CheckIfEquiped(1);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(1);
                }
            }
            else if (angle >= 227 && angle < 315)
            {
                SelectObject.eulerAngles = new Vector3(0, 0, 90);
                SelectionText.text = "Left Hand";
                CheckIfEquiped(2);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(2);
                }
            }
            else
            {
                SelectObject.eulerAngles = new Vector3(0, 0, 0);
                SelectionText.text = "Headgear";
                CheckIfEquiped(3);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(3);
                }
            }

        }
    }

    void ToggelEquip(int itemindex)
    {

        if (EquipedItems[itemindex].GetComponent<Equipment>().IsEquiped)
        {
            EquipedItems[itemindex].SetActive(false);
        }
        else if (!EquipedItems[itemindex].GetComponent<Equipment>().IsEquiped)
        {
            EquipedItems[itemindex].SetActive(true);
        }
        EquipedItems[itemindex].GetComponent<Equipment>().IsEquiped = !EquipedItems[itemindex].GetComponent<Equipment>().IsEquiped;
    }

    void CheckIfEquiped(int itemindex)
    {
        if (EquipedItems[itemindex].GetComponent<Equipment>().IsEquiped)
        {
            UnequipSprite.SetActive(true);
        }
        else if (!EquipedItems[itemindex].GetComponent<Equipment>().IsEquiped)
        {
            UnequipSprite.SetActive(false);
        }


    }
}
