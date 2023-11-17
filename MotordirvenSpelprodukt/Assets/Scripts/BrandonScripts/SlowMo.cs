using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo: MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowDownDuration = 2;
    public PauseMenu pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.GameIsPaused)
        {
            Time.timeScale += (1f / slowDownDuration) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime += (0.01f / slowDownDuration) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);
        }
    }

    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
