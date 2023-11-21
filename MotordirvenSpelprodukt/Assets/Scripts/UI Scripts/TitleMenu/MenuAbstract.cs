using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuAbstract : MonoBehaviour, IMenu
{
    [SerializeField] protected GameObject _prevMenu;
    [SerializeField] protected GameObject _menuOption1;
    [SerializeField] protected GameObject _menuOption2;
    [SerializeField] protected GameObject _menuOption3;


    public void ClickESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            _prevMenu.SetActive(true);
        }
    }

    public virtual void ClickBack()
    {
        gameObject.SetActive(false);
        _prevMenu.SetActive(true);
    }

    public virtual void ClickMenuOption1()
    {
        _menuOption1.SetActive(true);
        gameObject.SetActive(false);
    }

    public virtual void ClickMenuOption2()
    {
        _menuOption2.SetActive(true);
        gameObject.SetActive(false);
    }

    public virtual void ClickMenuOption3()
    {
        _menuOption3.SetActive(true);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
       ClickESC();
    }
}
