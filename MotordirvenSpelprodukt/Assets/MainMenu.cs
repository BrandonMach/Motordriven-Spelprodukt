using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _prevMenu;


    public void ClickPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void ClickOptions()
    {
        _optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickEsc()
    {
        gameObject.SetActive(false);
        _prevMenu.SetActive(true);  
    }

}
