using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MenuAbstract, IMenu
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void ClickBack()
    {
        base.ClickBack();
    }
    public void ClickESC()
    {
        base.ClickESC();
    }

    public override void ClickMenuOption1()
    {
        base.ClickMenuOption1();
    }

    public override void ClickMenuOption2()
    {
        base.ClickMenuOption2();
    }

    public override void ClickMenuOption3()
    {
        base.ClickMenuOption3();
    }
}
