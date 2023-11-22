using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MenuAbstract, IMenu
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

    public override void ClickBack()
    {
        base.ClickBack();
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
