#pragma warning disable IDE1006 // Naming Styles
using System;
using UnityEngine;

[System.Serializable]
public class TimeTracker
{
    [field: SerializeField] public float timer { get; private set; } //The time limit
    public float tick { get; private set; } //The current time left
    [field: SerializeField] public float timeScale { get; private set; } = -1; //Counts up if positive, counts down if negative. Defaults to countdown.
    private bool isTicking = true;
    public event Action onFinish;
    public float TimePercentage => tick / timer;

    /// <summary>
    /// A simple timer. Defaults as a countdown timer.
    /// </summary>
    /// <param name="seconds">Set the timer where the tick will start at or go to</param>
    /// <param name="timeScale">Dictates the speed of the timer and it counts up (+) or down (-).</param>
    /// <param name="autoLoop">The timer loops? Defaults to false.</param>
    public TimeTracker(float seconds, float timeScale = -1)
    {
        SetTimeScale(timeScale);
        SetTimer(seconds);
    }

    /// <summary>
    /// Set the time scale, how fast is this timer?
    /// </summary>
    /// <param name="timeScale">Dictates the speed of the timer and it counts up (+) or down (-).</param>
    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }

    /// <summary>
    /// Change the timer.
    /// </summary>
    /// <param name="seconds">Timer value</param>
    /// <param name="resetTick">Auto call reset?</param>
    public void SetTimer(float seconds, bool resetTick = true)
    {
        timer = seconds;
        if (resetTick)
        {
            Reset();
        }
    }

    /// <summary>
    /// Resets tick to the starting value depending on the timescale.
    /// <br>For example: If set as a timer, set tick to 0 the starting value to the final value.</br>
    /// </summary>
    /// <param name="setToFinish">If true, set tick to the ending value.</param>
    /// <param name="autoInvoke">Auto invoke any subscribed delegates?</param>
    public void Reset(bool setToFinish = false)
    {
        tick = setToFinish ? (timeScale > 0 ? timer : 0) : (timeScale > 0 ? 0 : timer);
        isTicking = true;
    }

    /// <summary>
    /// Updates the timer.
    /// </summary>
    /// <param name="deltaTime">Pass through the UnityEngine.Time.deltaTime or another controlled time variable</param>
    /// <returns>The new tick value.</returns>
    public float Update(float deltaTime)
    {
        float delta = timeScale * deltaTime;
        float realTick = tick + delta;
        tick = Mathf.Clamp(realTick, 0, timer);
        if (isTicking && (timeScale < 0 && tick == 0 || timeScale > 0 && tick == timer))
        {
            isTicking = false;
            onFinish?.Invoke();
            if (isTicking)
            {
                return Update(realTick > timer ? realTick - timer : -realTick);
            }
        }
        return tick;
    }
}