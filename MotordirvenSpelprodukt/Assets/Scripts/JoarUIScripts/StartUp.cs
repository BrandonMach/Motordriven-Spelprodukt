using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    [SerializeField] private GameObject enterMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        //mainMenu = GameObject.FindWithTag("MainMenu");
        enterMenu.SetActive(true);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
