using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSettings : MenuAbstract, IMenu
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        base.Update();
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
        _prevMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
