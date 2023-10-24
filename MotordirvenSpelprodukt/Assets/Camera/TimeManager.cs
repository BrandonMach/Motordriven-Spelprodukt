using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    static bool isTimePaused;

    public static float  DefaultTimeScale()
    {
        if (isTimePaused)
        {
            return Time.timeScale;
        }
        else
        {
            return 0;
        }
    }
}
