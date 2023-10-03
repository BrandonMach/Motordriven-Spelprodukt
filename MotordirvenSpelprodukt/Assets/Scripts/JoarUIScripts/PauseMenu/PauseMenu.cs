using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    }


    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
