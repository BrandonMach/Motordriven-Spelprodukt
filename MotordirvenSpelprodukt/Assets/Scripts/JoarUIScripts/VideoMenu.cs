using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoMenu : MenuAbstract, IMenu
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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
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
