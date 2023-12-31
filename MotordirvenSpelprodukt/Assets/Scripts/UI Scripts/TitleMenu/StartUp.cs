using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    [SerializeField] private GameObject enterMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject videoMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject attackTreeMenu;

    // Start is called before the first frame update
    void Start()
    {
        //mainMenu = GameObject.FindWithTag("MainMenu");
        enterMenu.SetActive(true);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        audioMenu.SetActive(false);
        videoMenu.SetActive(false);
        controlsMenu.SetActive(false);
        attackTreeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
