using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuAbstract, IMenu
{
    public static bool GameIsPaused = false;
    GameManager _gameManager = GameManager.Instance;

    [SerializeField] GameObject _pauseMenuUI;
    [SerializeField] GameObject _bloodSplatterUI;

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
        _bloodSplatterUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        _bloodSplatterUI.SetActive(false);
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

    public void QuitGame()
    {
        if (GameManager.Instance._currentScen == GameManager.CurrentScen.CustomizationScene)
        {
            Resume();
            //Loading main menu
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (GameManager.Instance._currentScen == GameManager.CurrentScen.ArenaScen)
        {
            Resume();
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (GameManager.Instance._currentScen == GameManager.CurrentScen.MainMenuScene)
        {
            Application.Quit();
        }
        
    }
}
