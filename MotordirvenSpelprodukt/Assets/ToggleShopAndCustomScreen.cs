using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShopAndCustomScreen : MonoBehaviour
{
    public GameObject Shop;
    public GameObject CustomScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Shop.SetActive(true);
            CustomScreen.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Shop.SetActive(false);
            CustomScreen.SetActive(true);
        }
    }
}
