using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuAbstract, IMenu
{
    public static bool GameIsPaused = false;
    GameManager _gameManager = GameManager.Instance;

    [SerializeField] GameObject _pauseMenuUI;

   

    private void Start()
    {
        //InitializeToHideList();
    }


    // Update is called once per frame
    void Update()
    {
        ClickESC();

    }



    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    // Freezes the game
        GameIsPaused = true;
        Debug.Log("Clicked");
    }

    public void ClickESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc is pressed, game is paused: " + GameIsPaused);

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
        base.ClickMenuOption3();
    }

    public void QuitGame()
    {
        if (GameManager.Instance._currentScen == GameManager.CurrentScen.CustomizationScene)
        {
            Resume();
            //Loading main menu
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else if (GameManager.Instance._currentScen == GameManager.CurrentScen.AreaScen)
        {
            Resume();
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (GameManager.Instance._currentScen == GameManager.CurrentScen.MainMenuScene)
        {
            Application.Quit();
        }
        
    }
}
