using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMenu : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickEnter();
    }

    void ClickEnter()
    {
        

        if (Input.GetKeyDown(KeyCode.Return) && !_mainMenu.activeSelf)
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
}
