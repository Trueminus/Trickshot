using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    private static float currentTimeScale, slowedTimeScale = 0.1f, superSlowTimeScale = 0.01f,defaultTimeScale = 1f;
    //Time manipulation methods held in one class with standarized variables to avoid potential issues

    // What it says on the tin. Set time to slowed speed, assign it as current time for other methods
    public static void SlowTime(int timeIndex)
    {
        switch(timeIndex)
        {
            case 1:
                Time.timeScale = slowedTimeScale;
                currentTimeScale = slowedTimeScale;
                break;
            case 2:
                Time.timeScale = superSlowTimeScale;
                currentTimeScale = superSlowTimeScale;
                break;
            default:
                break;
        }
    }



    // Return time to default speed, adjust current time appropriately
    public static void ResetTime()
    {
        Time.timeScale = defaultTimeScale;

        currentTimeScale = defaultTimeScale;
    }

    // Turn off time
    public static void PauseTime()
    {
        Time.timeScale = 0f;
    }

    // Return time to current time value
    public static void ResumeTime()
    {
        Time.timeScale = currentTimeScale;
    }


}
