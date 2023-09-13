using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuAbstract, IMenu
{
    //[SerializeField] private GameObject _prevMenu;
    //[SerializeField] private GameObject _menuOption1;
    //[SerializeField] private GameObject _menuOption2;
    //[SerializeField] private GameObject _menuOption3;

    public void Update()
    {
        base.Update();
    }

    public void ClickNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void ClickESC()
    {
        base.ClickESC();
    }

    public void ClickMenuOption1()
    {
        base.ClickMenuOption1();
    }

    public void ClickMenuOption2()
    {
        base.ClickMenuOption2();
    }

    public void ClickMenuOption3()
    {
        base.ClickMenuOption3();
    }
}
