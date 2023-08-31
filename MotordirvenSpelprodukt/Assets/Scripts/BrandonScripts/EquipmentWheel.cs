using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentWheel : MonoBehaviour
{
    public Transform center;
    public Transform selectObject;
    public GameObject unequipSprite;

    public GameObject equipmentMenuRoot;
    bool isEquipmentMenuActive;

    public TextMeshProUGUI selectionText;

    public GameObject[] equipedItems;
    void Start()
    {
        isEquipmentMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            isEquipmentMenuActive = !isEquipmentMenuActive;

            if (isEquipmentMenuActive)
            {
                equipmentMenuRoot.SetActive(true);
            }
            else
            {
                equipmentMenuRoot.SetActive(false);
            }
        }


        if (isEquipmentMenuActive)
        {
            //Calculate angle

            Vector2 delta = center.position - Input.mousePosition; //Eller Handkontroll, kolla upp så att et funkar
            float angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg;
            angle += 180;

            Debug.LogError(angle);


            if (angle >= 46 && angle < 135)
            {
                selectObject.eulerAngles = new Vector3(0, 0, 270);
                selectionText.text = "Right Hand";
                CheckIfEquiped(0);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(0);
                }
            }
            else if (angle >= 136 && angle < 226)
            {
                selectObject.eulerAngles = new Vector3(0, 0, 180);
                selectionText.text = "Armour";
                CheckIfEquiped(1);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(1);
                }
            }
            else if (angle >= 227 && angle < 315)
            {
                selectObject.eulerAngles = new Vector3(0, 0, 90);
                selectionText.text = "Left Hand";
                CheckIfEquiped(2);
                if (Input.GetMouseButtonDown(0))
                {
                    ToggelEquip(2);
                }
            }
            else
            {
                selectObject.eulerAngles = new Vector3(0, 0, 0);
                selectionText.text = "Headgear";
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

        if (equipedItems[itemindex].GetComponent<Equipment>().isEquiped)
        {
            equipedItems[itemindex].SetActive(false);
        }
        else if (!equipedItems[itemindex].GetComponent<Equipment>().isEquiped)
        {
            equipedItems[itemindex].SetActive(true);
        }
        equipedItems[itemindex].GetComponent<Equipment>().isEquiped = !equipedItems[itemindex].GetComponent<Equipment>().isEquiped;
    }

    void CheckIfEquiped(int itemindex)
    {
        if (equipedItems[itemindex].GetComponent<Equipment>().isEquiped)
        {
            unequipSprite.SetActive(true);
        }
        else if (!equipedItems[itemindex].GetComponent<Equipment>().isEquiped)
        {
            unequipSprite.SetActive(false);
        }


    }
}
