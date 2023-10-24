using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuStartUp : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseSettingMenu;
    [SerializeField] private GameObject pauseAudioMenu;
    [SerializeField] private GameObject pauseVideoMenu;

    // Start is called before the first frame update
    void Start()
    {
        SetPauseMenuToDefault();
    }

    void OnEnable()
    {
        SetPauseMenuToDefault();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPauseMenuToDefault()
    {
        pauseMenu.SetActive(true);
        pauseSettingMenu.SetActive(false);
        pauseAudioMenu.SetActive(false);
        pauseVideoMenu.SetActive(false);
    }
}
