using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGate : MonoBehaviour
{
    private float startTime = 0;

    public void ResetTimer()
    {
        startTime = Time.time;
    }

    public void LogTime()
    {
        float curTime = Time.time - startTime;
        Debug.Log("Time Elapsed: " + curTime);
    }
}
