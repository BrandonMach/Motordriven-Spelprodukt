using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo: MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowDownDuration = 2;
    //public PauseMenu pauseMenu;

    public bool _returnSlowMo;
   
    void Update()
    {
        if (!PauseMenu.GameIsPaused &&  !_returnSlowMo)
        {
            Time.timeScale += (1f / slowDownDuration) * Time.unscaledDeltaTime;
            //Time.fixedDeltaTime += (0.01f / slowDownDuration) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            //Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);
        }
    }

    public void DoSlowmotion()
    {
        _returnSlowMo = false;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        

    }
    public void GoBackToNormal()
    {
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1;
    }
}
