using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMenu : MenuAbstract, IMenu
{

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
        
        if (Input.GetKeyDown(KeyCode.Return) && !_menuOption1.activeSelf)
        {
            _menuOption1.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

    public void ClickESC()
    {
        // Do nothing
    }

    public void PressToStart()
    {
        _menuOption1.SetActive(true);
        gameObject.SetActive(false);

        Debug.Log("Press to Start was hit");
    }

    public override void ClickMenuOption1()
    {
        _menuOption1.SetActive(true);
        gameObject.SetActive(false);

        Debug.Log("ClickMenuOption1 was hit");
    }

    public override void ClickMenuOption2()
    {
        throw new System.NotImplementedException();
    }

    public override void ClickMenuOption3()
    {
        throw new System.NotImplementedException();
    }
}
