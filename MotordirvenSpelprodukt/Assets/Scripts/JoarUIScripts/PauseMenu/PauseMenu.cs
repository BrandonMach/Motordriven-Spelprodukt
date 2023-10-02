using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MenuAbstract, IMenu
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject _pauseMenuUI;
    //[SerializeField] GameInput _gameInput;

    private void Start()
    {
        
        //_gameInput.OnPauseButtonPressed += GameInput_OnPauseButtonPressed;
    }

    //private void GameInput_OnPauseButtonPressed(object sender, System.EventArgs e)
    //{
    //    Debug.Log("OnPauseButtonPressed");
    //    if (GameIsPaused)
    //    {
    //        Resume();
    //    }
    //    else
    //    {
    //        Pause();
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //ClickESC(); 


    }


    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //void Pause()
    //{
    //    _pauseMenuUI.SetActive(true);
    //    Time.timeScale = 0f;    // Freezes the game
    //    GameIsPaused = true;
    //}

    //public override void ClickESC()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (GameIsPaused)
    //        {
    //            Resume();
    //        }
    //        else
    //        {
    //            Pause();
    //        }
    //    }
    //}

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
