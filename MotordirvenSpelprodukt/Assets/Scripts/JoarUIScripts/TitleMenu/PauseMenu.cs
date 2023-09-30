using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MenuAbstract, IMenu
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        ClickESC(); 
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
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

    public void ClickMenuOption1()
    {
        Resume();
    }

    public void ClickMenuOption2()
    {
        base.ClickMenuOption2();
    }

    public void ClickMenuOption3()
    {
        // TODO : Load TitleScreenScene
    }
}
