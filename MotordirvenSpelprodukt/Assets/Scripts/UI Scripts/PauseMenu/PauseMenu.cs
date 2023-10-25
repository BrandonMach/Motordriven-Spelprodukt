using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MenuAbstract, IMenu
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject _pauseMenuUI;

    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //ClickESC(); 


    }


    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }

    void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    // Freezes the game
        GameIsPaused = true;
    }

    public void ClickESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public override void ClickMenuOption1()
    {
        Resume();
    }

    public override void ClickMenuOption2()
    {
        base.ClickMenuOption2();
    }

    public override void ClickMenuOption3()
    {
        // TODO : Load TitleScreenScene
    }
}
